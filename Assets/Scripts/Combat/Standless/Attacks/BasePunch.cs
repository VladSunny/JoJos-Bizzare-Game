using UnityEngine;

using JJBG.Core;

namespace JJBG.Combat.Standless.Attacks
{
    public class BasePunch : MonoBehaviour, IAttack
    {
        [Header("References")]
        [SerializeField] private Animator _animator;
        [SerializeField] private Transform _playerObj;
        [SerializeField] private AnimationClip _basePunchClip;

        [Header("Settings")]
        [SerializeField] private float _damage = 10f;
        [SerializeField] private float _knockback = 5f;
        [SerializeField] private float _lunge = 5f;

        private DynamicHitBox _hitBox;
        private Rigidbody _rb;

        private void Awake() {
            _hitBox = GetComponentInParent<DynamicHitBox>();
            _rb = GetComponentInParent<Rigidbody>();
        }

        public void Attack() {
            _animator.Play(_basePunchClip.name);

            _hitBox.CreateHitBox(Vector3.forward * 1f, new Vector3(1f, 1f, 1f), Hit, true);
        }

        private void Hit(Collider collider) {
            if (collider.GetComponentInChildren<DynamicHitBox>() == _hitBox)
                        return;
                    
            Debug.Log(collider);

            Health health = collider.GetComponentInChildren<Health>();
            Rigidbody rb = collider.GetComponentInChildren<Rigidbody>();

            if (health) health.TakeDamage(_damage);
            if (rb) rb.AddForce(_playerObj.forward * _knockback, ForceMode.Impulse);

            _rb.AddForce(_playerObj.forward * _lunge, ForceMode.Impulse);
        }
    }
}
