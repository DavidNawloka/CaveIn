using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using TMPro;
using CaveIn.Core;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace CaveIn.UI
{
    public class LevelUI : MonoBehaviour
    {
        [SerializeField] GameObject DeathUI;
        [SerializeField] GameObject WinUI;
        [SerializeField] TextMeshProUGUI winGoldText;
        [SerializeField] Button nextLevelButton;
        [SerializeField] GameObject HUD;


        [HideInInspector] public UnityEvent OnPause;
        [HideInInspector] public UnityEvent OnUnpause;

        private void Awake()
        {
            Time.timeScale = 1;
        }
        private void Start()
        {
            DisableAnythingElse();
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
            else if(nextLevelButton != null)
            {
                nextLevelButton.interactable = false;
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
            OnPause.Invoke();
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            Time.timeScale = 0;
        }

        private void DisableAnythingElse()
        {
            WinUI.SetActive(false);
            DeathUI.SetActive(false);
            HUD.SetActive(true);
        }
    }
}
