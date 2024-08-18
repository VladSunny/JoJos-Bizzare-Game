using System.Collections.Generic;
using UnityEngine;

namespace JJBG
{
    public class RagdollHandler : MonoBehaviour
    {
        private List<Rigidbody> _rigidbodies;
        private List<Collider> _colliders;

        private Animator _animator;

        private void Awake()
        {
            _rigidbodies = new List<Rigidbody>(GetComponentsInChildren<Rigidbody>());
            _colliders = new List<Collider>(GetComponentsInChildren<Collider>());
            _animator = GetComponent<Animator>();

            Disable();
        }

        public void Disable() {
            foreach (var rb in _rigidbodies)
                rb.isKinematic = true;

            foreach (var col in _colliders)
                col.enabled = false;
            
            _animator.enabled = true;
        }

        public void Enable() {
            _animator.enabled = false;

            foreach (var col in _colliders)
                col.enabled = true;

            foreach (var rb in _rigidbodies)
                rb.isKinematic = false;
        }
    }
}
