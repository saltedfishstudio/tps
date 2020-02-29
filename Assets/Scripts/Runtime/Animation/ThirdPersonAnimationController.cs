using System;
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

        [SerializeField]
        float currentSpeed = default;
        [SerializeField] float maxSpeed = default;
        [SerializeField] Renderer[] renderers = new Renderer[0];
        [SerializeField] GameObject estimatedPosition = default;

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
        bool isJumping = false;
        Action OnJumpKeyDown;
        Action OnJumpKeyUp;
        KeyCode jumpKey = KeyCode.Space;

        void Start()
        {
            OnJumpKeyDown += OnJumpStart;
            OnJumpKeyUp += OnJumpEnded;
            
            Observable.EveryFixedUpdate()
                .Select(u => isGrounded)
                .DistinctUntilChanged()
                .ThrottleFrame(3)
                // .Where(x => x)
                .Subscribe(OnStableGrounded);

            Observable.EveryUpdate()
                .Where(e => Input.GetKeyDown(jumpKey))
                .Subscribe(e =>
                {
                    OnJumpKeyDown.Invoke();
                });

            Observable.EveryUpdate()
                .Where(e => Input.GetKeyUp(jumpKey))
                .Subscribe(e =>
                {
                    OnJumpKeyUp.Invoke();
                });

            
            // instancing material
            foreach (Renderer meshRenderer in renderers)
            {
                var instances = new Material[meshRenderer.sharedMaterials.Length];
                for (int i = 0; i < meshRenderer.sharedMaterials.Length; i++)
                {
                    instances[i] = Instantiate(meshRenderer.sharedMaterials[i]);
                }

                meshRenderer.sharedMaterials = instances;
            }
        }

        void OnJumpStart()
        {
            if (isJumping)
            {
                return;
            }
            
            rigidBody.AddForce(Vector3.up * forcePowerDebug);
            isJumping = true;
        }

        void OnJumpEnded()
        {
            if (!isJumping)
            {
                return;
            }

            isJumping = false;
        }

        bool checker = false;

        void OnStableGrounded(bool onGround)
        {
            if (onGround && !checker)
            {
                checker = true;
                Debug.Log("Checked");
                return;
            }
            
            animator.SetBool(isInAir, !onGround);
        }

        Color color;
        static readonly int color1 = Shader.PropertyToID("_Color");

        void ValidateGround()
        {
            Debug.DrawRay(transform.position, Vector3.down * groundTestRayLength);
            
            estimatedPosition.transform.position = transform.position + rigidBody.velocity * (Time.fixedDeltaTime * 2);
            
            if (Physics.Raycast(transform.position + Vector3.up * 0.02f, Vector3.down, groundTestRayLength, groundLayerMask))
            {
                isGrounded = true;
                color = Color.red;

                return;
            }

            color = Color.green;
            isGrounded = false;
        }

        void Update()
        {
            Debug.DrawRay(transform.position, Vector3.down * groundTestRayLength, color);
            
            foreach (Renderer render in renderers)
            {
                foreach (Material material in render.sharedMaterials)
                {
                    material.SetColor(color1, color);
                }
            }
        }

        void FixedUpdate()
        {
            currentSpeed = rigidBody.velocity.magnitude;
            
            ValidateGround();

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