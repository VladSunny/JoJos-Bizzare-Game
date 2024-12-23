using UnityEngine;
using UnityEngine.Audio;

namespace JJBG.Audio
{
    [System.Serializable]
    public class Sound
    {
        public string name;

        public AudioClip clip;

        [Range(0f, 1f)]
        public float volume = 1f;
        [Range(0.1f, 3f)]
        public float pitch;
        [Range(0f, 1f)]
        public float spatialBlend = 0f;

        public bool playOnAwake = false;

        [HideInInspector] public AudioSource source;
    }
}
