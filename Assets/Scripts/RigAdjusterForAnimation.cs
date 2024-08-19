using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JJBG
{
    public class BoneTransformData
    {
        public Vector3 position;
        public Quaternion rotation;
    }

    public class RigAdjusterForAnimation
    {
        [SerializeField] private const float _timeToShiftBones = 0.5f;

        private AnimationClip _animationClip;

        private MonoBehaviour _view;

        private List<Transform> _bones;
        private BoneTransformData[] _bonesBeforeAnimation;
        private BoneTransformData[] _bonesAtStartOfAnimation;

        private Coroutine _shiftBonesToStartOfAnimationCoroutine;

        public RigAdjusterForAnimation(AnimationClip animationClip, IEnumerable<Transform> bones, MonoBehaviour view) {
            _animationClip = animationClip;
            _view = view;
            _bones = new List<Transform>(bones);

            _bonesBeforeAnimation = new BoneTransformData[_bones.Count];
            _bonesAtStartOfAnimation = new BoneTransformData[_bones.Count];

            for (int i = 0; i < _bones.Count; i++) {
                _bonesBeforeAnimation[i] = new BoneTransformData();
                _bonesAtStartOfAnimation[i] = new BoneTransformData();
            }

        }

        public void Adjust(Action callback) {
            SaveCurrentBonesDataTo(_bonesBeforeAnimation);

            if (_shiftBonesToStartOfAnimationCoroutine != null) {
                _view.StopCoroutine(_shiftBonesToStartOfAnimationCoroutine);
            }

            _shiftBonesToStartOfAnimationCoroutine = _view.StartCoroutine(ShiftBonesToAnimation(callback));
        }

        private IEnumerator ShiftBonesToAnimation(Action callback) {
            float progress = 0;

            while (progress < _timeToShiftBones) {
                progress += Time.deltaTime;
                float t = progress / _timeToShiftBones;

                for (int i = 0; i < _bones.Count; i++) {
                    _bones[i].localPosition = Vector3.Lerp(_bonesBeforeAnimation[i].position, _bonesAtStartOfAnimation[i].position, t);
                    _bones[i].localRotation = Quaternion.Lerp(_bonesBeforeAnimation[i].rotation, _bonesAtStartOfAnimation[i].rotation, t);
                }

                yield return null;
            }

            callback?.Invoke();
        }

        private void SaveCurrentBonesDataTo(BoneTransformData[] bones) {
            for (int i = 0; i < _bones.Count; i++) {
                bones[i].position = _bones[i].localPosition;
                bones[i].rotation = _bones[i].localRotation;
            }
        }

        private void SaveBonesDataFromStartOfAnimation() {
            Vector3 initPosition = _view.transform.position;
            Quaternion initRotation = _view.transform.rotation;

            _animationClip.SampleAnimation(_view.gameObject, 0);

            SaveCurrentBonesDataTo(_bonesAtStartOfAnimation);
            
            _view.transform.position = initPosition;
            _view.transform.rotation = initRotation;
        }
    }
}
