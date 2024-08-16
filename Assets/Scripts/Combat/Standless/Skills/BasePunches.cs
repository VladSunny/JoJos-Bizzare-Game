using UnityEngine;

using JJBG.Combat.Standless.Attacks;

namespace JJBG.Combat.Standless.Skills
{
    public class BasePunches : MonoBehaviour, ISkill
    {
        private IAttack[] _attacks;
        private int _currentAttack = 0;

        private void Awake() {
            _attacks = GetComponentsInChildren<IAttack>();
        }

        public void Activate() {
            _attacks[_currentAttack].Attack();
            _currentAttack = (_currentAttack + 1) % _attacks.Length;
        }
    }
}
