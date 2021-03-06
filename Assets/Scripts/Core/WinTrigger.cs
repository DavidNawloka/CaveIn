using CaveIn.GameplayLoop;
using CaveIn.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CaveIn.Core
{
    public class WinTrigger : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            if(other.tag == "Player")
            {
                Win(other);
            }
        }

        private static void Win(Collider other)
        {
            Miner miner = other.gameObject.GetComponent<Miner>();
            FindObjectOfType<LevelUI>().Win(miner.GetGoldAmount(), miner.GetMaxGoldAmount());
        }
    }
}
