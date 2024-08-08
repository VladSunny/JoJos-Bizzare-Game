using UnityEngine;
using UnityEngine.UI;

using JJBG.Combat;
using DG.Tweening;

namespace JJBG.UI
{
    public class HealthBar : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private Health _health;

        [Header("Settings")]
        [SerializeField] private float _sliderSpeed = 0.5f;

        private Slider _slider;
        private float _maxHealth;

        private void Awake() {
            _slider = GetComponent<Slider>();

            _maxHealth = _health.GetMaxHealth();

            _slider.minValue = 0;
            _slider.maxValue = _maxHealth;
        }

        private void OnEnable() {
            _health.onHealthChanged += OnHealthChanged;
        }

        private void OnDisable() {
            _health.onHealthChanged -= OnHealthChanged;
        }

        private void OnHealthChanged(float health) {
            _slider.DOValue(health, _sliderSpeed).SetEase(Ease.OutCubic);
        }
    }
}
