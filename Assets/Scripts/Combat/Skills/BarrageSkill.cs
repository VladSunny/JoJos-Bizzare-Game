using UnityEngine;
using Cysharp.Threading.Tasks;

using JJBG.Movement;

namespace JJBG.Combat
{
    public class BarrageSkill : MonoBehaviour, ISkill
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

        // [Header("Settings")]
        // [SerializeField] private float _damage = 10f;
        // [SerializeField] private float _knockback = 5f;
        // [SerializeField] private float _enemyStunDuration = 2f;
        // [SerializeField] private float _lunge = 5f;
        // [SerializeField] private bool _makeRagdoll = false;
        // [Tooltip("In milliseconds"), SerializeField] private int _punchDelay = 500;
        // [SerializeField] private float _attackTime = 1f;

        private string _basePunchClipName;

        private void Awake()
        {
            _basePunchClipName = gameObject.name;
        }

        public UniTask Attack()
        {
            _spMovement.SetAttackTimer(100f);

            _animator.SetBool("Barraging", true);

            return UniTask.CompletedTask;
        }

        public UniTask Stop()
        {
            _spMovement.SetAttackTimer(0f);

            _animator.SetBool("Barraging", false);

            return UniTask.CompletedTask;
        }
    }
}
