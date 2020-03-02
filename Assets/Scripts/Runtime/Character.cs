using UnityEngine;

namespace TPS
{
	public class Character : MonoBehaviour, IInputComponent
	{
		[Header("Component")] 
		[SerializeField] SpringArm springArm = default;
		[SerializeField] Rigidbody rigidBody = default;
		[SerializeField] Animator animator = default;
		[SerializeField] GameObject mesh = default;
		
		[Header("Setting")]
		[SerializeField] float turnSpeed = 10;
		[SerializeField] float maxSpeed = 6;
		[SerializeField] float threshold = 0.1f;
		
		const string horizontalBinding = "Horizontal";
		const string verticalBinding = "Vertical";

		const string jumpBinding = "Jump";
		
		static readonly int speedHash = Animator.StringToHash("Speed");
		
		float horizontalValue;
		float verticalValue;

		float yDirection;
		float currentSpeed;

		bool pressedJump;
		float jumpKeyHoldTime;
		float jumpMaxHoldTime;
		bool wasJumping;
		float jumpForceTimeRemaining;
		int jumpCurrentCount;
		
		void Awake()
		{
			InitializeInputValue();
			
			InputModule.Register(this);
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

				movement = mesh.transform.forward *
				           (new Vector2(horizontalValue, verticalValue).magnitude * maxSpeed);

				rigidBody.MovePosition(rigidBody.transform.position + movement * Time.fixedDeltaTime);
			}

			animator.SetFloat(speedHash, movement.magnitude / maxSpeed);
		}

		void OnDestroy()
		{
			InputModule.Release(this);
		}

		void IInputComponent.BindInput()
		{
			InputModule.BindAxis(horizontalBinding, this, MoveRight);
			InputModule.BindAxis(verticalBinding, this, MoveForward);       
			
			InputModule.BindAction(jumpBinding, EInputEvent.IE_Pressed, this, Jump);
			InputModule.BindAction(jumpBinding, EInputEvent.IE_Released, this, StopJumping);
		}
		
		void IInputComponent.ReleaseInput()
		{
			InputModule.UnbindAxis(horizontalBinding, this, MoveRight);
			InputModule.UnbindAxis(verticalBinding, this, MoveForward);
		}

		void MoveForward(float value)
		{
			verticalValue = value;
		}

		void MoveRight(float value)
		{
			horizontalValue = value;
		}

		void Jump()
		{
			pressedJump = true;
			jumpKeyHoldTime = 0;
		}

		void StopJumping()
		{
			pressedJump = false;
			ResetJumpState();
		}

		bool IsFalling()
		{
			return false;
		}

		bool CanJump()
		{
			return false;
		}

		bool DoJump()
		{
			return false;
		}
		
		void OnJumped()
		{
			
		}
		
		protected void  InitializeInputValue()
		{
			horizontalValue = 0;
			verticalValue = 0;
		}
				
		Quaternion GetForwardDirection()
		{
			if (springArm)
			{
				return springArm.transform.localRotation;
			}

			return transform.rotation;
		}

		void CheckJumpInput(float deltaTime)
		{
			if (pressedJump)
			{
				bool firstJump = jumpCurrentCount == 0;
				if (firstJump && IsFalling())
				{
					jumpCurrentCount++;
				}

				bool didJump = CanJump() && DoJump();
				if (didJump)
				{
					if (!wasJumping)
					{
						jumpCurrentCount++;
						jumpForceTimeRemaining = jumpMaxHoldTime;
						OnJumped();
					}
				}

				wasJumping = didJump;
			}
		}

		void ClearJumpInput(float deltaTime)
		{
			if (pressedJump)
			{
				jumpKeyHoldTime += deltaTime;

				if (jumpKeyHoldTime >= jumpMaxHoldTime)
				{
					pressedJump = false;
				}
			}
			else
			{
				jumpForceTimeRemaining = 0;
				wasJumping = false;
			}
		}

		void ResetJumpState()
		{
			pressedJump = false;
			wasJumping = false;
			jumpKeyHoldTime = 0f;
			jumpForceTimeRemaining = 0f;

			if (!IsFalling())
			{
				jumpCurrentCount = 0;
			}
		}
	}
}