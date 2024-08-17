using UnityEngine;

using JJBG.Check;

namespace JJBG.Movement
{
    [RequireComponent(typeof(Rigidbody))]
    [RequireComponent(typeof(GroundCheck))]

    public class CharacterMovement : MonoBehaviour
    {
        [Header("Movement")]
        [SerializeField] private float _walkSpeed = 3;
        [SerializeField] private float _runSpeed = 6;
        [SerializeField] private float _groundDrag = 4;
        [SerializeField] private float _speedTransitionDuration = 0.2f;

        [Header("Combat")]
        [SerializeField] private float _stunnedSpeed = 0.5f;

        [Header("Jump")]
        [SerializeField] private float _jumpForce = 12;
        [SerializeField] private float _jumpCooldown = 0.25f;
        [SerializeField] private float _airMultiplier = 0.4f;

        public enum MovementStates {
            Walking,
            Running,
            Stunned
        }
        private MovementStates _movementState;

        private bool canJump = true;
        private bool _grounded;
        private float _currentSpeed;
        private float _targetSpeed;

        private GroundCheck _groundCheck;
        private Animator _animator;
        private Rigidbody _rb;

        private void Awake()
        {
            _rb = GetComponent<Rigidbody>();
            _groundCheck = GetComponent<GroundCheck>();
            _animator = GetComponentInChildren<Animator>();

            _rb.freezeRotation = true;

            ChangeMovementState(MovementStates.Walking);

            _currentSpeed = _walkSpeed;
        }

        private void Update()
        {
            _grounded = _groundCheck.OnGround();

            SpeedControl();

            if (_grounded)
                _rb.drag = _groundDrag;
            else
                _rb.drag = 0;

            _currentSpeed = Mathf.Lerp(_currentSpeed, _targetSpeed, Time.deltaTime / _speedTransitionDuration);
        }

        private void LateUpdate()
        {
            _animator.SetFloat("Speed", _rb.velocity.magnitude);
        }

        public void Move(Vector3 movement)
        {
            Vector3 move = movement * 10f * _currentSpeed;

            if (!_grounded)
                move *= _airMultiplier;

            _rb.AddForce(move, ForceMode.Force);
        }

        private void SpeedControl()
        {
            Vector3 flatVelocity = new Vector3(_rb.velocity.x, 0f, _rb.velocity.z);

            if (flatVelocity.magnitude > _currentSpeed)
            {
                Vector3 limitedVelocity = flatVelocity.normalized * _currentSpeed;
                _rb.velocity = new Vector3(limitedVelocity.x, _rb.velocity.y, limitedVelocity.z);
            }
        }

        public void Jump()
        {
            if (!canJump || !_groundCheck.OnGround()) return;

            canJump = false;

            Invoke(nameof(ResetJump), _jumpCooldown);

            _rb.velocity = new Vector3(_rb.velocity.x, 0, _rb.velocity.z);
            _rb.AddForce(transform.up * _jumpForce, ForceMode.Impulse);
        }

        public void ChangeMovementState(MovementStates state)
        {
            _movementState = state;
            switch (state)
            {
                case MovementStates.Walking:
                    _targetSpeed = _walkSpeed;
                    break;
                case MovementStates.Running:
                    _targetSpeed = _runSpeed;
                    break;
                case MovementStates.Stunned:
                    _targetSpeed = _stunnedSpeed;
                    break;
            }
        }

        private void ResetJump()
        {
            canJump = true;
        }

        public MovementStates GetMovementState() => _movementState;
    }
}
