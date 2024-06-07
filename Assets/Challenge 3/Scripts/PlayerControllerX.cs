using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControllerX : MonoBehaviour
{
	public bool gameOver;

	public float floatForce;
	private float gravityModifier = 1.5f;
	private Rigidbody playerRb;

	public ParticleSystem explosionParticle;
	public ParticleSystem fireworksParticle;

	private AudioSource playerAudio;
	public AudioClip moneySound;
	public AudioClip explodeSound;
	[SerializeField] private AudioClip bounceSound;

	// Start is called before the first frame update
	void Start()
	{
		Physics.gravity *= gravityModifier;

		playerRb = GetComponent<Rigidbody>();
		playerAudio = GetComponent<AudioSource>();

		// Apply a small upward force at the start of the game
		playerRb.AddForce(Vector3.up * 5, ForceMode.Impulse);

	}

	// Update is called once per frame
	void Update()
	{
		// While space is pressed and player is low enough, float up
		if (Input.GetKey(KeyCode.Space) && !gameOver)
		{
			playerRb.AddForce(Vector3.up * floatForce);
		}
		
		LimitUpper();
	}

	private void OnCollisionEnter(Collision other)
	{
		if (other.gameObject.CompareTag("Ground"))
		{
			playerAudio.PlayOneShot(bounceSound, 1.0f);
			Bounce();
		}

		// if player collides with bomb, explode and set gameOver to true
		else if (other.gameObject.CompareTag("Bomb"))
		{
			playerAudio.PlayOneShot(explodeSound, 1.0f);
			gameOver = true;
			Debug.Log("Game Over!");
			Destroy(other.gameObject);
		} 

		// if player collides with money, fireworks
		else if (other.gameObject.CompareTag("Money"))
		{
			fireworksParticle.Play();
			playerAudio.PlayOneShot(moneySound, 1.0f);
			Destroy(other.gameObject);

		}

	}
	private void LimitUpper()
	{
		float limitX = 16;
		float downForce = 10;
		
		if (transform.position.y > limitX)
		{
			transform.position = new Vector3(transform.position.x, limitX, transform.position.z);
			playerRb.AddForce(Vector3.up * - downForce, ForceMode.Impulse);
		}
	}
	
	private void Bounce()
	{
		float limitY = 0;
		float bounceForce = 10;
		if (transform.position.y <= limitY)
		{
			playerRb.AddForce(Vector3.up * bounceForce, ForceMode.Impulse);
		}
	}
}
