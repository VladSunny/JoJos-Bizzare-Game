using UnityEngine;

namespace JJBG.Movement
{
    public enum MovementState {
            Idle,
            Hiding,
            Attacking
    }

    public class StarPlatinumMovement : MonoBehaviour
    {
        
        [Header("Settings")]
        [SerializeField] private float _changePositionSpeed = 10f;
        [SerializeField] private float _eps = 0.01f;

        private Transform _idlePosition;
        private Transform _playerObj;
        private Transform _attackPosition;

        private Vector3 _targetPosition;

        public MovementState movementState = MovementState.Idle;

        public void Initialize(Transform idlePosition, Transform playerObj) {
            _idlePosition = idlePosition;
            _playerObj = playerObj;
        }

        private void Update() {
            if (_playerObj == null || _idlePosition == null) return;
            
            switch (movementState) {
                case MovementState.Idle:
                    _targetPosition = _idlePosition.position;
                    break;
                case MovementState.Hiding:
                    _targetPosition = _playerObj.position;
                    break;
                case MovementState.Attacking:
                    _targetPosition = _attackPosition.position;
                    break;
            }
        }

        private void LateUpdate() {
            if (Vector3.Distance(transform.position, _targetPosition) > _eps)
                transform.position = Vector3.Lerp(transform.position, _targetPosition, Time.deltaTime * _changePositionSpeed);
            
            transform.forward = _playerObj.forward;
        }

    }
}
