using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class PlayerController : MonoBehaviour
{
    private Rigidbody playerRb;

    public float jumpForce = 10;
    public float gravityModifier = 2;

    private bool isOnGround = false;

    public bool gameOver = false;

    private Animator animator;

    public ParticleSystem smokeParticleSystem;
    public ParticleSystem dirtParticleSystem;

    public AudioSource audioSource;
    public AudioClip jumpClip;
    public AudioClip collideClip;

    // Start is called before the first frame update
    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
        Physics.gravity *= gravityModifier;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isOnGround)
            animator.ResetTrigger("Jump_trig");

        if (Input.GetKeyDown(KeyCode.Space) && isOnGround && !gameOver)
        {
            playerRb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isOnGround = false;
            animator.SetTrigger("Jump_trig");
            animator.SetBool("Jump_b", !isOnGround);
            dirtParticleSystem.Stop();
            audioSource.PlayOneShot(jumpClip);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag.Equals("Ground"))
        {
            isOnGround = true;
            animator.SetBool("Jump_b", !isOnGround);
            animator.ResetTrigger("Jump_trig");
            if (!gameOver)
                dirtParticleSystem.Play();
        }
        else if (collision.gameObject.tag.Equals("Obstacle"))
        {
            gameOver = true;
            animator.SetBool("Death_b", true);
            animator.SetInteger("DeathType_int", 1);
            dirtParticleSystem.Stop();
            smokeParticleSystem.Play();
            audioSource.PlayOneShot(collideClip);
            print("Game Over!");
        }
            
    }


}
