using UnityEngine;

namespace JJBG
{
    public class StandUpHandler : MonoBehaviour
    {
        [Header("Settings")]
        [SerializeField] private float _timeToStandUp = 3f;

        private RagdollHandler _ragdollHandler;

        private float _standUpTimer = 0f;

        private void Awake() {
            _ragdollHandler = GetComponentInParent<RagdollHandler>();
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
                _ragdollHandler.Disable();
            }
        }

        private void OnRagdollChanged(bool isRagdoll) {
            if (isRagdoll) {
                _standUpTimer = _timeToStandUp;
            }
        }
    }
}
