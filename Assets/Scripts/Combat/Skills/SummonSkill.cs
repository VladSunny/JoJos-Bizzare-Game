using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using UnityEngine;

using JJBG.Movement;

namespace JJBG.Combat
{
    public class SummonSkill : MonoBehaviour, ISkill
    {
        [Header("Dependencies")]
        [SerializeField] private SPMovement _spMovement;
        [SerializeField] private GameObject _spObj;
        [SerializeField] private CombatCore _combatCore;

        [Header("Settings")]
        [Tooltip("In milliseconds"), SerializeField] private int _hideDuration;

        public async UniTask Attack()
        {
            if (_combatCore.GetCombatType() == CombatType.Stand) {
                if (!_spMovement.SetMovementState(MovementState.Hiding)) return;
                _combatCore.SetCombatType(CombatType.Standless);

                await Task.Delay(_hideDuration);
                
                _spObj.SetActive(false);
            }
            else {
                if (!_spMovement.SetMovementState(MovementState.Idle)) return;
                _combatCore.SetCombatType(CombatType.Stand);
                _spObj.SetActive(true);
            }
        }

        public UniTask Stop()
        {
            return UniTask.CompletedTask;
        }
    }
}
