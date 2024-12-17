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

        [Header("AudioSettings")]
        [SerializeField] private string _mediumPunchClipPref = "medPunch_";
        [SerializeField] private string _hardPunchClipPref = "hardPunch_";
        [SerializeField] private int _mediumPunchSoundsCount = 3;
        [SerializeField] private int _hardPunchSoundsCount = 3;

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

            if (_audioManager != null) {
                if (hitInfo.soundType == HitInfo.SoundType.MediumPunch)
                    _audioManager.Play(_mediumPunchClipPref + Random.Range(1, _mediumPunchSoundsCount + 1));
                else if (hitInfo.soundType == HitInfo.SoundType.HardPunch)
                    _audioManager.Play(_hardPunchClipPref + Random.Range(1, _hardPunchSoundsCount + 1));
            }
        }
    }
}
