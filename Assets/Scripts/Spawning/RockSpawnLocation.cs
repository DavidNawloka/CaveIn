using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CaveIn.Spawning
{
    public class RockSpawnLocation : MonoBehaviour
    {
        [SerializeField] RockSize spawnRockSize;
        [SerializeField] float gizmoRadius = 1f;

        private void OnDrawGizmos()
        {
            if (spawnRockSize == RockSize.Small) Gizmos.color = Color.yellow;
            if (spawnRockSize == RockSize.Medium) Gizmos.color = new Color(1, .5f, 0);
            if (spawnRockSize == RockSize.Big) Gizmos.color = Color.red;

            Gizmos.DrawSphere(transform.position, gizmoRadius);
        }

        public RockSize GetSpawnSize()
        {
            return spawnRockSize;
        }
    }

    public enum RockSize
    {
        Small,
        Medium,
        Big
    }

}