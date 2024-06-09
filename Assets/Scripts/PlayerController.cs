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
	[SerializeField] private float doubleJumpForce;
	[SerializeField] private float gravityMulti;
	[SerializeField] private float runSpeed;
	[SerializeField] private bool isOnGround = true;

	// Private fields
	private bool canDoubleJump = false;
	private bool gameOver = false;
	private bool isRunning = false;

	// Constants for tags and animation parameters
	private const string GROUND = "Ground";
	private const string OBSTACLE = "Obstacle";
	private const string JUMP_TRIG = "Jump_trig";
	private const string RUNNING_JUMP = "Running_Jump";
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
		if (Input.GetKeyDown(KeyCode.LeftShift) && !gameOver)
		{
			StartRunning();
		}

		if (Input.GetKeyUp(KeyCode.LeftShift) && !gameOver)
		{
			StopRunning();
		}

		if (Input.GetKeyDown(KeyCode.Space) && !gameOver)
		{
			if (isOnGround)
			{
				Jump();
			}
			else if (canDoubleJump)
			{
				DoubleJump();
			}
		}
	}

	private void StartRunning()
	{
		isRunning = true;
		playerAnim.SetFloat("SpeedMultiplier", runSpeed); // Assuming you have a speed multiplier in your animation
	}

	private void StopRunning()
	{
		isRunning = false;
		playerAnim.SetFloat("SpeedMultiplier", 1.0f); // Reset to normal speed
	}

	private void Jump()
	{
		playerRb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
		playerAnim.SetTrigger(JUMP_TRIG);
		isOnGround = false;

		playerAudio.PlayOneShot(jumpSound);
		dirtParticle.Stop();

		canDoubleJump = true;
	}

	private void DoubleJump()
	{
		playerRb.AddForce(Vector3.up * doubleJumpForce, ForceMode.Impulse);
		playerAnim.Play(RUNNING_JUMP, 3, 0f);
		playerAudio.PlayOneShot(jumpSound);

		canDoubleJump = false;
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
		canDoubleJump = false;
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

	public void SetGameOver(bool state)
	{
		gameOver = state;
	}

	public bool IsRunning()
	{
		return isRunning;
	}
}
 