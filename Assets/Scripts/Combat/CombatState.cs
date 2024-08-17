using System;
using JJBG.Attributes;
using UnityEngine;

namespace JJBG.Combat
{
    public class CombatState : MonoBehaviour
    {
        public event Action<CombatStates> OnStateChange;
        public enum CombatStates {
            Idle,
            Defending,
            Stunned
        }

        [SerializeField, ReadOnly] private CombatStates _currentState = CombatStates.Idle;
        [SerializeField, ReadOnly] private float _currentStun = 0f;

        private void Update() {
            if (_currentStun > 0) {
                if (_currentState != CombatStates.Stunned) {
                    _currentState = CombatStates.Stunned;
                    OnStateChange?.Invoke(_currentState);
                }
                _currentStun -= Time.deltaTime;
            }
            else if (_currentState == CombatStates.Stunned) {
                _currentState = CombatStates.Idle;
                OnStateChange?.Invoke(_currentState);
            }
        }

        public bool CanAttack() {
            return _currentState == CombatStates.Idle;
        }

        public void SetStun(float stun) {
            _currentStun = stun;
        }
    }
}
