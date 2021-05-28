using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Astutos.CCFPS
{
    public class CCFPSController : MonoBehaviour
    {
        [Header("Speed Related")]
        [Tooltip("Speed in m/s when not sprinting")] public float walkSpeed = 20f;
        [Tooltip("Speed in m/s when sprinting")] public float sprintSpeed = 15f;
        [Tooltip("Speed in m/s when jumping")] [SerializeField] float jumpSpeed = 10f;
        [SerializeField] AudioClip[] runSounds;
        [Header("Jump Related")]
        [Tooltip("% amount of how much current velocity is being taken into account when starting to jump")] [SerializeField] float jumpVelocityInfluence = .5f;
        [Tooltip("in m/s²")] [SerializeField] float gravity = -9.81f;
        [Tooltip("in m")] [SerializeField] float jumpHeight = 5f;
        [Tooltip("Radius of ground check sphere")] [SerializeField] float groundCheckDistance = .4f;
        [Tooltip("LayerMask ground check checks for")] [SerializeField] LayerMask groundMask;
        [SerializeField] AudioClip[] jumpStartSounds;
        [SerializeField] AudioClip[] jumpEndSounds;

        AudioSource audioSource;
        Animator animator;
        CharacterController characterController;

        bool isGrounded = true;
        bool isGroundedLastFrame = true;
        Vector3 velocity;
        Vector3 currentVelocity;
        float speed;

        void Awake()
        {
            audioSource = GetComponent<AudioSource>();
            animator = GetComponent<Animator>();
            characterController = GetComponent<CharacterController>();
        }


        void Update()
        {
            isGrounded = Physics.CheckSphere(transform.localPosition - new Vector3(0, 1f, 0), groundCheckDistance, groundMask);

            if (isGrounded && !isGroundedLastFrame) PlaySound(jumpEndSounds);


            ResetVelocity();
            HandleHorizontalMovement();
            HandleVerticalMovement();

            isGroundedLastFrame = isGrounded;

        }
        public bool GetIsGrounded()
        {
            return isGrounded;
        }
        public float GetVelocity()
        {
            return currentVelocity.magnitude;
        }
        private void ResetVelocity()
        {
            if (isGrounded && velocity.y < 0)
            {
                currentVelocity = characterController.velocity;
                velocity = new Vector3(0, -2f, 0);
            }
        }

        private void HandleHorizontalMovement()
        {
            UpdateSpeed();

            float moveX = Input.GetAxis("Horizontal");
            float moveZ = Input.GetAxis("Vertical");

            Vector3 motion = transform.forward * moveZ + transform.right * moveX;
            UpdateAnimator(motion);
            motion = Vector3.ClampMagnitude(motion, 1f);
            characterController.Move(motion * Time.deltaTime * speed);
            currentVelocity = characterController.velocity;
        }

        private void UpdateAnimator(Vector3 motion)
        {
            if (motion.magnitude > 0.1 && isGrounded)
            {
                animator.SetBool("Run", true);
                PlaySound(runSounds);
            }
            else
            {
                animator.SetBool("Run", false);
            }
        }

        private void UpdateSpeed()
        {
            if (!isGrounded)
            {
                speed = jumpSpeed;
            }
            else if (Input.GetKey(KeyCode.LeftShift))
            {
                speed = sprintSpeed;
            }
            else
            {
                speed = walkSpeed;
            }
        }
        private void HandleVerticalMovement()
        {
            if (Input.GetButtonDown("Jump") && isGrounded)
            {
                Vector3 jumpVelocity = characterController.velocity * jumpVelocityInfluence;
                jumpVelocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
                velocity = jumpVelocity;
                PlaySound(jumpStartSounds);
            }

            velocity.y += gravity * Time.deltaTime;

            characterController.Move(velocity * Time.deltaTime);
        }

        private void PlaySound(AudioClip[] audioArray)
        {
            if (audioSource.isPlaying) return;

            int randNum = UnityEngine.Random.Range(0, audioArray.Length);
            audioSource.PlayOneShot(audioArray[randNum]);
        }

    }
}

