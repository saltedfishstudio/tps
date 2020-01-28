using UnityEngine;

namespace TPS.Anim
{
    [RequireComponent(typeof(Animator))]
    public class ThirdPersonAnimationController : MonoBehaviour
    {
        [SerializeField] Animator animator = default;
        [SerializeField] Rigidbody rigidBody = default;

        float currentSpeed = default;
        [SerializeField] float maxSpeed = default;

        // Parameter
        static readonly int speedRatio = Animator.StringToHash(speedParameter);
        static readonly int isInAir = Animator.StringToHash(isInAirParameter);
        
        // Triggers
        static readonly int onJumpStartComplete = Animator.StringToHash(onJumpStartParameter);
        static readonly int onJumpEndComplete = Animator.StringToHash(onJumpEndParameter);

        const string isInAirParameter = "IsInAir";
        const string speedParameter = "SpeedRatio";
        const string onJumpStartParameter = "OnJumpStartComplete";
        const string onJumpEndParameter = "OnJumpEndComplete";

        [SerializeField] bool isDebugMode = default;
        [SerializeField] bool isInAirDebug = false;
        [Range(0,375)]
        [SerializeField] float speedDebug = default;
        [SerializeField] float forcePowerDebug = 10.0f;

        void FixedUpdate()
        {
            if (isDebugMode)
            {
                if (isInAirDebug)
                {
                    rigidBody.AddForce(Vector3.up * forcePowerDebug);
                    animator.SetBool(isInAir, isInAirDebug);
                }

                animator.SetFloat(speedRatio, speedDebug / maxSpeed);
                return;
            }
            
            currentSpeed = rigidBody.velocity.magnitude;
            animator.SetFloat(speedRatio, currentSpeed / maxSpeed);
        }

        public void OnJumpStartCompleteNotify()
        {
            animator.SetTrigger(onJumpStartComplete);
        }

        public void OnJumpEndCompleteNotify()
        {
            animator.SetTrigger(onJumpEndComplete);
        }

        // this worked from ThirdPersonIdle.animClip
        public void TestFunction()
        {
            Debug.Log("Test Animation Event");
        }

#if UNITY_EDITOR
        void OnValidate()
        {
            if (animator == null)
            {
                animator = GetComponent<Animator>();
            }

            if (rigidBody == null)
            {
                rigidBody = GetComponent<Rigidbody>();
            }
        }
#endif
    }
}