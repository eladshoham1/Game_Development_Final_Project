using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Transform groundCheck;
    public AudioClip footStep;
    public AudioClip jump;
    public AudioClip land;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;

    private CharacterController controller;
    private float speed = 7f;
    private float gravity = -9.81f;
    private float jumpHeight = 1f;

    private Vector3 velocity;
    private bool isGrounded;

    private AudioSource sound;
    private Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
        sound = GetComponent<AudioSource>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 prevPosition = this.transform.position;
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if(isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;

        controller.Move(move*speed*Time.deltaTime);

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            sound.clip = jump;
            sound.Play();
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        } 
        else if (sound.clip == jump && isGrounded)
        {
            sound.Stop();
            sound.clip = land;
            sound.Play();
        } 
        else
        {
            sound.clip = footStep;
        }

        velocity.y += gravity * Time.deltaTime;

        controller.Move(velocity * Time.deltaTime);

        anim.SetBool("IsWalking", prevPosition != this.transform.position);
        if (!sound.isPlaying && anim.GetBool("IsWalking"))
        {
            sound.Play();
        }
    }
}
