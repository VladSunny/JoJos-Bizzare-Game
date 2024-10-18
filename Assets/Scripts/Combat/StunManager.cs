using UnityEngine;

using JJBG.Attributes;
using JJBG.Movement;

namespace JJBG.Combat
{
    public class StunManager : MonoBehaviour
    {
        [Header("Dependencies")]
        [SerializeField] private Rigidbody _rb;
        [SerializeField] private CharacterMovement _movement;
        [SerializeField] private RagdollHandler _ragdollHandler;

        [Header("Settings")]
        [SerializeField] private float _stunMultiplier = 0.5f;
        [SerializeField] private float _ragdollStun = 6f;

        [Header("Stun")]
        [SerializeField, ReadOnly] private float _stun = 0f;

        [Header("Debug")]
        [SerializeField] private bool _debug = true;
        [SerializeField] private KeyCode _stunKey = KeyCode.Alpha3;

        private void OnEnable() {
            _ragdollHandler.onRagdollChanged += OnRagdollChanged;
        }

        private void OnDisable() {
            _ragdollHandler.onRagdollChanged -= OnRagdollChanged;
        }

        private void Update() {
            if (_stun > 0) {
                _stun -= Time.deltaTime;
                _movement.ChangeMovementState(CharacterMovement.MovementStates.Walking);
                SpeedControl();
            }

            if (_debug && Input.GetKeyDown(_stunKey))
                AddStun(3f);
        }

        public void AddStun(float stun) {
            _stun += stun;
        }

        private void SpeedControl()
        {
            float tragetSpeed = _movement.GetTargetSpeed();

            Vector3 flatVelocity = new Vector3(_rb.velocity.x, 0f, _rb.velocity.z);

            if (flatVelocity.magnitude > tragetSpeed * _stunMultiplier)
            {
                Vector3 limitedVelocity = flatVelocity.normalized * tragetSpeed * _stunMultiplier;
                _rb.velocity = new Vector3(limitedVelocity.x, _rb.velocity.y, limitedVelocity.z);
            }
        }

        private void OnRagdollChanged(bool isRagdoll) {
            if (isRagdoll)
                _stun = _ragdollStun;
        }
    }
}
