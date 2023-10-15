using System.Reflection;
using UnityEngine;

namespace Yohoho.Scripts.Monbeh
{
    public class CameraFollow : MonoBehaviour
    {
        public Transform Target;
        public Vector3 Offset;

        public float smoothSpeed = 0.1f;

        private void Start()
        {
            Offset = transform.position - Target.position;
        }

        private void LateUpdate()
        {
            SmoothFollow();
        }

        public void SmoothFollow()
        {
            Vector3 targetPos = Target.position + Offset;
            Vector3 smoothFollow = Vector3.Lerp(transform.position,
            targetPos, smoothSpeed);

            transform.position = smoothFollow;
            transform.LookAt(Target);
        }
    }
}