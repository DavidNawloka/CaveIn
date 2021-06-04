using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using TMPro;
using CaveIn.Core;
using UnityEngine.SceneManagement;

namespace CaveIn.UI
{
    public class LevelUI : MonoBehaviour
    {
        [SerializeField] GameObject DeathUI;
        [SerializeField] GameObject WinUI;
        [SerializeField] GameObject TutorialUI;
        [SerializeField] TextMeshProUGUI winGoldText;
        [SerializeField] GameObject HUD;


        [HideInInspector] public UnityEvent OnPause;
        [HideInInspector] public UnityEvent OnUnpause;

        private void Awake()
        {
            Time.timeScale = 1;
        }
        private void Start()
        {
            if(SceneManager.GetActiveScene().buildIndex == 1 && FindObjectOfType<ProgressTracker>().levelProgress[1] == 0)
            {
                Pause();
            }
            else
            {
                DisableAnythingElse();
            }
        }
        public void Death()
        {
            DisableHud();
            DeathUI.SetActive(true);
        }
        public void Win(int goldAmount, int maxGold)
        {
            DisableHud();
            winGoldText.text = goldAmount + "/" + maxGold;

            ProgressTracker progressTracker = FindObjectOfType<ProgressTracker>();

            if(progressTracker.levelProgress[SceneManager.GetActiveScene().buildIndex] < goldAmount)
            {
                progressTracker.levelProgress[SceneManager.GetActiveScene().buildIndex] = goldAmount;
            }
            if(goldAmount >= 6)
            {
                progressTracker.levelProgress[(SceneManager.GetActiveScene().buildIndex + 1) % SceneManager.sceneCountInBuildSettings] = 0;
            }
            WinUI.SetActive(true);
        }

        public void Unpause()
        {
            Time.timeScale = 1;
            OnUnpause.Invoke();
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }

        private void DisableHud()
        {
            HUD.SetActive(false);
            Pause();
        }

        private void Pause()
        {
            Time.timeScale = 0;
            OnPause.Invoke();
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }

        private void DisableAnythingElse()
        {
            WinUI.SetActive(false);
            DeathUI.SetActive(false);
            if(TutorialUI != null) TutorialUI.SetActive(false);
            HUD.SetActive(true);
        }
    }
}
