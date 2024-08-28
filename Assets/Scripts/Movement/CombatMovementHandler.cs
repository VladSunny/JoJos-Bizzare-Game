using JJBG.Combat;
using UnityEngine;

namespace JJBG.Movement
{
    public class CombatMovementHandler : MonoBehaviour
    {
        private CombatState _combatState;
        private CharacterMovement _characterMovement;

        private void Awake()
        {
            _combatState = GetComponentInChildren<CombatState>();
            _characterMovement = GetComponent<CharacterMovement>();
        }

        private void OnEnable()
        {
            _combatState.OnStateChange += OnCombatStateChanged;
        }

        private void OnDisable()
        {
            _combatState.OnStateChange -= OnCombatStateChanged;
        }

        private void OnCombatStateChanged(CombatStates state)
        {
            if (state == CombatStates.Stunned)
                _characterMovement.ChangeMovementState(CharacterMovement.MovementStates.Stunned);
            if (state == CombatStates.Idle)
                _characterMovement.ChangeMovementState(CharacterMovement.MovementStates.Walking);
        }
    }
}
