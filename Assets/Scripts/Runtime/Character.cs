using System;
using UnityEngine;

namespace TPS
{
	public class Character : MonoBehaviour, IInputComponent
	{
		[Header("Component")] 
		[SerializeField] CameraBoom cameraBoom = default;
		[SerializeField] Rigidbody rigidBody = default;
		[SerializeField] Animator animator = default;
		
		[Header("Setting")]
		[SerializeField] float turnSpeed = 10;
		[SerializeField] float maxSpeed = 6;
		[SerializeField] float threshold = 0.1f;

		[Header("Debug Option")]
		[SerializeField] bool useVelocity = true;
		[SerializeField] float forceValue = 10f;
		
		const string horizontalBinding = "Horizontal";
		const string verticalBinding = "Vertical";

		static readonly int speedHash = Animator.StringToHash("Speed");
		
		float horizontalValue;
		float verticalValue;

		float yDirection;
		float currentSpeed;

		void Awake()
		{
			InitializeInputValue();
			
			InputComponent.Register(this);
		}

		void FixedUpdate()
		{
			Vector3 movement = Vector3.zero;
			
			if (Mathf.Abs(horizontalValue) > threshold
			    || Mathf.Abs(verticalValue) > threshold)
			{
				float atanRad = Mathf.Atan2(verticalValue, horizontalValue);
				float atan = atanRad * 57.2958f;
				yDirection = -(atan - 90f) + GetForwardDirection().eulerAngles.y;
				
				rigidBody.MoveRotation(Quaternion.Slerp(rigidBody.rotation, Quaternion.Euler(new Vector3(0, yDirection, 0)),
					Time.fixedDeltaTime * turnSpeed));

				movement = transform.forward *
				                   (new Vector2(horizontalValue, verticalValue).magnitude * maxSpeed);

				rigidBody.MovePosition(rigidBody.transform.position + movement * Time.fixedDeltaTime);
			}

			animator.SetFloat(speedHash, movement.magnitude / maxSpeed);
		}

		void OnDestroy()
		{
			InputComponent.Release(this);
		}

		void IInputComponent.BindInput()
		{
			InputComponent.BindAxis(horizontalBinding, this, MoveRight);
			InputComponent.BindAxis(verticalBinding, this, MoveForward);
		}
		
		void IInputComponent.ReleaseInput()
		{
			InputComponent.UnbindAxis(horizontalBinding, this, MoveRight);
			InputComponent.UnbindAxis(verticalBinding, this, MoveForward);
		}

		void MoveForward(float value)
		{
			verticalValue = value;
		}

		void MoveRight(float value)
		{
			horizontalValue = value;
		}

		protected void  InitializeInputValue()
		{
			horizontalValue = 0;
			verticalValue = 0;
		}
				
		Quaternion GetForwardDirection()
		{
			if (cameraBoom)
			{
				return cameraBoom.transform.localRotation;
			}

			return transform.rotation;
		}
	}
}