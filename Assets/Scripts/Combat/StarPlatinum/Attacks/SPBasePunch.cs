using UnityEngine;

using JJBG.Combat.Standless.Attacks;
using JJBG.Movement;
using System.Threading.Tasks;

namespace JJBG.Combat.StarPlatinum.Attacks
{
    public class SPBasePunch : BasePunch
    {
        [Header("References")]
        [SerializeField] private StarPlatinumMovement _starPlatinumMovement;

        public override async void Attack() {
            _starPlatinumMovement.movementState = MovementState.Attacking;
            
            base.Attack();
            await Task.Delay(_punchDelay);

            _starPlatinumMovement.movementState = MovementState.Idle;
        }
    }
}
