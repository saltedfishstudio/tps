using System;
using System.Collections.Generic;
using UnityEngine;

namespace TPS
{
	public sealed class InputModule : MonoBehaviour
	{
		static Dictionary<string, Dictionary<EInputEvent, Action>> actionMap =
			new Dictionary<string, Dictionary<EInputEvent, Action>>();
		
		static Dictionary<string, List<Action<float>>> axisMap =
			new Dictionary<string, List<Action<float>>>();
		
		static List<IInputComponent> components = new List<IInputComponent>();

		[SerializeField] EUnityEvent BindTiming = EUnityEvent.UE_Start;
		[SerializeField] EUnityEvent ReleaseTiming = EUnityEvent.UE_Start;

		bool IsConfigured = false;
		[SerializeField] bool getInput = true;
		
		void Awake()
		{
			if (IsConfigured) return;
			
			if (BindTiming == EUnityEvent.UE_Awake)
			{
				SetUpPlayerInput();
			}
		}

		void Start()
		{
			if (IsConfigured) return;
			
			if (BindTiming == EUnityEvent.UE_Start)
			{
				SetUpPlayerInput();
			}
		}

		void OnEnable()
		{
			if (IsConfigured) return;
			
			if (BindTiming == EUnityEvent.UE_OnEnable)
			{
				SetUpPlayerInput();
			}
		}

		void Update()
		{
			if (!getInput)
				return;
			
			ExecuteActionMap();
			ExecuteAxisMap();
			ExecuteTouchMap();
		}

		void ExecuteActionMap()
		{
			foreach (string actionName in actionMap.Keys)
			{
				Dictionary<EInputEvent, Action> map = actionMap[actionName];
				foreach (EInputEvent inputEvent in map.Keys)
				{
					if (ConvertInput(inputEvent, actionName))
					{
						map[inputEvent].Invoke();
					}
				}
			}
		}

		void ExecuteAxisMap()
		{
			foreach (string axisName in axisMap.Keys)
			{
				float value = Input.GetAxis(axisName);
				foreach (Action<float> act in axisMap[axisName])
				{
					act.Invoke(value);
				}
			}
		}

		void ExecuteTouchMap()
		{
			
		}

		void SetUpPlayerInput()
		{
			foreach (IInputComponent component in components)
			{
				component.BindInput();
			}
			
			IsConfigured = true;
		}

		bool ConvertInput(EInputEvent keyEvent, string keyName)
		{
			switch (keyEvent)
			{
				case EInputEvent.IE_Pressed:
					return Input.GetButtonDown(keyName);
				
				case EInputEvent.IE_Released:
					return Input.GetButtonUp(keyName);

				case EInputEvent.IE_Repeat:
					break;
				case EInputEvent.IE_DoubleClick:
					break;
				case EInputEvent.IE_Axis:
					break;
				case EInputEvent.IE_MAX:
					break;
				default:
					throw new ArgumentOutOfRangeException(nameof(keyEvent), keyEvent, null);
			}

			return false;
		}

		public static void Register(IInputComponent component)
		{
			components.Add(component);
		}

		public static void Release(IInputComponent component)
		{
			component.ReleaseInput();
			
			components.Remove(component);
		}

		public static void BindAction(string actionName, EInputEvent keyEvent, object userClass, Action action)
		{
			if (!actionMap.TryGetValue(actionName, out var map))
			{
				map = new Dictionary<EInputEvent, Action>();
			}

			if (!map.TryGetValue(keyEvent, out var act))
			{
				act = action;
			}
			else
			{
				act += action;
			}

			map[keyEvent] = act;
			actionMap[actionName] = map;
		}

		public static void BindAxis(string axisName, object userClass, Action<float> action)
		{
			if (!axisMap.TryGetValue(axisName, out var act))
			{
				act = new List<Action<float>>();
			}

			act.Add(action);

			axisMap[axisName] = act;
		}

		public static void BindTouch(string touchName, EInputEvent keyEvent, object userClass, Action<Vector2> action)
		{
			
		}

		public static void UnbindAxis(string axisName, object userClass, Action<float> action)
		{
			if (axisMap.TryGetValue(axisName, out var act))
			{
				act.Remove(action);
				axisMap[axisName] = act;
			}
		}
	}
	
	public enum EInputEvent
	{
		IE_Pressed              = 0,
		IE_Released             = 1,
		IE_Repeat               = 2,
		IE_DoubleClick          = 3,
		IE_Axis                 = 4,
		IE_MAX                  = 5,
	}

	public enum EUnityEvent
	{
		UE_Awake                = 0,
		UE_Start                = 1,
		UE_OnEnable             = 2, 
	}

	public interface IInputComponent
	{
		void BindInput();
		void ReleaseInput();
	}
}