using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Animations;
using DG.Tweening;
using Cysharp.Threading.Tasks;
using TMPro;

namespace JJBG.UI
{
    public class Cooldown : MonoBehaviour
    {
        [Header("Settings")]
        [SerializeField] private float _appearDuration = 0.5f;
        [SerializeField] private float _disappearDuration = 0.5f;

        private Slider _slider;
        private float _duration;

        public void Initilize(float duration) {
            _duration = duration;

            _slider = GetComponent<Slider>();

            _slider.maxValue = _duration;
            _slider.value = _duration;

            Animate().Forget();
        }

        private void Update() {
            if (_slider == null) return;
            _slider.value -= Time.deltaTime;
        }

        private async UniTaskVoid Animate()
        {
            transform.localScale = Vector3.zero;

            await transform.DOScale(Vector3.one, _appearDuration).SetEase(Ease.OutBack).AsyncWaitForCompletion();

            await UniTask.Delay((int)(_duration * 1000));

            await transform.DOScale(Vector3.zero, _disappearDuration).SetEase(Ease.InBack).AsyncWaitForCompletion();

            Destroy(gameObject);
        }
    }
}
