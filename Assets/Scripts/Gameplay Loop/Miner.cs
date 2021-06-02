using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CaveIn.Core;
using TMPro;
using System;

namespace CaveIn.GameplayLoop
{
    public class Miner : MonoBehaviour
    {
        [SerializeField] RectTransform interactionPoint;
        [SerializeField] float maxRaycastDistance = 2;
        [SerializeField] LayerMask raycastFilterLayer;
        [SerializeField] AudioClip[] pickaxeHitSounds;
        [Range(0,1f)][SerializeField] float volume = 1;
        [SerializeField] float pitch = 1;
        [SerializeField] TextMeshProUGUI goldText;
        [SerializeField] TextMeshProUGUI silverText;

        GameDifficulty gameDifficulty;
        Animator animator;
        AudioSource audioSource;

        private int gold;
        private int silver;
        float timer;
        private void Awake()
        {
            audioSource = GetComponent<AudioSource>();
            animator = GetComponent<Animator>();
            gameDifficulty = FindObjectOfType<GameDifficulty>();
        }
        private void Start()
        {
            UpdateUI();
        }
        private void Update()
        {
            if (Input.GetKey(KeyCode.Mouse0))
            {
                Ray ray = Camera.main.ScreenPointToRay(interactionPoint.position);
                RaycastHit hit;

                if (Physics.Raycast(ray,out hit, maxRaycastDistance,raycastFilterLayer))
                {
                    Ore ore = hit.collider.gameObject.GetComponent<Ore>();
                    if (ore == null) return;
                    StartMining(ore);
                }
                else
                {
                    timer = 0;
                    animator.SetBool("Mine", false);
                }
            }
            else
            {
                animator.SetBool("Mine", false);
                timer = 0;
            }
        }

        private void StartMining(Ore ore)
        {
            animator.SetBool("Mine", true);

            if (timer >= ore.GetDestuctionTime())
            {
                animator.SetBool("Mine", false);
                if (ore.GetIsGold())
                {
                    gold += 1;
                    ore.Destroy();
                }
                else
                {
                    silver += 1;
                    gameDifficulty.IncreaseDifficulty(-1);
                    ore.Destroy();
                }
                UpdateUI();
                timer = 0;
            }

            timer += Time.deltaTime;
        }

        private void UpdateUI()
        {
            silverText.text = silver.ToString();
            goldText.text = gold.ToString();
        }

        public void PickaxeHit() // Animation Event
        {
            int randNum = UnityEngine.Random.Range(0, pickaxeHitSounds.Length);
            audioSource.pitch = pitch;
            audioSource.PlayOneShot(pickaxeHitSounds[randNum],volume); // TODO: Update mining sound
            audioSource.pitch = 1;
        }
        
    }
}
