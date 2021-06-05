using CaveIn.GameplayLoop;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace CaveIn.Feedback
{
    public class HealthIndicator : MonoBehaviour
    {
        [SerializeField] Image redImage;
        [SerializeField] float pulseBeginningPercent = 50;
        [SerializeField] float pulseIncreasePercent = 10;
        [SerializeField] float pulseLengthBeginning = 1;
        [SerializeField] float pulseLengthDecrease= .2f;
        [SerializeField] float minPulseLength = .1f;

        Health health;
        bool shouldPulseStart = true;
        float currentPulseLength;
        float lastHealth;

        private void Awake()
        {
            health = GetComponent<Health>();
            health.OnHealthUpdate.AddListener(UpdatePulse);
        }
        private void Start()
        {
            currentPulseLength = pulseLengthBeginning;
            redImage.enabled = false;
        }
        private void UpdatePulse(float currentHealth, float initialHealth)
        {
            if(currentHealth / initialHealth <= pulseBeginningPercent / 100 && shouldPulseStart)
            {
                shouldPulseStart = false;
                lastHealth = currentHealth;
                StartCoroutine(HealthPulse());
            }
            if (!shouldPulseStart)
            {
                if((lastHealth-currentHealth)/initialHealth >= pulseIncreasePercent / 100)
                {
                    currentPulseLength -= pulseLengthDecrease;
                    if (currentPulseLength <= minPulseLength) currentPulseLength = minPulseLength;
                    lastHealth = currentHealth;
                }
            }
            
        }

        private IEnumerator HealthPulse()
        {
            float timer = 0;
            float alpha = redImage.color.a;
            float factor = 1;

            redImage.color = new Color(redImage.color.r, redImage.color.g, redImage.color.b, 0);
            redImage.enabled = true;
            while (true)
            {
                redImage.color = new Color(redImage.color.r, redImage.color.g, redImage.color.b, Mathf.Clamp(timer / (currentPulseLength/2) * alpha,0,alpha));
                if (timer >= currentPulseLength/2) factor = -1;
                if (timer <= 0) factor = 1;
                timer += Time.deltaTime * factor;

                yield return null;
            }
        }
    }
}
