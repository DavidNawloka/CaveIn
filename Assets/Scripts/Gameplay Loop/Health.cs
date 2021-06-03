using CaveIn.Spawning;
using CaveIn.UI;
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

        LevelUI levelUIManager;

        private void Start()
        {
            levelUIManager = FindObjectOfType<LevelUI>();
            currentHealth = initialHealth;
        }
        private void OnCollisionEnter(Collision collision)
        {
            Rock rock = collision.gameObject.GetComponent<Rock>();
            if (rock != null && rock.CanHurt())
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
            levelUIManager.Death();
        }
    }
}
