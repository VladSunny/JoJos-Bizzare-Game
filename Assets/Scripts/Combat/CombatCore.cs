using UnityEngine;

namespace JJBG.Combat
{
    public class CombatCore : MonoBehaviour
    {
        [SerializeField] private CombatType _combatType;
        private Health _health;
        private StunManager _stunManager;

        private void Awake() {
            _health = GetComponent<Health>();
            _stunManager = GetComponent<StunManager>();
        }

        public bool CanAttack(CombatType type = CombatType.Standless) {
            if (type != CombatType.Other && _combatType != type) return false;

            return _stunManager.GetStun() <= 0 && _health.GetHealth() > 0;
        }

        public CombatType GetCombatType() {
            return _combatType;
        }

        public void SetCombatType(CombatType type) {
            _combatType = type;
        }
    }
}
