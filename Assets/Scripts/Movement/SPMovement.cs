using UnityEngine;

using JJBG.Attributes;

namespace JJBG.Movement
{
    public enum MovementState
    {
        Idle,
        Hiding,
        Attacking
    }

    public class SP : MonoBehaviour
    {

        [Header("Settings")]
        [SerializeField] private float _idleMovementSpeed = 10f;
        [SerializeField] private float _combatMovementSpeed = 20f;
        [SerializeField] private float _rotationSpeed = 10f;
        [SerializeField] private float _eps = 0.01f;
        
        [Header("Points")]
        [SerializeField] private Transform _idlePosition;
        [SerializeField] private Transform _playerObj;
        [SerializeField] private Transform _attackPosition;

        [Header("Debug")]
        [SerializeField] private bool _debug = false;

        [Header("Timers")]
        [SerializeField, ReadOnly] private float _attackTimer = 0f;

        [Header("Movement state")]
        [SerializeField, ReadOnly] MovementState movementState = MovementState.Idle;

        private Vector3 _targetPosition;
        private float _changePositionSpeed = 0f;


        public void Initialize(Transform idlePosition, Transform playerObj, Transform attackPosition)
        {
            _idlePosition = idlePosition;
            _playerObj = playerObj;
            _attackPosition = attackPosition;
        }

        private void Update()
        {
            if (_attackTimer > 0) {
                _attackTimer -= Time.deltaTime;
                movementState = MovementState.Attacking;
            }

            if (_playerObj == null || _idlePosition == null || _attackPosition == null) return;

            switch (movementState)
            {
                case MovementState.Idle:
                    _targetPosition = _idlePosition.localPosition;
                    _changePositionSpeed = _idleMovementSpeed;
                    break;
                case MovementState.Hiding:
                    _targetPosition = _playerObj.localPosition;
                    _changePositionSpeed = _idleMovementSpeed;
                    break;
                case MovementState.Attacking:
                    _targetPosition = _attackPosition.localPosition;
                    _changePositionSpeed = _combatMovementSpeed;
                    break;
            }

            if (_debug && Input.GetKeyDown(KeyCode.Alpha5)) {
                if (movementState == MovementState.Attacking)
                    movementState = MovementState.Idle;
                else
                    movementState++;
            }

            MoveToTraget();
        }

        private void MoveToTraget()
        {
            if (Vector3.Distance(transform.localPosition, _targetPosition) > _eps)
                transform.localPosition = Vector3.Lerp(transform.localPosition, _targetPosition, Time.deltaTime * _changePositionSpeed);

            transform.forward = Vector3.Slerp(transform.forward, _playerObj.forward, Time.deltaTime * _rotationSpeed);
        }

        public void SetMovementState(MovementState state)
        {
            movementState = state;
        }

        public void SetAttackTimer(float time)
        {
            _attackTimer = time;
        }
    }
}
