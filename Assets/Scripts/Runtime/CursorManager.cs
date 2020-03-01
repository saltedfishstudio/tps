using UnityEngine;

namespace TPS
{
	public class CursorManager : MonoBehaviour
	{
		[SerializeField] KeyCode escapeLockMode = KeyCode.Escape;
		[SerializeField] bool autoLock = true;
		
		static CursorManager instance = default;
		static bool isLocked = default;

		public static bool IsAvailable
		{
			get { return isLocked; }
		}
        
		void Awake()
		{
			instance = this;
			SetCursorMode(autoLock);
		}

		void Update()
		{
			if (Input.GetKeyDown(escapeLockMode))
			{
				ToggleCursorMode();
			}
		}

		void SetCursorMode(bool lockMode)
		{
			if (lockMode)
			{
				Cursor.visible = false;
				Cursor.lockState = CursorLockMode.Locked;
				isLocked = true;
			}
			else
			{
				Cursor.visible = true;
				Cursor.lockState = CursorLockMode.None;
				isLocked = false;
			}
		}

		public static void ToggleCursorMode()
		{
			instance.SetCursorMode(!isLocked);
		}
	}
}