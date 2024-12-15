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
                s.source.spatialBlend = s.spatialBlend;
                
                if (s.playOnAwake)
                    s.source.Play();
            }
        }

        public void Play(string name)
        {
            Array.Find(sounds, sound => sound.name == name).source.Play();
        }

        public void Stop(string name)
        {
            Array.Find(sounds, sound => sound.name == name).source.Stop();
        }

        public string PlayRandom() {
            string name = sounds[UnityEngine.Random.Range(0, sounds.Length)].name;
            Play(name);
            return name;
        }
    }
}
