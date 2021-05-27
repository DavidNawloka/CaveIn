using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CaveIn.Spawning
{
    public class Rock : MonoBehaviour
    {
        [SerializeField] RockSize rockSize;
        [SerializeField] float maxDamage;
        [SerializeField] float minDamage;

        public RockSize GetRockSize()
        {
            return rockSize;
        }
        public float GetDamage()
        {
            return UnityEngine.Random.Range(minDamage, maxDamage);
        }
        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.CompareTag("Ground")) Destroy(gameObject);
        }
    }
}
