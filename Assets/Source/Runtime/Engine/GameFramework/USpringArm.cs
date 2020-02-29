using System;
using UnityEngine;
using UnityEngine.PlayerLoop;

namespace Epic.Engine.GameFramework
{
	public class USpringArm : USceneComponent
	{
		#region UE4

		[SerializeField] public float targetArmLength;
		[SerializeField] public Vector3 socketOffset;
		[SerializeField] public Vector3 targetOffset;
		[SerializeField] public float probeSize;
		[SerializeField] public ECollisionChannel probeChannel;

		[SerializeField] public bool doCollisionTest = true;
		[SerializeField] public bool usePawnControlRotation = true;
		
		[SerializeField] public bool inheritPitch = true;
		[SerializeField] public bool inheritYaw = true;
		[SerializeField] public bool inheritRoll = true;
		
		[SerializeField] public bool enableCameraLag = true;
		[SerializeField] public bool enableCameraRotationLag = true;
		[SerializeField] public bool useCameraLagSubstepping = true;
		[SerializeField] public bool drawDebugLagMarkers = true;

		[SerializeField] public float cameraLagSpeed;
		[SerializeField] public float cameraRotationLagSpeed;
		[SerializeField] public float cameraLagMaxTimeStep;
		[SerializeField] public float cameraLagMaxDistance;

		public bool isCameraFixed = false;
		public Vector3 unfixedCameraPosition;

		public Vector3 previousDesiredLoc;
		public Vector3 previousArmOrigin;
		public Quaternion previousDesiredRot;

		public string socketName;

		protected Vector3 relativeSocketLocation;
		protected Quaternion relativeSocketRotation;
		
		public Quaternion GetDesiredRotation()
		{
			return GetComponentRotation();
		}
		
		public Quaternion GetTargetRotation()
		{
			var desiredRot = GetDesiredRotation();

			if (usePawnControlRotation)
			{
				if (GetOwner() is APawn owningPawn)
				{
					Quaternion pawnViewRotation = owningPawn.GetViewRotation();
					
					//if(desiredRot != pawnViewRotation)
					desiredRot = pawnViewRotation;
				}
			}

			if (!IsUsingAbsoluteRotation())
			{
				Quaternion localRelativeRotation = GetRelativeRotation();
				if (!inheritPitch)
				{
					desiredRot.x = localRelativeRotation.x;
				}

				if (!inheritYaw)
				{
					desiredRot.y = localRelativeRotation.y;
				}

				if (!inheritRoll)
				{
					desiredRot.z = localRelativeRotation.z;
				}
			}

			return desiredRot;
		}

		protected void UpdateDesiredArmLocation(bool doTrace, bool doLocationLag, bool doRotationLag, float deltaTime)
		{
			Quaternion desiredRot = GetTargetRotation();

			if (doRotationLag)
			{
				if (useCameraLagSubstepping && deltaTime > cameraLagMaxTimeStep && cameraRotationLagSpeed > 0.0f)
				{
					var armRotStep = Quaternion.Euler(
						(desiredRot.eulerAngles - previousDesiredRot.eulerAngles).normalized *
						(1.0f / deltaTime));

					var lerpTarget = previousDesiredRot;
					float remainingTime = deltaTime;
					while (remainingTime > 0)
					{
						float lerpAmount = Mathf.Min(cameraLagMaxTimeStep, remainingTime);
						lerpTarget *= Quaternion.Euler(armRotStep.eulerAngles * lerpAmount);
						remainingTime -= lerpAmount;

						desiredRot = Quaternion.Lerp(previousDesiredRot, lerpTarget, lerpAmount);
						previousDesiredRot = desiredRot;
					}
				}
				else
				{
					desiredRot = Quaternion.Lerp(previousDesiredRot, desiredRot, deltaTime);
				}
			}
			previousDesiredRot = desiredRot;

			
		}
		
		protected virtual Vector3 BlendLocations(Vector3 desiredArmLocation, Vector3 traceHitLocation,
			bool hitSomething, float deltaTime)
		{
			return hitSomething ? traceHitLocation : desiredArmLocation;
		}

		public override void ApplyWorldOffset(Vector3 inOffset, bool worldShift)
		{
			base.ApplyWorldOffset(inOffset, worldShift);
			previousDesiredLoc += inOffset;
			previousDesiredRot *= Quaternion.Euler(inOffset);
		}

		public override void OnRegister()
		{
			base.OnRegister();

			cameraLagMaxTimeStep = Mathf.Max(cameraLagMaxTimeStep, 1.0f / 200.0f);
			cameraLagSpeed = Mathf.Max(cameraLagSpeed, 0.0f);
			
			UpdateDesiredArmLocation(false, false, false, 0.0f);
		}

		public override void PostLoad()
		{
			base.PostLoad();
		}

		public override void TickComponent(float deltaTime, ELevelTick tickType, FActorComponentTickFunction thisTickFunction)
		{
			base.TickComponent(deltaTime, tickType, thisTickFunction);
			UpdateDesiredArmLocation(doCollisionTest, enableCameraLag, enableCameraRotationLag,deltaTime );
		}

		public override FTransform GetSocketTransform()
		{
			Debug.Log("Not Implemented");
			return base.GetSocketTransform();
		}

		public override bool HasAnySockets()
		{
			return false;
		}

		public Vector3 GetUnfixedCameraPosition()
		{
			return unfixedCameraPosition;
		}

		public bool IsCollisionFixApplied()
		{
			return isCameraFixed;
		}

		#endregion

		protected override void Initialize()
		{
			base.Initialize();
			
			primaryComponentTick.canEverTick = 1;
			primaryComponentTick.tickGroup = ETickingGroup.TG_PostPhysics;
	
			autoActivate = true;
			tickInEditor = true;
			usePawnControlRotation = false;
			doCollisionTest = true;

			inheritPitch = true;
			inheritYaw = true;
			inheritRoll = true;

			targetArmLength = 300.0f;
			probeSize = 12.0f;
			probeChannel = ECollisionChannel.ECC_Camera;

			relativeSocketRotation = Quaternion.identity;

			useCameraLagSubstepping = true;
			cameraLagSpeed = 10.0f;
			cameraRotationLagSpeed = 10.0f;
			cameraLagMaxTimeStep = 1.0f / 60.0f;
			cameraLagMaxDistance = 0.0f;

			unfixedCameraPosition = Vector3.zero;
		}
	}
}