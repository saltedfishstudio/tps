using UnityEngine;

namespace TPS
{
    public class SpringArm : MonoBehaviour, IInputComponent
    {
        [Header("Component")] 
        [SerializeField] Camera targetCamera = default;
        [SerializeField] GameObject target = default;

        [Header("Setting")]
        [SerializeField] float targetArmLength = 3f;
        [SerializeField] float mouseXControl = 2.0f;
        [SerializeField] float mouseYControl = 2.0f;
        [SerializeField] Vector3 targetOffset = new Vector3(0, 0.97f, 0);

        [Header("Probe")]
        [SerializeField] bool useProbe = true;
        [SerializeField] float probeSize = .12f;

        #region Internal

        /// <summary>
        /// sphere collider on camera gameObject to validate spring arm length.
        /// </summary>
        SphereCollider probe;
        
        /// <summary>
        /// cached mouse x axis value
        /// </summary>
        float mouseX;
        
        /// <summary>
        /// cached mouse y axis value
        /// </summary>
        float mouseY;

        const string xAxisBinding = "Mouse X";
        const string yAxisBinding = "Mouse Y";

        
        #endregion

        protected void Awake()
        {
            SetupProbe();
            InitializeInputValue();
            
            InputModule.Register(this);
        }

        protected void Update()
        {
            ProcessSpringArm();
        }

        void OnDestroy()
        {
            InputModule.Release(this);
        }

        protected void InitializeInputValue()
        {
            mouseX = 0;
            mouseY = 0;
        }

        void SetupProbe()
        {
            probe = targetCamera.GetComponent<SphereCollider>();
            probe.enabled = useProbe;
            
            if (useProbe)
            {
                probe.radius = probeSize;
                probe.isTrigger = true;
            }
        }

        void IInputComponent.BindInput()
        {
            InputModule.BindAxis(xAxisBinding, this, Turn);
            InputModule.BindAxis(yAxisBinding, this, LookUp);

        }

        void IInputComponent.ReleaseInput()
        {
            InputModule.UnbindAxis(xAxisBinding, this, Turn);
            InputModule.UnbindAxis(yAxisBinding, this, LookUp);
        }

        void Turn(float value)
        {
            mouseX = value;
        }

        void LookUp(float value)
        {
            mouseY = value;
        }


        void ProcessSpringArm()
        {
            transform.localPosition = target.transform.position + targetOffset;
            transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles +
                                                  new Vector3(-mouseY * mouseYControl, mouseX * mouseXControl, 0));
            
            targetCamera.transform.localRotation = Quaternion.identity;
            targetCamera.transform.localPosition = Vector3.forward * GetDesiredArmLength();
        }

        float GetDesiredArmLength()
        {
            // @todo::probe collide에 따라 targetArmLength 줄이기 구현 
            
            return -targetArmLength;
        }
    }
}