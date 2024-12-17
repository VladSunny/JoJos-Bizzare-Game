using JJBG.Audio;
using UnityEngine;

namespace JJBG.Combat
{
    public class HitHandler : MonoBehaviour
    {
        [Header("Dependencies")]
        [SerializeField] private RagdollHandler _ragdollHandler;
        [SerializeField] private Rigidbody _rb;
        [SerializeField] private Animator _animator;

        private Health _health;
        private AudioManager _audioManager;
        private StunManager _stunManager;
        private Rigidbody _hipsRigidbody;

        private void Awake() {
            _health = GetComponent<Health>();
            _audioManager = GetComponent<AudioManager>();
            _stunManager = GetComponent<StunManager>();

            _hipsRigidbody = _animator.GetBoneTransform(HumanBodyBones.Hips).GetComponent<Rigidbody>();
        }

        public void GetHit(HitInfo hitInfo) {
            _health.TakeDamage(hitInfo.damage);
            _stunManager.SetStun(hitInfo.stunDuration);

            if (hitInfo.isRagdoll) {   
                _ragdollHandler.Enable();
                _hipsRigidbody.AddForce(hitInfo.force, ForceMode.Impulse);
            }
            else {
                _rb.AddForce(hitInfo.force, ForceMode.Impulse);
            }
        }
    }
}
