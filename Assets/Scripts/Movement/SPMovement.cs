using JJBG.Attributes;
using UnityEngine;

namespace JJBG.Movement
{
    public enum MovementState
    {
        Idle,
        Hiding,
        Attacking
    }

    public class StarPlatinumMovement : MonoBehaviour
    {

        [Header("Settings")]
        [SerializeField] private float _idleMovementSpeed = 10f;
        [SerializeField] private float _combatMovementSpeed = 20f;
        [SerializeField] private float _rotationSpeed = 10f;
        [SerializeField] private float _eps = 0.01f;

        [Header("Timers")]
        [SerializeField, ReadOnly] public float attackingTimer = 0f;

        private float _changePositionSpeed = 0f;

        private Transform _idlePosition;
        private Transform _playerObj;
        private Transform _attackPosition;

        private Vector3 _targetPosition;

        public MovementState movementState = MovementState.Idle;

        public void Initialize(Transform idlePosition, Transform playerObj, Transform attackPosition)
        {
            _idlePosition = idlePosition;
            _playerObj = playerObj;
            _attackPosition = attackPosition;
        }

        private void Update()
        {
            if (_playerObj == null || _idlePosition == null) return;

            if (attackingTimer > 0)
            {
                movementState = MovementState.Attacking;
                attackingTimer -= Time.deltaTime;
            }
            else if (movementState == MovementState.Attacking)
            {
                movementState = MovementState.Idle;
            }

            switch (movementState)
            {
                case MovementState.Idle:
                    _targetPosition = _idlePosition.position;
                    _changePositionSpeed = _idleMovementSpeed;
                    break;
                case MovementState.Hiding:
                    _targetPosition = _playerObj.position;
                    _changePositionSpeed = _idleMovementSpeed;
                    break;
                case MovementState.Attacking:
                    _targetPosition = _attackPosition.position;
                    _changePositionSpeed = _combatMovementSpeed;
                    break;
            }

            MoveToTraget();
        }

        private void MoveToTraget()
        {
            if (Vector3.Distance(transform.position, _targetPosition) > _eps)
                transform.position = Vector3.Lerp(transform.position, _targetPosition, Time.deltaTime * _changePositionSpeed);

            transform.forward = Vector3.Slerp(transform.forward, _playerObj.forward, Time.deltaTime * _rotationSpeed);
        }

    }
}
