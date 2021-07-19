using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerController : MonoBehaviour
{
    public Transform groundCheck;
    public GameObject npc;
    public GameObject npc1;
    public GameObject npc2;
    public AudioClip footStep;
    public AudioClip jump;
    public AudioClip land;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;

    private CharacterController controller;
    private float speed = 2.5f;
    private float gravity = -9.81f;
    private float jumpHeight = 1f;
    private float currentSpeed = 0f;

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

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;

        if (Input.GetKey(KeyCode.LeftShift) && Input.GetKey(KeyCode.W))
            currentSpeed = speed * 2f;
        else
            currentSpeed = speed;

        controller.Move(move* currentSpeed * Time.deltaTime);

        startNPC(npc);
        startNPC(npc1);
        startNPC(npc2);

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            sound.Stop();
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

    void startNPC(GameObject idle)
    {
        if (Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0)
        {
            NavMeshAgent nma = idle.GetComponent<NavMeshAgent>();
            Animator an = idle.GetComponent<Animator>();

            if (!nma.enabled && an.GetInteger("NPCState") != 2)
            {
                nma.enabled = true;
                an.SetInteger("NPCState", 1);
            }
        }
    }
}
