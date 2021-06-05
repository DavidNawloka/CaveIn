using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace CaveIn.Core
{
    public class LevelLoader : MonoBehaviour
    {

        public void LoadScene(int sceneIndex)
        {
            StartCoroutine(LoadWithFade(sceneIndex));
        }
        public void ReloadCurrentScene()
        {
            StartCoroutine(LoadWithFade(SceneManager.GetActiveScene().buildIndex));
        }
        public void LoadNextLevel()
        {
            StartCoroutine(LoadWithFade(SceneManager.GetActiveScene().buildIndex+1));
        }

        private IEnumerator LoadWithFade(int sceneIndex)
        {
            GameObject backGroundMusic = GameObject.FindGameObjectWithTag("Background");
            if(backGroundMusic != null)
            {
                StartCoroutine(MuteAudioSource(backGroundMusic.GetComponent<AudioSource>()));
            }
            GetComponent<Animator>().SetTrigger("fadeOut");
            yield return new WaitForSecondsRealtime(1f);
            SceneManager.LoadScene(sceneIndex);
        }
        private IEnumerator MuteAudioSource(AudioSource audioSource)
        {
            float timer = 0;
            float startingVolume = audioSource.volume;
            while (timer <= .5f)
            {
                audioSource.volume = startingVolume - timer / .5f;
                timer += Time.deltaTime;
                yield return null;
            }
            audioSource.volume = 0;
        }
    }
}
