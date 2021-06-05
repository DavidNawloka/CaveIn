using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CaveIn.Spawning
{
    public class RockSpawner : MonoBehaviour
    {
        [SerializeField] GameObject[] rocks;
        [SerializeField] Transform rockParent;
        
        [SerializeField] int maxRocksPerSpawn = 10;
        [SerializeField] int minRocksPerSpawn = 4;

        float timeBetweenSpawn = 5f;
        float currentTimeBetweenSpawn;
        float timer = 0;
        List<RockSpawnLocation> rockSpawnerSmall = new List<RockSpawnLocation>();
        List<RockSpawnLocation> rockSpawnerMedium = new List<RockSpawnLocation>();
        List<RockSpawnLocation> rockSpawnerBig = new List<RockSpawnLocation>();

        List<Vector3> usedSpawnerSmall = new List<Vector3>();
        List<Vector3> usedSpawnerMedium = new List<Vector3>();
        List<Vector3> usedSpawnerBig = new List<Vector3>();
        private void Awake()
        {
            Time.timeScale = 1;
        }
        private void Start()
        {
            currentTimeBetweenSpawn = timeBetweenSpawn;
            foreach (RockSpawnLocation spawner in FindObjectsOfType<RockSpawnLocation>()) 
            {

                switch (spawner.GetSpawnSize())
                {
                    case (RockSize.Small):
                        rockSpawnerSmall.Add(spawner);
                        break;
                    case (RockSize.Medium):
                        rockSpawnerMedium.Add(spawner);
                        break;
                    case (RockSize.Big):
                        rockSpawnerBig.Add(spawner);
                        break;
                }
            }
        }
        private void Update()
        {
            if (timer > currentTimeBetweenSpawn)
            {
                timer = 0;
                currentTimeBetweenSpawn = GetRandomNum(timeBetweenSpawn * .95f, timeBetweenSpawn * 1.05f);
                SpawnRocks();
            }
            timer += Time.deltaTime;
        }

        private void SpawnRocks()
        {
            int randRockIndex = GetRandomNum(0, rocks.Length);
            int randAmountIndex = GetRandomNum(minRocksPerSpawn, maxRocksPerSpawn);

            usedSpawnerSmall.Clear();
            usedSpawnerMedium.Clear();
            usedSpawnerBig.Clear();

            for (int i = 0; i < randAmountIndex; i++)
            {
                GameObject rock = rocks[randRockIndex];
                Vector3 spawnLocation = GetRandomSpawnLocation(rock.GetComponent<Rock>().GetRockSize());
                if (spawnLocation == Vector3.zero) return;

                GameObject rockInstance = Instantiate(rock, spawnLocation, Quaternion.Euler(GetRandomRotation()),rockParent);
                var rigidbody = rockInstance.GetComponent<Rigidbody>();
                if (rigidbody == null) return;

                rigidbody.AddTorque(new Vector3(GetRandomNum(20, 50f), GetRandomNum(20f, 50f), GetRandomNum(20f, 50f)));
            }
        }

        private Vector3 GetRandomSpawnLocation(RockSize rockSize)
        {
            if (rockSize == RockSize.Small)
            {
                Vector3 spawnLocation = rockSpawnerSmall[GetRandomNum(0, rockSpawnerSmall.Count)].transform.position;

                if (usedSpawnerSmall.Count >= rockSpawnerSmall.Count) return Vector3.zero;
                while (usedSpawnerSmall.Contains(spawnLocation))
                {
                    spawnLocation = rockSpawnerSmall[GetRandomNum(0, rockSpawnerSmall.Count)].transform.position;
                }

                usedSpawnerSmall.Add(spawnLocation);
                return spawnLocation;

            }
            else if (rockSize == RockSize.Medium)
            {
                Vector3 spawnLocation = rockSpawnerMedium[GetRandomNum(0, rockSpawnerMedium.Count)].transform.position;

                if (usedSpawnerMedium.Count >= rockSpawnerMedium.Count) return Vector3.zero;
                while (usedSpawnerMedium.Contains(spawnLocation))
                {
                    spawnLocation = rockSpawnerMedium[GetRandomNum(0, rockSpawnerMedium.Count)].transform.position;
                }

                usedSpawnerMedium.Add(spawnLocation);
                return spawnLocation;
            }
            else
            {
                Vector3 spawnLocation = rockSpawnerBig[GetRandomNum(0, rockSpawnerBig.Count)].transform.position;

                if (usedSpawnerBig.Count >= rockSpawnerBig.Count) return Vector3.zero;
                while (usedSpawnerBig.Contains(spawnLocation))
                {
                    spawnLocation = rockSpawnerBig[GetRandomNum(0, rockSpawnerBig.Count)].transform.position;
                }

                usedSpawnerBig.Add(spawnLocation);
                return spawnLocation;
            }
        }

        private Vector3 GetRandomRotation()
        {
            return new Vector3(
                GetRandomNum(0, 360f),
                GetRandomNum(0, 360f),
                GetRandomNum(0, 360f));
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

        public void UpdateSpawnValues(float timeBetweenSpawn,int minRocksPerSpawn, int maxRocksPerSpawn)
        {
            this.timeBetweenSpawn = timeBetweenSpawn;
            this.minRocksPerSpawn = minRocksPerSpawn;
            this.maxRocksPerSpawn = maxRocksPerSpawn;
        }
    }

    
}
