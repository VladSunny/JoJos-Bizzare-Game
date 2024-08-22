using JJBG.Attributes;
using JJBG.Movement;
using UnityEngine;

using Cysharp.Threading;
using Cysharp.Threading.Tasks;

namespace JJBG.Combat.StarPlatinum.Skills
{
    public class Summon : MonoBehaviour, ISkill
    {
        [Header("References")]
        [SerializeField] private HideHandler _hideHandler;
        [SerializeField] private StarPlatinumMovement _SPMovement;

        [Header("Settings")]
        [SerializeField] private bool _enabled = false;
        [SerializeField] private int _hideDuration = 1000;
        [SerializeField] private float _cooldown = 2f;

        [Header("Timers")]
        [ReadOnly, SerializeField] private float _cooldownTimer = 0f;

        private void Start() {
            _enabled = !_enabled;
            Activate();
        }

        private void Update() {
            if (_cooldownTimer >= 0)
                _cooldownTimer -= Time.deltaTime;
        }

        public async void Activate()
        {
            if (_cooldownTimer > 0) return;

            _enabled = !_enabled;
            _cooldownTimer = _cooldown;

            if (_enabled) {
                _hideHandler.Show();
                _SPMovement.movementState = MovementState.Idle;
            }
            else {
                _SPMovement.movementState = MovementState.Hiding;

                await UniTask.Delay(_hideDuration);

                _hideHandler.Hide();
            }
        }
    }
}
