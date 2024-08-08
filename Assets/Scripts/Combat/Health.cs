using UnityEngine;
using JJBG.Attributes;

namespace JJBG.Combat
{
    public class Health : MonoBehaviour
    {
        public delegate void OnHealthChanged(float health);
        public OnHealthChanged onHealthChanged;

        [SerializeField, ReadOnly] private float _health;

        [Header("Health")]
        [SerializeField] private float _maxHealth = 100f;

        [Header("Healing")]
        [SerializeField] private float _timeToStartHealing = 5f;
        [SerializeField] private float _healAmount = 10f;
        [SerializeField] private float _healRate = 1f;

        [Header("Debug")]
        [SerializeField] private bool _debug = true;
        [SerializeField] private KeyCode _damageKey = KeyCode.Alpha1;
        [SerializeField] private KeyCode _healKey = KeyCode.Alpha2;

        private float _healTimer;

        private void Awake() {
            _health = _maxHealth;
        }

        private void Start() {
            onHealthChanged?.Invoke(_health);
        }

        private void Update() {
            if (_healTimer > 0)
                _healTimer -= Time.deltaTime;

            if (_healTimer <= 0 && _health < _maxHealth) {
                Heal(_healAmount);
                _healTimer = _healRate;
            }

            if (_debug) {
                if (Input.GetKeyDown(_damageKey))
                    TakeDamage(10f);

                if (Input.GetKeyDown(_healKey))
                    Heal(10f);
            }
        }

        public float GetHealth() { return _health; }
        public float GetMaxHealth() { return _maxHealth; }

        public void TakeDamage(float damage) {
            _health = Mathf.Clamp(_health - damage, 0, _maxHealth);
            _healTimer = _timeToStartHealing;

            onHealthChanged?.Invoke(_health);
        }

        public void Heal(float heal) {
            _health = Mathf.Clamp(_health + heal, 0, _maxHealth);
            _healTimer = _timeToStartHealing;

            onHealthChanged?.Invoke(_health);
        }
    }
}
