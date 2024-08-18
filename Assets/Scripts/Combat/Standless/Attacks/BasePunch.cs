using UnityEngine;
using System.Threading;

using JJBG.Core;
using System.Threading.Tasks;

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
        [SerializeField] private float _enemyStunDuration = 2f;
        [SerializeField] private float _lunge = 5f;
        [SerializeField] private bool _makeRagdoll = false;
        [Tooltip("In milliseconds"), SerializeField] private int _punchDelay = 500;

        private DynamicHitBox _hitBox;
        private Rigidbody _rb;

        private void Awake() {
            _hitBox = GetComponentInParent<DynamicHitBox>();
            _rb = GetComponentInParent<Rigidbody>();
        }

        public async void Attack() {
            _animator.Play(_basePunchClip.name);

            await Task.Delay(_punchDelay);

            _hitBox.CreateHitBox(Vector3.forward * 1f, new Vector3(1f, 1f, 1f), Hit, true);
        }

        private void Hit(Collider collider) {
            if (collider.GetComponentInChildren<DynamicHitBox>() == _hitBox)
                        return;
                    
            Debug.Log(collider);

            Health health = collider.GetComponentInChildren<Health>();
            CombatState combatState = collider.GetComponentInChildren<CombatState>();

            if (health) health.TakeDamage(_damage);
            if (combatState) combatState.SetStun(_enemyStunDuration);

            if (_makeRagdoll) {
                RagdollHandler ragdollHandler = collider.GetComponentInChildren<RagdollHandler>();
                Animator animator = collider.GetComponentInChildren<Animator>();
                Rigidbody hipsRigidbody = animator.GetBoneTransform(HumanBodyBones.Hips).GetComponent<Rigidbody>();

                if (ragdollHandler) ragdollHandler.Enable();
                if (hipsRigidbody) hipsRigidbody.AddForce(_playerObj.forward * _knockback, ForceMode.Impulse);
            }
            else {
                Rigidbody rb = collider.GetComponentInChildren<Rigidbody>();

                if (rb) rb.AddForce(_playerObj.forward * _knockback, ForceMode.Impulse);
            }

            _rb.AddForce(_playerObj.forward * _lunge, ForceMode.Impulse);
        }
    }
}
