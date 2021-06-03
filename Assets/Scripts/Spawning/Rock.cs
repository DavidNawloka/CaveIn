using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CaveIn.Spawning
{
    public class Rock : MonoBehaviour
    {
        [SerializeField] RockSize rockSize;
        [SerializeField] float timeAfterHit = .5f;
        [SerializeField] float maxDamage;
        [SerializeField] float minDamage;
        [SerializeField] GameObject startDeathParticles;
        [SerializeField] AudioClip[] spawnSounds;
        [SerializeField] AudioClip[] hitSounds;

        AudioSource audioSource;
        bool canHurt = true;

        private void Awake()
        {
            audioSource = GetComponent<AudioSource>();
        }
        private void OnEnable()
        {
            if (startDeathParticles != null) SpawnParticles();
            if (spawnSounds.Length != 0) PlaySound(spawnSounds);
            
        }

        public bool CanHurt()
        {
            return canHurt; 
        }

        public RockSize GetRockSize()
        {
            return rockSize;
        }
        public float GetDamage()
        {
            return UnityEngine.Random.Range(minDamage, maxDamage);
        }
        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.CompareTag("Ground"))
            {
                if (hitSounds.Length != 0) PlaySound(hitSounds);
                canHurt = false;
                StartCoroutine(MuteAudioSource());
                Destroy(gameObject, timeAfterHit);
            }
                
        }

        private IEnumerator MuteAudioSource()
        {
            float timer = 0;
            while (timer <= timeAfterHit)
            {
                audioSource.volume = 1-timer / timeAfterHit;
                timer += Time.deltaTime;
                yield return null;
            }
            audioSource.volume = 0;
        }

        private void SpawnParticles()
        {
            GameObject particles = Instantiate(startDeathParticles, transform.position, Quaternion.identity);
            Destroy(particles, 1.5f);
        }
        private void PlaySound(AudioClip[] audioArray)
        {
            int randNum = UnityEngine.Random.Range(0, audioArray.Length);
            audioSource.PlayOneShot(audioArray[randNum]);
        }
    }
}
