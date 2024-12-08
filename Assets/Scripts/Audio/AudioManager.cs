using UnityEngine;
using UnityEngine.Audio;
using System;

namespace JJBG.Audio
{
    public class AudioManager : MonoBehaviour
    {
        public Sound[] sounds;

        void Awake()
        {
            foreach (Sound s in sounds)
            {
                s.source = gameObject.AddComponent<AudioSource>();
                s.source.clip = s.clip;

                s.source.volume = s.volume;
                s.source.pitch = s.pitch;
                
                if (s.playOnAwake)
                    s.source.Play();
            }
        }

        public void Play(string name)
        {
            Array.Find(sounds, sound => sound.name == name).source.Play();
        }
    }
}
