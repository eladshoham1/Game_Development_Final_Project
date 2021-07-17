using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public GameObject playerCamera;

    private CharacterController controller;
    private float speed = 3f;
    private float gravity = 0.2f;
    private float velocity = 0f;
    private AudioSource footStepSound;
    private Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
        footStepSound = GetComponent<AudioSource>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 prevPosition = this.transform.position;
        float horizontal = Input.GetAxis("Horizontal") * speed;
        float vertical = Input.GetAxis("Vertical") * speed;
        playerCamera.transform.localEulerAngles = new Vector3(0, 0, 0);
        transform.localEulerAngles = new Vector3(0, 0, 0);
        controller.Move((Vector3.right * horizontal + Vector3.forward * vertical) * Time.deltaTime);

        if (!controller.isGrounded)
        {
            velocity -= gravity * Time.deltaTime;
            controller.Move(new Vector3(0, velocity, 0));
        }
        else
            velocity = 0;

        anim.SetBool("IsWalking", prevPosition != this.transform.position);
        if (!footStepSound.isPlaying && controller.velocity.magnitude > 0.1f)
            footStepSound.Play();
    }
}




/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public AudioClip FootStepsSound;
    public AudioClip JumpSound;
    public AudioClip LandingSound;

    private Vector3 moveDir = Vector3.zero;

    private Animator anim;
    private CharacterController controller;
    private AudioSource audioSource;
    private bool wasGrounded = false;
    private bool isJumpPressed = false;
    private float speed = 4.0f;
    private float jumpHeight = 7.0f;
    private float gravity = 20f;

    void Start()
    {
        anim = GetComponent<Animator>();
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
                anim.SetBool("IsWalking", true);
                moveDir *= (speed * 1.5f);
            }
            else
            {
                anim.SetBool("IsWalking", false);
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

}*/