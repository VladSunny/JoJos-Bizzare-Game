using System;
using JJBG.Attributes;
using UnityEngine;

namespace JJBG.Combat
{
    public enum CombatStates
    {
        Idle,
        Falled,
        Stunned
    }

    public enum CombatTypes
    {
        Standless,
        Stand
    }

    public class CombatState : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private RagdollHandler _ragdollHandler;

        public event Action<CombatStates> OnStateChange;

        [SerializeField, ReadOnly] private CombatStates _currentState = CombatStates.Idle;
        [SerializeField, ReadOnly] private CombatTypes _combatType = CombatTypes.Standless;
        [SerializeField, ReadOnly] private float _currentStun = 0f;

        public void SetCombatType(CombatTypes combatType)
        {
            _combatType = combatType;
        }

        private void Awake()
        {
            SkillBase[] skills = GetComponentsInChildren<SkillBase>();

            for (int i = 0; i < skills.Length; i++)
            {
                skills[i].Initialize(this);
            }
        }

        private void OnEnable()
        {
            _ragdollHandler.onRagdollChanged += OnRagdollChanged;
        }

        private void OnDisable()
        {
            _ragdollHandler.onRagdollChanged -= OnRagdollChanged;
        }

        private void OnRagdollChanged(bool isRagdoll)
        {
            if (isRagdoll)
            {
                _currentState = CombatStates.Falled;
                OnStateChange?.Invoke(_currentState);
            }
            else
            {
                _currentState = CombatStates.Idle;
                OnStateChange?.Invoke(_currentState);
            }
        }

        private void Update()
        {
            if (_currentState == CombatStates.Falled) return;

            if (_currentStun > 0)
            {
                if (_currentState != CombatStates.Stunned)
                {
                    _currentState = CombatStates.Stunned;
                    OnStateChange?.Invoke(_currentState);
                }
                _currentStun -= Time.deltaTime;
            }
            else if (_currentState == CombatStates.Stunned)
            {
                _currentState = CombatStates.Idle;
                OnStateChange?.Invoke(_currentState);
            }
        }

        public bool CanAttack(CombatTypes combatType)
        {
            return _currentState == CombatStates.Idle && _combatType == combatType && _currentStun <= 0;
        }

        public void SetStun(float stun)
        {
            _currentStun = stun;
        }
    }
}
