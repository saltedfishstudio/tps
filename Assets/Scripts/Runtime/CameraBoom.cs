using System;
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
            if (useProbe)
            {
                probe = targetCamera.GetComponent<SphereCollider>();
                if (probe == null)
                {
                    probe = targetCamera.gameObject.AddComponent<SphereCollider>();
                }

                probe.radius = probeSize;
                probe.isTrigger = true;
            }
        }

        void Update()
        {
            mouseX = Input.GetAxis(xAxisBinding);
            mouseY = Input.GetAxis(yAxisBinding);
            
            transform.position = targetOffset;
            transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles +
                                                  new Vector3(-mouseY * mouseYControl, (mouseX * mouseXControl), 0));
            
            targetCamera.transform.SetParent(transform);
            targetCamera.transform.localRotation = Quaternion.identity;
            targetCamera.transform.localPosition = Vector3.forward * -2;
            targetCamera.transform.SetParent(null);
        }

        void OnGUI()
        {
            GUI.TextArea(
                new Rect(
                    padding.x, 
                    padding.y, 
                    400, 
                    40)
                , $"x:{mouseX}, y:{mouseY}");
        }
    }
}