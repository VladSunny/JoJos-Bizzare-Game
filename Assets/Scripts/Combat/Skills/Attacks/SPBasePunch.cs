using UnityEngine;

using JJBG.Combat.Standless.Attacks;
using JJBG.Movement;
using Cysharp.Threading.Tasks;

namespace JJBG.Combat.StarPlatinum.Attacks
{
    public class SPBasePunch : BasePunch
    {
        [Header("References")]
        [SerializeField] private StarPlatinumMovement _starPlatinumMovement;

        public override async UniTask Attack()
        {
            _starPlatinumMovement.movementState = MovementState.Attacking;

            await base.Attack();

            _starPlatinumMovement.movementState = MovementState.Idle;
        }
    }
}
