using UnityEngine;

using JJBG.Movement;
using JJBG.Attributes;
using JJBG.Core;

namespace JJBG.Combat.Skills
{
    public class SPBarrage : SkillBase
    {
        [Header("Name of object should be name of clip in animator")]

        [Space(20)]

        [Header("Settings")]
        [SerializeField] private float _damagePerPunch = 10f;
        [SerializeField] private int _punchesCount = 50;
        [SerializeField] private float _barrageDuration = 5f;
        [SerializeField] private float _lungePerPunch = 2f;
        [SerializeField] private float _knockbackPerPunch = 2f;
        [SerializeField] private float _enemyStun = 0.5f;
        [SerializeField] private float _delayDuration = 0.8f;

        [Header("References")]
        [SerializeField] private Transform _obj;
        [SerializeField] private Animator _animator;
        [SerializeField] private StarPlatinumMovement _starPlatinumMovement;

        [Header("Timers")]
        [SerializeField, ReadOnly] private float _barrageTimer = 0f;
        [SerializeField, ReadOnly] private float _punchTimer = 0f;

        private bool _barraging = false;
        private float _timePerPunch;

        private CombatState _combatState;
        private DynamicHitBox _hitBox;

        private void Awake()
        {
            _hitBox = GetComponentInParent<DynamicHitBox>();

            _timePerPunch = _barrageDuration / (float)_punchesCount;
        }

        public override void Initialize(CombatState combatState)
        {
            _combatState = combatState;
        }

        public override void Activate()
        {
            _barrageTimer = _barrageDuration;
            _barraging = true;
            _starPlatinumMovement.attackingTimer = _barrageDuration;
            _combatState.SetStun(_barrageDuration);
            _animator.SetBool("Barraging", true);
        }

        public void Deactivate()
        {
            if (!_barraging) return;

            _barraging = false;
            _barrageTimer = 0f;
            _combatState.SetStun(0f);
            _starPlatinumMovement.attackingTimer = _delayDuration;
            _animator.SetBool("Barraging", false);
        }

        private void Update()
        {
            if (_barrageTimer > 0)
            {
                _barrageTimer -= Time.deltaTime;
                _punchTimer += Time.deltaTime;
            }
            else if (_barraging)
            {
                Deactivate();
            }

            if (_punchTimer >= _timePerPunch)
            {
                _punchTimer = 0f;
                _hitBox.CreateHitBox(Vector3.forward * 1f, new Vector3(1f, 1f, 1f), Hit, true);
            }
        }

        private void Hit(Collider collider)
        {
            if (collider.GetComponentInChildren<DynamicHitBox>() == _hitBox)
                return;

            Health health = collider.GetComponentInChildren<Health>();
            CombatState combatState = collider.GetComponentInChildren<CombatState>();

            if (health) health.TakeDamage(_damagePerPunch);
            if (combatState) combatState.SetStun(_enemyStun);

            Rigidbody rb = collider.GetComponentInChildren<Rigidbody>();

            if (rb) rb.AddForce(_obj.forward * _knockbackPerPunch, ForceMode.Impulse);
        }
    }
}
