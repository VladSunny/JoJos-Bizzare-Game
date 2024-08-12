using UnityEngine;

using JJBG.Check;

namespace JJBG.Movement
{
    public class TurnWalking : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private GroundCheck _groundCheck;

        [Header("Settings")]
        [SerializeField] private float _tiltFactor = 10f;
        [SerializeField] private float _tiltSpeed = 5f;
        [SerializeField] private float _maxAngle = 20f;

        private Rigidbody _rb;

        private Quaternion targetRotation;

        private void Awake()
        {
            _rb = GetComponentInParent<Rigidbody>();
        }

        void Start()
        {
            targetRotation = transform.localRotation;
        }

        void Update()
        {
            Vector3 worldVelocity = _rb.velocity;

            Vector3 localVelocity = transform.InverseTransformDirection(worldVelocity);

            float localSpeedX = localVelocity.x;

            float tiltAngle = Mathf.Min(_maxAngle, localSpeedX * _tiltFactor);

            if (!_groundCheck.OnGround()) tiltAngle = 0f;

            targetRotation = Quaternion.Euler(0f, 0f, -tiltAngle);

            transform.localRotation = Quaternion.Lerp(transform.localRotation, targetRotation, Time.deltaTime * _tiltSpeed);
        }
    }
}
