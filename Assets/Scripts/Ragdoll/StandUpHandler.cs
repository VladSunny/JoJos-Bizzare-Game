using UnityEngine;

using JJBG.Combat;

namespace JJBG
{
    public class StandUpHandler : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private CombatState _combatState;

        [Header("Settings")]
        [SerializeField] private float _timeToStandUp = 3f;
        [SerializeField] private float _standUpDuration = 1f;
        [SerializeField] private const string _animationName = "StandingUp";

        private RagdollHandler _ragdollHandler;
        private Animator _animator;

        private float _standUpTimer = 0f;

        private void Awake() {
            _ragdollHandler = GetComponentInParent<RagdollHandler>();
            _animator = GetComponent<Animator>();
        }

        private void OnEnable() {
            _ragdollHandler.onRagdollChanged += OnRagdollChanged;
        }

        private void OnDisable() {
            _ragdollHandler.onRagdollChanged -= OnRagdollChanged;
        }

        private void Update() {
            if (_standUpTimer > 0) {
                _standUpTimer -= Time.deltaTime;
            }
            else if (_ragdollHandler.IsRagdoll) {
                _combatState.SetStun(_standUpDuration);
                _ragdollHandler.Disable();
                _animator.Play(_animationName);
            }
        }

        private void OnRagdollChanged(bool isRagdoll) {
            if (isRagdoll) {
                _standUpTimer = _timeToStandUp;
            }
        }
    }
}
