using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CaveIn.GameplayLoop
{
    public class Ore : MonoBehaviour
    {
        [SerializeField] bool isGold = false;
        [SerializeField] float destructionTime = 1.5f;
        
        public bool GetIsGold()
        {
            return isGold;
        }

        public float GetDestuctionTime()
        {
            return destructionTime;
        }

        public void Destroy()
        {
            // TODO: Particle Effects
            Destroy(gameObject);
        }
    }
}

