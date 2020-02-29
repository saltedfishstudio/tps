using System;
using System.Threading.Tasks;
using UnityEngine;

namespace TPS
{
	public class Character : MonoBehaviour
	{
		[SerializeField] GameObject cameraBoom;
		[SerializeField] Rigidbody rigidBody;
		[SerializeField] Animator animator;
		[SerializeField] float turnSpeed = 10;
		[SerializeField] float speed = 10;
		
		const string horizontalBinding = "Horizontal";
		const string verticalBinding = "Vertical";

		float horizontalValue;
		float verticalValue;

		[SerializeField]
		float threshold = 0.1f;
		float yDirection;
		static readonly int speedHash = Animator.StringToHash("Speed");
		float currentSpeed;
		
		Quaternion GetForwardVector()
		{
			if (cameraBoom)
			{
				return cameraBoom.transform.localRotation;
			}

			return transform.rotation;
		}

		void Update()
		{
			horizontalValue = Input.GetAxis(horizontalBinding);
			verticalValue = Input.GetAxis(verticalBinding);

		}

		void FixedUpdate()
		{
			if (Mathf.Abs(horizontalValue) > threshold
			    || Mathf.Abs(verticalValue) > threshold)
			{
				float atanRad = Mathf.Atan2(verticalValue, horizontalValue);
				float atan = atanRad * 57.2958f;
				yDirection = -(atan - 90f) + 60f;
				
				rigidBody.MoveRotation(Quaternion.Slerp(rigidBody.rotation, Quaternion.Euler(new Vector3(0, yDirection + GetForwardVector().eulerAngles.y, 0)),
					Time.fixedDeltaTime * turnSpeed));

				Vector3 movement = transform.forward *
				                   (new Vector2(horizontalValue, verticalValue).magnitude * speed * Time.fixedDeltaTime);

				// Apply this movement to the rigidbody's position.
				rigidBody.MovePosition(rigidBody.position + movement);
				
				// rigidBody.velocity =
			}

			Debug.Log($"{rigidBody.velocity.magnitude:N5}");
			currentSpeed = rigidBody.velocity.magnitude;
			animator.SetFloat(speedHash, currentSpeed);
		}
		
		void OnGUI()
		{
			GUI.TextField(
				new Rect(
					20, 
					40, 
					400, 
					20)
				, $"h:{horizontalValue:N2}, v:{verticalValue:N2}, s:{currentSpeed:N2}");
		}
	}
}