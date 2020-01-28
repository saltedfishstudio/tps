using UnityEngine;
using UniRx;
using UniRx.Triggers;

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

        [SerializeField] LayerMask groundLayerMask = default;
        [SerializeField] int throttleFrame = 3;
        [SerializeField] float groundTestRayLength = 0.1f;
        bool isGrounded = default;

        void Start()
        {
            Observable.EveryFixedUpdate()
                .Select(u => isGrounded)
                .DistinctUntilChanged()
                .ThrottleFrame(3)
                // .Where(x => x)
                .Subscribe(OnStableGrounded);
        }

        void OnStableGrounded(bool onGround)
        {
            animator.SetBool(isInAir, onGround);
        }

        void ValidateGround()
        {
            if (Physics.Raycast(transform.position + Vector3.up * 0.02f, Vector3.down, groundTestRayLength, groundLayerMask))
            {
                isGrounded = true;
                return;
            }

            isGrounded = false;
        }

        void FixedUpdate()
        {
            ValidateGround();
            
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