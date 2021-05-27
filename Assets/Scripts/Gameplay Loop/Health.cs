using CaveIn.Spawning;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CaveIn.GameplayLoop
{
    public class Health : MonoBehaviour
    {
        [SerializeField] float initialHealth = 100;

        private void OnCollisionEnter(Collision collision)
        {
            Rock rock = collision.gameObject.GetComponent<Rock>();
            if (rock != null)
            {
                initialHealth -= rock.GetDamage();
                CheckIfDead();
            }
        }

        private void CheckIfDead()
        {
            if (initialHealth < 0)
            {
                Die();
            }
        }

        private void Die()
        {
            // TODO: Handle Game Over
        }
    }
}
