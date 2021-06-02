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
            GetComponent<Animator>().SetTrigger("fadeOut");
            yield return new WaitForSecondsRealtime(1f);
            SceneManager.LoadScene(sceneIndex);
        }
    }
}
