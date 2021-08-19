using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public GameObject statusCanvas;
    public Transform groundCheck;
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
        if (GetComponent<Stats>().IsDead() || !statusCanvas.activeInHierarchy)
            return;

        Vector3 prevPosition = this.transform.position;
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if (isGrounded && velocity.y < 0)
            velocity.y = -2f;

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;

        if (Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0)
        {
            if (sound.clip != jump)
                PlaySound(footStep);

            if (Input.GetKey(KeyCode.LeftShift))
            {
                currentSpeed = speed * 2f;
                sound.pitch = 2f;
            }
            else
            {
                currentSpeed = speed;
                sound.pitch = 1f;
            }
        }

        controller.Move(move * currentSpeed * Time.deltaTime);

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            sound.Stop();
            PlaySound(jump);
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        } 
        else if (sound.clip == jump && isGrounded)
            PlaySound(land);

        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);

        anim.SetInteger("NPCState", prevPosition == this.transform.position ? 0 : 1);
    }

    private void PlaySound(AudioClip clip)
    {
        if (!sound.isPlaying)
        {
            sound.clip = clip;
            sound.Play();
        }
    }
}
