using UnityEngine;

using JJBG.Combat;
using TMPro;

namespace JJBG.UI
{
    public class DamagePopupsManager : MonoBehaviour
    {
        [Header("Dependencies")]
        [SerializeField] GameObject damagePopupPrefab;

        private Health _health;

        private void Awake() {
            _health = GetComponent<Health>();
        }

        private void OnEnable() {
            _health.onDamaged += onDamaged;
        }

        private void OnDisable() {
            _health.onDamaged -= onDamaged;
        }

        private void onDamaged(float damage) {
            GameObject damagePopup = Instantiate(damagePopupPrefab, transform.position + Vector3.up, Quaternion.identity);
            damagePopup.GetComponent<DamagePopup>().Initilize(damage);
        }
    }
}
