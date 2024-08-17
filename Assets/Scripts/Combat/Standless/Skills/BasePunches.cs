using JJBG.Attributes;
using UnityEngine;

namespace JJBG.Combat.Standless.Skills
{
    public class BasePunches : SkillBase
    {
        [Header("Settings")]
        [SerializeField] private float _playerStunDuration = 2f;
        [SerializeField] private float _cooldown = 3f;
        [SerializeField] private float _timeForContinueCombo = 2.5f;

        [Header("Timers")]
        [SerializeField, ReadOnly] private float _comboTimer = 0f;
        [SerializeField, ReadOnly] private float _cooldownTimer = 0f;

        private CombatState _combatState;

        private IAttack[] _attacks;
        private int _currentAttack = 0;

        private void Awake() {
            _attacks = GetComponentsInChildren<IAttack>();
            _combatState = GetComponentInParent<CombatState>();
        }

        private void Update() {
            if (_cooldownTimer > 0) {
                _cooldownTimer -= Time.deltaTime;
            }
            
            if (_comboTimer > 0) {
                _comboTimer -= Time.deltaTime;
            }

            if (_currentAttack > 0 && _comboTimer <= 0) {
                _cooldownTimer = _cooldown;
                _currentAttack = 0;
                return;
            }
        }

        public override void Activate() {
            if (_cooldownTimer > 0) return;

            _comboTimer = _timeForContinueCombo;

            if (!_combatState.CanAttack()) return;

            _combatState.SetStun(_playerStunDuration);
            _attacks[_currentAttack].Attack();

            _currentAttack++;

            if (_currentAttack == _attacks.Length) {
                _currentAttack = 0;
                _cooldownTimer = _cooldown;
            }
        }
    }
}
