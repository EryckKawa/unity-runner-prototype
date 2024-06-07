using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Components
    private Rigidbody playerRb;
    private Animator playerAnim;
    private AudioSource playerAudio;
    
    // Serialized fields
    [SerializeField] private ParticleSystem explosionParticle;
    [SerializeField] private ParticleSystem dirtParticle;
    [SerializeField] private AudioClip jumpSound;
    [SerializeField] private AudioClip deathSound;
    [SerializeField] private float jumpForce;
    [SerializeField] private float gravityMulti;
    [SerializeField] private bool isOnGround = true;

    // Private fields
    private bool gameOver = false;

    // Constants for tags and animation parameters
    private const string GROUND = "Ground";
    private const string OBSTACLE = "Obstacle";
    private const string JUMP_TRIG = "Jump_trig";
    private const string DEATH_B = "Death_b";
    private const string DEATHTYPE_INT = "DeathType_int";

    // Start is called before the first frame update
    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        playerAnim = GetComponent<Animator>();
        playerAudio = GetComponent<AudioSource>();
        
        Physics.gravity *= gravityMulti;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isOnGround && !gameOver)
        {
            Jump();
        }
    }
    
    private void Jump()
    {
        playerRb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        playerAnim.SetTrigger(JUMP_TRIG);
        isOnGround = false;

        playerAudio.PlayOneShot(jumpSound);
        dirtParticle.Stop();
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag(GROUND))
        {
            Land();
        }
        else if (other.gameObject.CompareTag(OBSTACLE))
        {
            Die();
        }
    }
    
    private void Land()
    {
        isOnGround = true;
        dirtParticle.Play();
    }

    private void Die()
    {
        playerAnim.SetBool(DEATH_B, true);
        playerAnim.SetInteger(DEATHTYPE_INT, 1);

        explosionParticle.Play();
        dirtParticle.Stop();
        playerAudio.PlayOneShot(deathSound);
    
        gameOver = true;
    }

    public bool GetGameOver()
    {
        return gameOver;
    }
}
