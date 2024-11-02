using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;

namespace JJBG
{
    public class RagdollHandler : MonoBehaviour
    {
        public delegate void OnRagdollChanged(bool isRagdoll);
        public OnRagdollChanged onRagdollChanged;

        [Header("Dependencies")]
        [SerializeField] private Transform _parent;
        [SerializeField] private LayerMask _groundLayerMask;

        [Header("Debug")]
        [SerializeField] private bool _debug = false;

        private List<Rigidbody> _rigidbodies;
        private List<Collider> _colliders;

        private Animator _animator;
        private Transform _hipsTransform;
        
        private bool _enabled = false;

        public bool IsRagdoll => _enabled;

        private void Awake()
        {
            _rigidbodies = new List<Rigidbody>(GetComponentsInChildren<Rigidbody>());
            _colliders = new List<Collider>(GetComponentsInChildren<Collider>());
            _animator = GetComponent<Animator>();
            
            _hipsTransform = _animator.GetBoneTransform(HumanBodyBones.Hips);
        }

        private void Start() {
            Disable();
        }

        private void Update() {
            if (!_debug) return;

            if (Input.GetKeyDown(KeyCode.R)) {
                if (_enabled)
                    Disable();
                else
                    Enable();
            }
        }

        public async void Disable() {
            _enabled = false;

            foreach (var rb in _rigidbodies)
                rb.isKinematic = true;

            foreach (var col in _colliders)
                col.enabled = false;
            
            _animator.enabled = true;

            await AdjustParentPositionToHipsAsync();
            // AdjustParentRotationToHips();

            _parent.GetComponent<Collider>().enabled = true;
            _parent.GetComponent<Rigidbody>().isKinematic = false;

            onRagdollChanged?.Invoke(false);
        }

        public void Enable() {
            _parent.GetComponent<Rigidbody>().isKinematic = true;
            _parent.GetComponent<Collider>().enabled = false;

            _enabled = true;
            
            _animator.Rebind();
            _animator.enabled = false;

            foreach (var col in _colliders)
                col.enabled = true;

            foreach (var rb in _rigidbodies)
                rb.isKinematic = false;

            onRagdollChanged?.Invoke(true);
        }

        private async UniTask AdjustParentPositionToHipsAsync() {
            Vector3 initHipsPosition = _hipsTransform.position;
            _parent.position = initHipsPosition;

            await AdjustParentPositionRelativeGroundAsync();

            _hipsTransform.position = initHipsPosition;
        }

        private async UniTask AdjustParentPositionRelativeGroundAsync() {
            if (Physics.Raycast(_parent.position, Vector3.down, out var hit, 5, _groundLayerMask)) {
                _parent.position = new Vector3(_parent.position.x, hit.point.y, _parent.position.z);
            }
            await UniTask.Yield(PlayerLoopTiming.LastFixedUpdate);
        }

        // private void AdjustParentRotationToHips() {
        //     Vector3 initHipsPosition = _hipsTransform.position;
        //     Quaternion initHipsRotation = _hipsTransform.rotation;

        //     Vector3 directionForRotate = -_hipsTransform.up;

        //     directionForRotate.y = 0;

        //     Quaternion correctionRotation = Quaternion.FromToRotation(_parent.forward, directionForRotate.normalized);
        //     _parent.rotation = initHipsRotation * correctionRotation;

        //     _hipsTransform.position = initHipsPosition;
        //     _hipsTransform.rotation = initHipsRotation;
        // }
    }
}
