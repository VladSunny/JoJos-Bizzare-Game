using UnityEngine;
using JJBG.Attributes;

namespace JJBG.Combat
{
    public class Health : MonoBehaviour
    {
        [Header("Health")]
        [SerializeField] private float _maxHealth = 100f;

        [Header("Healing")]
        [SerializeField] private float _timeToStartHealing = 5f;
        [SerializeField] private float _healAmount = 10f;
        [SerializeField] private float _healRate = 1f;

        [SerializeField, ReadOnly] private float _health;
        private float _healTimer;

        private void Awake() {
            _health = _maxHealth;
        }

        private void Update() {
            if (_healTimer > 0)
                _healTimer -= Time.deltaTime;

            if (_healTimer <= 0 && _health < _maxHealth) {
                Heal(_healAmount);
                _healTimer = _healRate;
            }
        }

        public void TakeDamage(float damage) {
            _health = Mathf.Clamp(_health - damage, 0, _maxHealth);
            _healTimer = _timeToStartHealing;
        }

        public void Heal(float heal) {
            _health = Mathf.Clamp(_health + heal, 0, _maxHealth);
            _healTimer = _timeToStartHealing;
        }
    }
}
