using UnityEngine;

namespace JJBG.Combat
{
    public class CombatCore : MonoBehaviour
    {
        private Health _health;
        private StunManager _stunManager;

        private void Awake() {
            _health = GetComponent<Health>();
            _stunManager = GetComponent<StunManager>();
        }

        public bool CanAttack() {
            return _stunManager.GetStun() <= 0 && _health.GetHealth() > 0;
        }
    }
}
