using UnityEngine;
using Cysharp.Threading.Tasks;

using JJBG.Movement;
using JJBG.Attributes;
using JJBG.Audio;

namespace JJBG.Combat
{
    public class BarrageSkill : MonoBehaviour, ISkill
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


        [Header("Clip Name")]
        [SerializeField] private string _barrageAnimationClipName = "Barraging";

        [Header("Settings")]
        [SerializeField] private float _damage = 10f;
        [SerializeField] private float _knockback = 5f;
        [SerializeField] private float _enemyStunDuration = 2f;
        [SerializeField] private float _lunge = 5f;
        [SerializeField] private float _secondsBetweenAttacks = 0.5f;
        [SerializeField] private float _timeForContinueCombo = 0.5f;

        [Header("Effects")]
        [SerializeField] private ParticleSystem _barrageEffect;

        [Header("Timers")]
        [SerializeField, ReadOnly] private float _attackTimer = 0f;

        private bool _barraging = false;
        private string _currentAudio = "";

        private void Update() {
            if (_barraging) {
                if (_attackTimer > 0) {
                    _attackTimer -= Time.deltaTime;
                } else {
                    _attackTimer = _secondsBetweenAttacks;
                    _dynamicHitBox.CreateHitBox(Vector3.forward * 1f, new Vector3(1f, 1f, 1f), Hit, true);
                }
            }
            else
                _attackTimer = 0f;
        }

        public UniTask Attack()
        {
            _spMovement.SetAttackTimer(100f);

            _animator.SetBool(_barrageAnimationClipName, true);

            _barraging = true;

            _barrageEffect.Play();

            _currentAudio = _audioManager.PlayRandom();

            return UniTask.CompletedTask;
        }

        public UniTask Stop()
        {
            _spMovement.SetAttackTimer(_timeForContinueCombo);

            _animator.SetBool(_barrageAnimationClipName, false);

            _barraging = false;

            _barrageEffect.Stop();

            _audioManager.Stop(_currentAudio);

            return UniTask.CompletedTask;
        }

        private void Hit(Collider collider)
        {
            if (collider.gameObject == _player)
                return;

            Health health = collider.GetComponentInChildren<Health>();
            StunManager stunManager = collider.GetComponentInChildren<StunManager>();

            if (health) health.TakeDamage(_damage);
            if (stunManager) stunManager.SetStun(_enemyStunDuration);

            Rigidbody rb = collider.GetComponentInChildren<Rigidbody>();

            if (rb) rb.AddForce(_playerObj.forward * _knockback, ForceMode.Impulse);

            if (_rb)
                _rb.AddForce(_playerObj.forward * _lunge, ForceMode.Impulse);
        }
    }
}
