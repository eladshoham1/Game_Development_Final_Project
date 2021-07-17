using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public AudioClip FootStepsSound;
    public AudioClip JumpSound;
    public AudioClip LandingSound;

    private Vector3 moveDir = Vector3.zero;

    private CharacterController controller;
    private AudioSource audioSource;
    private bool wasGrounded = false;
    private bool isJumpPressed = false;
    private float speed = 4.0f;
    private float jumpHeight = 7.0f;
    private float gravity = 20f;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        Move();
    }

    void FixedUpdate()
    {
        if (controller.isGrounded)
        {
            moveDir = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
            moveDir = transform.TransformDirection(moveDir);
            if (Input.GetKey(KeyCode.LeftShift) && Input.GetKey(KeyCode.W))
            {
                moveDir *= (speed * 1.5f);
            }
            else
            {
                moveDir *= speed;
            }


            if (controller.velocity.sqrMagnitude > 0f)
            {
                if (!audioSource.isPlaying)
                {
                    audioSource.clip = FootStepsSound;
                    audioSource.Play();
                }
            }

            else
            {
                if (audioSource.clip != JumpSound && audioSource.clip != LandingSound)
                    if (audioSource.isPlaying)
                        audioSource.Stop();
            }

            if (controller.isGrounded != wasGrounded)
            {
                audioSource.Stop();
                audioSource.clip = LandingSound;
                audioSource.Play();
            }
            wasGrounded = true;
        }
        else
        {
            if (audioSource.clip != JumpSound && audioSource.clip != LandingSound)
                if (audioSource.isPlaying)
                    audioSource.Stop();

            if (controller.isGrounded != wasGrounded)
            {
                audioSource.Stop();
                audioSource.clip = JumpSound;
                audioSource.Play();
            }
            wasGrounded = false;
        }

        if (isJumpPressed)
        {
            moveDir.y = jumpHeight;
            isJumpPressed = false;
        } 

        moveDir.y -= gravity * Time.deltaTime;
        controller.Move(moveDir * Time.deltaTime);
    }

    private void Move()
    {
        if (Input.GetKeyDown(KeyCode.Space) && controller.isGrounded)
        {
            isJumpPressed = true;
        }
    }

}