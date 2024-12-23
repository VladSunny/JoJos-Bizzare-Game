using UnityEngine;
using Cysharp.Threading.Tasks;

using JJBG.Attributes;

namespace JJBG.Combat
{
    public class HoldController : MonoBehaviour, ISkillController
    {
        
        public event OnUsed onUsed;
        
        [Header("Dependencies")]
        [SerializeField] private CombatCore _combatCore;
        [SerializeField] private StunManager _stunManager;

        [Header("Controls")]
        [SerializeField] private string _actionName = "Punch";

        [Header("Settings")]
        [SerializeField] private float _playerStunDuration = 0f;
        [SerializeField] private float _cooldown = 3f;
        [SerializeField] private CombatType _combatType = CombatType.Standless;
        [SerializeField] private float _holdDuration = 4f;

        [Header("Timers")]
        [SerializeField, ReadOnly] private float _cooldownTimer = 0f;
        [SerializeField, ReadOnly] private float _holdTimer = 0f;

        private ISkill _skill;
        private PlayerControls _playerControls;

        private bool _activated = false;

        public CombatType GetCombatType() => _combatType;
        public float GetCooldown() => _cooldown;

        private void Awake() {
            if (_actionName != "")
                _playerControls = new PlayerControls();
            
            _skill = GetComponentInChildren<ISkill>();
        }

        private void OnEnable() {
            if (_playerControls != null) {
                _playerControls.Enable();
                
                _playerControls.FindAction(_actionName).performed += ctx => Activate();
                _playerControls.FindAction(_actionName).canceled += ctx => Deactivate();
            }
        }

        private void OnDisable() {
            if (_playerControls != null) {
                _playerControls.FindAction(_actionName).canceled -= ctx => Deactivate();
                _playerControls.FindAction(_actionName).performed -= ctx => Activate();

                _playerControls.Disable();
            }
        }
        
        private void Update() {
            if (_cooldownTimer > 0)
                _cooldownTimer -= Time.deltaTime;

            if (_holdTimer > 0)
                _holdTimer -= Time.deltaTime;
            else if (_activated)
                Deactivate();
        }

        public void Activate()
        {
            if (_cooldownTimer > 0) return;
            if (!_combatCore.CanAttack(_combatType)) return;

            _activated = true;

            _holdTimer = _holdDuration;

            _stunManager.SetStun(100f);

            _skill.Attack().Forget();
        }
        
        public void Deactivate()
        {
            if (!_activated) return;

            _cooldownTimer = _cooldown;
            
            _activated = false;

            onUsed?.Invoke();

            _stunManager.SetStun(_playerStunDuration, true);

            _skill.Stop().Forget();
        }
    }
}
