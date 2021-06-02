using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using TMPro;

namespace CaveIn.UI
{
    public class LevelUI : MonoBehaviour
    {
        [SerializeField] GameObject DeathUI;
        [SerializeField] GameObject WinUI;
        [SerializeField] TextMeshProUGUI winGoldText;
        [SerializeField] GameObject HUD;


        [HideInInspector] public UnityEvent OnPause;

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
            WinUI.SetActive(true);
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
            HUD.SetActive(true);
        }
    }
}
