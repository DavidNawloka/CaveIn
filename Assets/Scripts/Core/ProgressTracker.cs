using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

namespace CaveIn.Core
{
    public class ProgressTracker : MonoBehaviour
    {
        GameObject[] levelButtonsGameobject;
        int availableLevels;
        [HideInInspector] public Dictionary<int, int> levelProgress = new Dictionary<int, int>(); // -1 = not unlocked, 0 = unlocked, 1-12 gold
        private void Awake()
        {
            DontDestroyOnLoad(gameObject);
            ProgressTracker[] progressTrackers = FindObjectsOfType<ProgressTracker>();
            if (progressTrackers.Length > 1)
            {
                Destroy(gameObject);
            }
            availableLevels = SceneManager.sceneCountInBuildSettings;

            for (int i = 1; i < availableLevels; i++)
            {
                levelProgress.Add(i, -1);
            }
            levelProgress[1] = 0;
        }
        public void UpdateProgress(GameObject[] buttons)
        {
            levelButtonsGameobject = buttons;
            for (int i = 1; i < availableLevels; i++)
            {
                Button levelButton = levelButtonsGameobject[i - 1].GetComponent<Button>();
                TextMeshProUGUI text = levelButtonsGameobject[i - 1].GetComponentInChildren<TextMeshProUGUI>();


                int progress = levelProgress[i];
                switch (progress)
                {
                    case -1:
                        levelButton.interactable = false;
                        break;
                    case 0:
                        levelButton.interactable = true;
                        break;
                }
                text.text = Mathf.Clamp(levelProgress[i],0,12).ToString() + "/12";
            }
        }
    }
}
