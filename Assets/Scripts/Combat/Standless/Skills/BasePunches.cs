using JJBG.Attributes;
using UnityEngine;

using JJBG.Combat.Standless.Attacks;
using Cysharp.Threading.Tasks;

namespace JJBG.Combat.Standless.Skills
{
    public class BasePunches : SkillBase
    {
        [Header("References")]
        [SerializeField] private Transform _playerObj;
        [SerializeField] private Animator _animator;

        [Header("Settings")]
        [SerializeField] private float _playerStunDuration = 2f;
        [SerializeField] private float _cooldown = 3f;
        [SerializeField] private float _timeForContinueCombo = 2.5f;

        [Header("Timers")]
        [SerializeField, ReadOnly] private float _comboTimer = 0f;
        [SerializeField, ReadOnly] private float _cooldownTimer = 0f;

        private CombatState _combatState;
        private BasePunch[] _attacks;

        private int _currentAttack = 0;

        public override void Initialize(CombatState combatState)
        {
            _combatState = combatState;
        }

        private void Start()
        {
            _attacks = GetComponentsInChildren<BasePunch>();

            for (int i = 0; i < _attacks.Length; i++)
            {
                _attacks[i].Initialize(_animator, _playerObj);
            }
        }

        private void Update()
        {
            if (_cooldownTimer > 0)
            {
                _cooldownTimer -= Time.deltaTime;
            }

            if (_comboTimer > 0)
            {
                _comboTimer -= Time.deltaTime;
            }

            if (_currentAttack > 0 && _comboTimer <= 0)
            {
                _cooldownTimer = _cooldown;
                _currentAttack = 0;
                return;
            }
        }

        public override void Activate()
        {
            if (!_combatState.CanAttack()) return;

            if (_cooldownTimer > 0) return;

            _comboTimer = _timeForContinueCombo;

            _combatState.SetStun(_playerStunDuration);
            _attacks[_currentAttack].Attack().Forget();

            _currentAttack++;

            if (_currentAttack == _attacks.Length)
            {
                _currentAttack = 0;
                _cooldownTimer = _cooldown;
            }
        }
    }
}
