using UnityEngine;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;

using JJBG.Movement;
using JJBG.Audio;

namespace JJBG.Combat
{
    public class BasePunchSkill : MonoBehaviour, ISkill
    {
        // [Header("Name of object should be name of clip in animator")]
        // [Space(20)]

        [Header("Dependencies")]
        [SerializeField] private Animator _animator;
        [SerializeField] private Transform _playerObj;
        [SerializeField] private GameObject _player;
        [SerializeField] private DynamicHitBox _dynamicHitBox;
        [SerializeField] private Rigidbody _rb;
        [SerializeField] private SPMovement _spMovement;
        [SerializeField] private AudioManager _audioManager;

        [Header("Settings")]
        [SerializeField] private float _damage = 10f;
        [SerializeField] private float _knockback = 5f;
        [SerializeField] private float _enemyStunDuration = 2f;
        [SerializeField] private float _lunge = 5f;
        [SerializeField] private bool _makeRagdoll = false;
        [SerializeField] private HitInfo.SoundType _soundType = HitInfo.SoundType.MediumPunch;
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

            if (_audioManager != null)
                _audioManager.PlayRandom();

            _animator.CrossFade(_basePunchClipName, 0.5f);

            await Task.Delay(_punchDelay);

            _dynamicHitBox.CreateHitBox(Vector3.forward * 1f, new Vector3(1f, 1f, 1f), Hit, true);
        }

        private void Hit(Collider collider)
        {
            if (collider.gameObject == _player)
                return;
                
            HitHandler hitHandler = collider.GetComponentInChildren<HitHandler>();

            HitInfo hitInfo = new HitInfo(
                _damage,
                _playerObj.forward * _knockback,
                _player,
                _makeRagdoll,
                _enemyStunDuration,
                _soundType
            );

            if (hitHandler) hitHandler.GetHit(hitInfo);

            if (_rb)
                _rb.AddForce(_playerObj.forward * _lunge, ForceMode.Impulse);
        }

        public UniTask Stop()
        {
            return UniTask.CompletedTask;
        }
    }
}
