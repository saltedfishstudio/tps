using UnityEngine;

namespace TPS
{
    public class CameraBoom : MonoBehaviour
    {
        [SerializeField] Camera targetCamera;
        [SerializeField] GameObject target;

        [SerializeField] float targetArmLength = 3f;
        [SerializeField] float mouseXControl = 2.0f;
        [SerializeField] float mouseYControl = 2.0f;

        [SerializeField] bool useProbe = true;
        [SerializeField] public float probeSize = .12f;
        
        [SerializeField] Vector3 targetOffset = new Vector3(0, 0.97f, 0);
        
        SphereCollider probe;
        float mouseX;
        float mouseY;
        readonly Vector2 padding = new Vector2(20,20);

        const string xAxisBinding = "Mouse X";
        const string yAxisBinding = "Mouse Y";

        void Awake()
        {
            probe = targetCamera.GetComponent<SphereCollider>();
            probe.enabled = useProbe;
            
            if (useProbe)
            {
                probe.radius = probeSize;
                probe.isTrigger = true;
            }

            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
            
            transform.localPosition = target.transform.position + targetOffset;
        }

        void Update()
        {
            mouseX = Input.GetAxis(xAxisBinding);
            mouseY = Input.GetAxis(yAxisBinding);
            
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