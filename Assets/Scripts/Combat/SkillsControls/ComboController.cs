using UnityEngine;
using Cysharp.Threading.Tasks;

using JJBG.Attributes;

namespace JJBG.Combat
{
    public class ComboController : MonoBehaviour, ISkillController
    {
        [Header("Dependencies")]
        [SerializeField] private CombatCore _combatCore;
        [SerializeField] private StunManager _stunManager;

        [Header("Controls")]
        [SerializeField] private string _actionName = "Punch";

        [Header("Settings")]
        [SerializeField] private float _playerStunDuration = 2f;
        [SerializeField] private float _cooldown = 3f;
        [SerializeField] private float _timeForContinueCombo = 2.5f;
        [SerializeField] private CombatType _combatType = CombatType.Standless;

        [Header("Timers")]
        [SerializeField, ReadOnly] private float _comboTimer = 0f;
        [SerializeField, ReadOnly] private float _cooldownTimer = 0f;

        private ISkill[] _skills;
        private PlayerControls _playerControls;

        private int _currentAttack = 0;

        public CombatType GetCombatType() => _combatType;

        private void Awake()
        {
            if (_actionName != "")
                _playerControls = new PlayerControls();
            
            _skills = GetComponentsInChildren<ISkill>();
        }

        private void OnEnable()
        {
            if (_playerControls != null) {
                _playerControls.Enable();
                _playerControls.FindAction(_actionName).performed += ctx => Activate();
            }
        }

        private void OnDisable()
        {
            if (_playerControls != null) {
                _playerControls.FindAction(_actionName).performed -= ctx => Activate();
                _playerControls.Disable();
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
            }
        }

        public void Activate()
        {
            if (_cooldownTimer > 0) return;
            if (!_combatCore.CanAttack(_combatType)) return;

            _comboTimer = _timeForContinueCombo;

            _stunManager.SetStun(_playerStunDuration);
            _skills[_currentAttack].Attack().Forget();
            
            _currentAttack++;

            if (_currentAttack >= _skills.Length)
            {
                _currentAttack = 0;
                _cooldownTimer = _cooldown;
            }
        }
    }
}
