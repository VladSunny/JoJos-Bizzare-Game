using TMPro;
using UnityEngine;
using UnityEngine.Animations;

namespace JJBG.UI
{
    public class DamagePopup : MonoBehaviour
    {
        [Header("Settings")]
        [SerializeField] private float _duration = 1.5f;

        private LookAtConstraint _lookAtConstraint;
        private TextMeshProUGUI _textMeshPro;

        private void Awake() {
            _lookAtConstraint = GetComponent<LookAtConstraint>();
            _textMeshPro = GetComponentInChildren<TextMeshProUGUI>();

            _lookAtConstraint.AddSource(new ConstraintSource {
                sourceTransform = Camera.main.transform,
                weight = 1f
            });

            Destroy(gameObject, _duration);
        }

        public void Initilize(float damage) {
            _textMeshPro.text = "-" + damage.ToString();
        }
    }
}
