using CaveIn.Spawning;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CaveIn.Core
{
    public class GameDifficulty : MonoBehaviour
    {
        [SerializeField] DifficultyLevel[] difficultyLevels;
        [SerializeField] float difficultyIncreaseTime = 10f;

        RockSpawner rockSpawner;

        float timer = 0;
        int currentDifficultyIndex = 0;
        private void Awake()
        {
            rockSpawner = FindObjectOfType<RockSpawner>();
            UpdateDifficulty();
        }
        private void Update()
        {
            if(timer >= difficultyIncreaseTime)
            {
                currentDifficultyIndex += 1;
                UpdateDifficulty();
                timer = 0;
            }
            timer += Time.deltaTime;
        }

        private void UpdateDifficulty()
        {
            if (currentDifficultyIndex >= difficultyLevels.Length || currentDifficultyIndex < 0) return;

            print(currentDifficultyIndex);

            DifficultyLevel currentLevel = difficultyLevels[currentDifficultyIndex];
            rockSpawner.UpdateSpawnValues(currentLevel.timeBetweenRockSpawn, currentLevel.minRocksPerSpawn, currentLevel.maxRocksPerSpawn);
        }
        public void IncreaseDifficulty(int amount)
        {
            currentDifficultyIndex += amount;
            UpdateDifficulty();
        }

        [Serializable]
        private class DifficultyLevel
        {
            public float timeBetweenRockSpawn;
            public int minRocksPerSpawn;
            public int maxRocksPerSpawn;
        }
    }
}
