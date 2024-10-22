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

            _combatType = CombatType.Standless;
        }

        public bool CanAttack(CombatType type = CombatType.Standless) {
            if (_combatType != type) return false;

            return _stunManager.GetStun() <= 0 && _health.GetHealth() > 0;
        }

        public void SetCombatType(CombatType type) {
            _combatType = type;
        }
    }
}
