using UnityEngine;

namespace TPS
{
	public class Character : MonoBehaviour
	{
		[SerializeField] CameraBoom cameraBoom;
		[SerializeField] Rigidbody rigidBody;
		[SerializeField] Animator animator;
		
		[SerializeField] float turnSpeed = 10;
		[SerializeField] float maxSpeed = 6;
		[SerializeField] float threshold = 0.1f;
		
		const string horizontalBinding = "Horizontal";
		const string verticalBinding = "Vertical";

		static readonly int speedHash = Animator.StringToHash("Speed");
		
		float horizontalValue;
		float verticalValue;

		float yDirection;
		float currentSpeed;
		
		Quaternion GetForwardDirection()
		{
			if (cameraBoom)
			{
				return cameraBoom.transform.localRotation;
			}

			return transform.rotation;
		}

		void Update()
		{
			GetKeyboardAxisInput();
		}

		void FixedUpdate()
		{
			// float angle = Vector3.Angle(transform.forward, new Vector3(cameraBoom.GetCameraForward().x,0,cameraBoom.GetCameraForward().z));
			// float angle2 = GetForwardDirection().eulerAngles.y;
			
			if (Mathf.Abs(horizontalValue) > threshold
			    || Mathf.Abs(verticalValue) > threshold)
			{
				
				float atanRad = Mathf.Atan2(verticalValue, horizontalValue);
				float atan = atanRad * 57.2958f;
				yDirection = -(atan - 90f) + GetForwardDirection().eulerAngles.y;
				
				rigidBody.MoveRotation(Quaternion.Slerp(rigidBody.rotation, Quaternion.Euler(new Vector3(0, yDirection, 0)),
					Time.fixedDeltaTime * turnSpeed));

				Vector3 movement = transform.forward *
				                   (new Vector2(horizontalValue, verticalValue).magnitude * maxSpeed);

				rigidBody.velocity = movement;
			}

			currentSpeed = rigidBody.velocity.magnitude;
			animator.SetFloat(speedHash, currentSpeed / maxSpeed);
		}

		void GetKeyboardAxisInput()
		{
			if (!CursorManager.IsAvailable)
			{
				InitializeKeyboardInputInternal();
				return;
			}
			
			horizontalValue = Input.GetAxis(horizontalBinding);
			verticalValue = Input.GetAxis(verticalBinding);
		}

		void InitializeKeyboardInputInternal()
		{
			horizontalValue = 0;
			verticalValue = 0;
		}
	}
}