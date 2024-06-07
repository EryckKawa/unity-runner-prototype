using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
	private Rigidbody playerRb;
	[SerializeField] private float jumpForce;
	[SerializeField] private float gravityMulti;
	[SerializeField] private bool isOnGround = true;
	private bool gameOver = false;

	private const string GROUND = "Ground";
	private const string OBSTACLE = "Obstacle";


	// Start is called before the first frame update
	void Start()
	{
		playerRb = GetComponent<Rigidbody>();
		Physics.gravity *= gravityMulti;
	}

	// Update is called once per frame
	void Update()
	{
		if (Input.GetKeyDown(KeyCode.Space) && isOnGround)
		{
			playerRb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
			isOnGround = false;
		}
	}
	
	private void OnCollisionEnter(Collision other)
	{
		if (other.gameObject.CompareTag(GROUND))
		{
			isOnGround = true;
		}
		else if (other.gameObject.CompareTag(OBSTACLE))
		{
			Debug.Log("Game Over!");
			gameOver = true;
		}
	}
	
	public bool GetGameOver()
	{
		return gameOver;
	}
}
