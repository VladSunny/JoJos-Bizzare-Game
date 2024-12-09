using UnityEngine;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;

using JJBG.Movement;

namespace JJBG.Combat
{
    public class BasePunchSkill : MonoBehaviour, ISkill
    {
        [Header("Name of object should be name of clip in animator")]
        [Space(20)]

        [Header("Dependencies")]
        [SerializeField] private Animator _animator;
        [SerializeField] private Transform _playerObj;
        [SerializeField] private GameObject _player;
        [SerializeField] private DynamicHitBox _dynamicHitBox;
        [SerializeField] private Rigidbody _rb;
        [SerializeField] private SPMovement _spMovement;

        [Header("Settings")]
        [SerializeField] private float _damage = 10f;
        [SerializeField] private float _knockback = 5f;
        [SerializeField] private float _enemyStunDuration = 2f;
        [SerializeField] private float _lunge = 5f;
        [SerializeField] private bool _makeRagdoll = false;
        [Tooltip("In milliseconds"), SerializeField] private int _punchDelay = 500;
        [SerializeField] private float _attackTime = 1f;

        private string _basePunchClipName;

        private void Awake()
        {
            _basePunchClipName = gameObject.name;
        }

        public async UniTask Attack()
        {
            if (_spMovement != null)
                _spMovement.SetAttackTimer(_attackTime);

            _animator.CrossFade(_basePunchClipName, 0.5f);

            await Task.Delay(_punchDelay);

            _dynamicHitBox.CreateHitBox(Vector3.forward * 1f, new Vector3(1f, 1f, 1f), Hit, true);
        }

        private void Hit(Collider collider)
        {
            if (collider.gameObject == _player)
                return;

            Health health = collider.GetComponentInChildren<Health>();
            StunManager stunManager = collider.GetComponentInChildren<StunManager>();

            if (health) health.TakeDamage(_damage);
            if (stunManager) stunManager.SetStun(_enemyStunDuration);

            if (_makeRagdoll)
            {
                RagdollHandler ragdollHandler = collider.GetComponentInChildren<RagdollHandler>();
                Animator animator = collider.GetComponentInChildren<Animator>();
                Rigidbody hipsRigidbody = animator.GetBoneTransform(HumanBodyBones.Hips).GetComponent<Rigidbody>();

                if (ragdollHandler) ragdollHandler.Enable();
                if (hipsRigidbody) hipsRigidbody.AddForce(_playerObj.forward * _knockback, ForceMode.Impulse);
            }
            else
            {
                Rigidbody rb = collider.GetComponentInChildren<Rigidbody>();

                if (rb) rb.AddForce(_playerObj.forward * _knockback, ForceMode.Impulse);
            }

            if (_rb)
                _rb.AddForce(_playerObj.forward * _lunge, ForceMode.Impulse);
        }

        public UniTask Stop()
        {
            return UniTask.CompletedTask;
        }
    }
}
