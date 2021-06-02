using CaveIn.Spawning;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace CaveIn.GameplayLoop
{
    public class Health : MonoBehaviour
    {
        [SerializeField] float initialHealth = 100;

        float currentHealth;

        [HideInInspector] public UnityEvent<float,float> OnHealthUpdate;

        private void Start()
        {
            currentHealth = initialHealth;
        }
        private void OnCollisionEnter(Collision collision)
        {
            Rock rock = collision.gameObject.GetComponent<Rock>();
            if (rock != null)
            {
                currentHealth -= rock.GetDamage();
                OnHealthUpdate.Invoke(currentHealth, initialHealth);
                CheckIfDead();
            }
        }

        private void CheckIfDead()
        {
            if (currentHealth < 0)
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
