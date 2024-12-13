using UnityEngine;

namespace JJBG.Audio
{
    public class BackgroundMusic : MonoBehaviour
    {
        [Header("Settings")]
        [SerializeField] private float _pauseTime = 5f;
        [SerializeField] private bool _globalMusicVolume = true;
        [SerializeField] private float _musicVolume = 1f;

        [Header("Debug")]
        [SerializeField] private KeyCode _nextSoundKey = KeyCode.Alpha4;

        private AudioManager _audioManager;
        private Sound[] _sounds;
        private int _currentSoundIndex = 0;

        private void Start() {
            _audioManager = GetComponent<AudioManager>();
            _sounds = _audioManager.sounds;

            for (int i = 0; i < _sounds.Length; i++) {
                _sounds[i].source.volume = _globalMusicVolume ? _musicVolume : _sounds[i].volume;
            }

            PlayNextSound();
        }

        private void Update() {
            if (Input.GetKeyDown(_nextSoundKey)) {
                CancelInvoke("PlayNextSound");

                PlayNextSound();
            }
        }

        private void PlayNextSound() {
            if (_sounds[_currentSoundIndex].source.isPlaying) {
                _sounds[_currentSoundIndex].source.Stop();
            }

            _currentSoundIndex = (_currentSoundIndex + 1) % _sounds.Length;
            _sounds[_currentSoundIndex].source.Play();

            Invoke("PlayNextSound", _sounds[_currentSoundIndex].clip.length + _pauseTime);
        }
    }
}
