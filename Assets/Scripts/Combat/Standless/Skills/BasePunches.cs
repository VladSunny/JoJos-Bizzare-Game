using UnityEngine;

namespace JJBG.Combat.Standless.Skills
{
    public class BasePunches : MonoBehaviour, ISkill
    {
        [Header("Settings")]
        [SerializeField] private float _stunDuration = 2f;

        private CombatState _combatState;

        private IAttack[] _attacks;
        private int _currentAttack = 0;

        private void Awake() {
            _attacks = GetComponentsInChildren<IAttack>();
            _combatState = GetComponentInParent<CombatState>();
        }

        public void Activate() {
            if (!_combatState.CanAttack()) return;

            _combatState.SetStun(_stunDuration);
            _attacks[_currentAttack].Attack();
            _currentAttack = (_currentAttack + 1) % _attacks.Length;
        }
    }
}
