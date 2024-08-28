using UnityEngine;
using Cysharp.Threading.Tasks;
using System.Threading.Tasks;

using JJBG.Combat.Standless.Attacks;
using JJBG.Movement;

namespace JJBG.Combat.StarPlatinum.Attacks
{
    public class SPBasePunch : BasePunch
    {
        [SerializeField] private float _delayDuration = 0.8f;

        [Header("References")]
        [SerializeField] private StarPlatinumMovement _starPlatinumMovement;

        public override async UniTask Attack()
        {
            _starPlatinumMovement.attackingTimer = _delayDuration;
            await base.Attack();
        }
    }
}
