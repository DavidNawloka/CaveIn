using CaveIn.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CaveIn.Visuals
{
    public class DifficultyIndicator : MonoBehaviour
    {
        [SerializeField] Transform needle;
        [SerializeField] float maxRotation;

        GameDifficulty gameDifficulty;
        Vector3 initialRot;
        

        private void Awake()
        {
            initialRot = needle.localRotation.eulerAngles;
            print(initialRot);
            gameDifficulty = FindObjectOfType<GameDifficulty>();
            gameDifficulty.OnDifficultyUpdate.AddListener(UpdateNeedle);
        }

        private void UpdateNeedle(int currentDifficulty,int maxDifficulty)
        {
            Vector3 newRot = new Vector3(initialRot.x + (float)currentDifficulty / (float)maxDifficulty * maxRotation, initialRot.y, initialRot.z);
            needle.localRotation = Quaternion.Euler(newRot); // TODO: Smooth it
        }

        
    }
}
