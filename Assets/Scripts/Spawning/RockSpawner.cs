using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CaveIn.Spawning
{
    public class RockSpawner : MonoBehaviour
    {
        [SerializeField] GameObject[] rocks;
        [SerializeField] float timeBetweenSpawn = 2f;
        [SerializeField] int maxRocksPerSpawn = 10;
        [SerializeField] float maxRockForce = 10;

        float timer = 0;
        List<RockSpawnLocation> rockSpawnerSmall = new List<RockSpawnLocation>();
        List<RockSpawnLocation> rockSpawnerMedium = new List<RockSpawnLocation>();
        List<RockSpawnLocation> rockSpawnerBig = new List<RockSpawnLocation>();
        private void Start()
        {
            foreach (Transform child in transform) 
            {
                RockSpawnLocation spawnLocation = child.GetComponent<RockSpawnLocation>();

                switch (spawnLocation.GetSpawnSize())
                {
                    case (RockSize.Small):
                        rockSpawnerSmall.Add(spawnLocation);
                        break;
                    case (RockSize.Medium):
                        rockSpawnerMedium.Add(spawnLocation);
                        break;
                    case (RockSize.Big):
                        rockSpawnerBig.Add(spawnLocation);
                        break;
                }
            }
        }
        private void Update()
        {
            if (timer > timeBetweenSpawn)
            {
                timer = 0;
                SpawnRocks();
            }
            timer += Time.deltaTime;
        }

        private void SpawnRocks()
        {
            int randRockIndex = GetRandomNum(0, rocks.Length);
            int randAmountIndex = GetRandomNum(2, maxRocksPerSpawn);

            for (int i = 0; i < randAmountIndex; i++)
            {
                GameObject rock = rocks[randRockIndex];
                Vector3 spawnLocation = GetRandomSpawnLocation(rock.GetComponent<Rock>().GetRockSize()); //TODO: During a single spawn only one rock should be spawned at any spawn location (not multiple)
                if (spawnLocation == Vector3.zero) return;

                Instantiate(rock, spawnLocation, rock.transform.rotation);
            }
        }

        private Vector3 GetRandomSpawnLocation(RockSize rockSize)
        {
            switch (rockSize)
            {
                case (RockSize.Small):
                    return rockSpawnerSmall[GetRandomNum(0, rockSpawnerSmall.Count)].transform.position;
                case (RockSize.Medium):
                    return rockSpawnerMedium[GetRandomNum(0, rockSpawnerMedium.Count)].transform.position;
                case (RockSize.Big):
                    return rockSpawnerBig[GetRandomNum(0, rockSpawnerBig.Count)].transform.position;
            }
            return Vector3.zero;
        }

        private int GetRandomNum(int min,int max)
        {
            // min inclusive, max exclusive
            return UnityEngine.Random.Range(min, max);
        }
        private float GetRandomNum(float min, float max)
        {
            return UnityEngine.Random.Range(min, max);
        }
    }

    
}
