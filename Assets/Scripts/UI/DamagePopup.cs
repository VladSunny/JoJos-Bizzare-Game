using TMPro;
using UnityEngine;
using UnityEngine.Animations;
using DG.Tweening;
using Cysharp.Threading.Tasks;

namespace JJBG.UI
{
    public class DamagePopup : MonoBehaviour
    {
        [Header("Settings")]
        [SerializeField] private Vector3 _offset = new Vector3(0f, 1f, 0f);
        [SerializeField] private Vector3 _maxRandomOffset = new Vector3(1f, 1f, 1f);

        [Header("Animation")]
        public float _appearDuration = 0.5f;
        public float _visibleDuration = 2f;
        public float _disappearDuration = 0.5f;

        private LookAtConstraint _lookAtConstraint;
        private TextMeshProUGUI _textMeshPro;

        private void Awake() {
            _lookAtConstraint = GetComponent<LookAtConstraint>();
            _textMeshPro = GetComponentInChildren<TextMeshProUGUI>();

            _lookAtConstraint.AddSource(new ConstraintSource {
                sourceTransform = Camera.main.transform,
                weight = 1f
            });

            Vector3 offset = new Vector3(
                Random.Range(-_maxRandomOffset.x, _maxRandomOffset.x),
                Random.Range(-_maxRandomOffset.y, _maxRandomOffset.y),
                Random.Range(-_maxRandomOffset.z, _maxRandomOffset.z)
            );

            transform.position += offset + _offset;

            Animate().Forget();
        }

        public void Initilize(float damage) {
            _textMeshPro.text = "-" + damage.ToString();
        }

        private async UniTaskVoid Animate()
        {
            transform.localScale = Vector3.zero;

            await transform.DOScale(Vector3.one, _appearDuration).SetEase(Ease.OutBack).AsyncWaitForCompletion();

            await UniTask.Delay((int)(_visibleDuration * 1000));

            await transform.DOScale(Vector3.zero, _disappearDuration).SetEase(Ease.InBack).AsyncWaitForCompletion();

            Destroy(gameObject);
        }
    }
}
