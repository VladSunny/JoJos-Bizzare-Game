using UnityEngine;
using Cysharp.Threading.Tasks;

using JJBG.Attributes;

namespace JJBG.Combat
{
    public class UnoController : MonoBehaviour, ISkillController
    {
        [Header("Dependencies")]
        [SerializeField] private CombatCore _combatCore;
        [SerializeField] private StunManager _stunManager;

        [Header("Controls")]
        [SerializeField] private string _actionName = "Punch";

        [Header("Settings")]
        [SerializeField] private float _playerStunDuration = 2f;
        [SerializeField] private float _cooldown = 3f;
        [SerializeField] private CombatType _combatType = CombatType.Standless;

        [Header("Timers")]
        [SerializeField, ReadOnly] private float _cooldownTimer = 0f;

        private ISkill skill;
        private PlayerControls _playerControls;

        public CombatType GetCombatType() => _combatType;

        private void Awake() {
            if (_actionName != "")
                _playerControls = new PlayerControls();

            skill = GetComponentInChildren<ISkill>();
        }

        private void OnEnable() {
            if (_playerControls != null)
                _playerControls.Enable();
        }

        private void OnDisable() {
            if (_playerControls != null)
                _playerControls.Disable();
        }

        private void Update() {
            if (_cooldownTimer > 0)
                _cooldownTimer -= Time.deltaTime;
            
            if (_playerControls != null && _playerControls.FindAction(_actionName).triggered) {
                Activate();
            }
        }

        public void Activate()
        {
            if (_cooldownTimer > 0) return;
            if (!_combatCore.CanAttack(_combatType)) return;

            _stunManager.SetStun(_playerStunDuration);
            skill.Attack().Forget();

            _cooldownTimer = _cooldown;
        }
    }
}
