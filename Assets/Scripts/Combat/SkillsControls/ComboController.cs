using UnityEngine;

using JJBG.Attributes;
using Cysharp.Threading.Tasks;

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

        [Header("Timers")]
        [SerializeField, ReadOnly] private float _comboTimer = 0f;
        [SerializeField, ReadOnly] private float _cooldownTimer = 0f;

        private ISkill[] _attacks;
        private PlayerControls _playerControls;

        private int _currentAttack = 0;

        private void Awake()
        {
            if (_actionName != "")
                _playerControls = new PlayerControls();
        }

        private void Start()
        {
            _attacks = GetComponentsInChildren<ISkill>();
        }

        private void OnEnable()
        {
            if (_playerControls != null)
                _playerControls.Enable();
        }

        private void OnDisable()
        {
            if (_playerControls != null)
                _playerControls.Disable();
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

            if (_playerControls != null && _playerControls.FindAction(_actionName).triggered) {
                Activate();
            }
        }

        public void Activate()
        {
            if (_cooldownTimer > 0) return;
            if (!_combatCore.CanAttack()) return;

            _comboTimer = _timeForContinueCombo;

            _stunManager.SetStun(_playerStunDuration);
            _attacks[_currentAttack].Attack().Forget();
            
            _currentAttack++;

            if (_currentAttack >= _attacks.Length)
            {
                _currentAttack = 0;
                _cooldownTimer = _cooldown;
            }
        }
    }
}
