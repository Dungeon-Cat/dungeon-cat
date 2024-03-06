using System;
using System.Collections;
using Scripts.Data;
using UnityEngine;
using Random = System.Random;

namespace Scripts.Components.UI
{
    public class AudioManager : ComponentWithData<AudioData>
    {
        public void PlayClip(AudioClip clip, Transform spawnTransform, float volume = 1f)
        {
            AudioSource audioSource = Instantiate(data.audioSource, spawnTransform.position, Quaternion.identity);
            audioSource.clip = clip;
            audioSource.volume = volume;
            audioSource.Play();
            float clipLength = audioSource.clip.length;
            Destroy(audioSource.gameObject, clipLength);
        }

        public void SetVolume(float volume = 1f)
        {
            if (data.audioSource != null)
            {
                data.audioSource.volume = volume;
            }
        }
    }
    public class SoundFXManager : AudioManager
    {
        public static SoundFXManager Instance { get; private set; }

        public void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
        }
    }

    public class MusicManager : AudioManager
    {
        public static MusicManager Instance { get; private set; }

        public void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
        }

        public void StopMusic()
        {
            
        }

        public IEnumerator BeginLevelMusic(AudioClip[] clips, Transform spawnTransform, float volume = 1f)
        {
            AudioSource audioSource = Instantiate(data.audioSource, spawnTransform.position, Quaternion.identity);

            Random rand = new Random();
            while (true)
            {
                int random = rand.Next(0, clips.Length - 1);
                audioSource.clip = clips[random];
                audioSource.volume = volume;
                audioSource.Play();
                float clipLength = audioSource.clip.length;
                yield return new WaitForSeconds(clipLength);
            }
            Destroy(audioSource.gameObject);
        }
    }
}