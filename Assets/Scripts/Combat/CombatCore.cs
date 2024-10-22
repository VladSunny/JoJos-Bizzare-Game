using UnityEngine;

namespace JJBG.Combat
{
    public class CombatCore : MonoBehaviour
    {
        private Health _health;
        private StunManager _stunManager;
        [SerializeField] private CombatType _combatType;

        private void Awake() {
            _health = GetComponent<Health>();
            _stunManager = GetComponent<StunManager>();

            _combatType = CombatType.Standless;
        }

        public bool CanAttack(CombatType type = CombatType.Standless) {
            if (_combatType != type) return false;

            return _stunManager.GetStun() <= 0 && _health.GetHealth() > 0;
        }
    }
}
