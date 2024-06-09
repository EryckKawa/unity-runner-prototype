using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
	[SerializeField] private GameObject[] obstaclesPrefabs;
	private Vector3 spawnPos = new Vector3(25, 0, 0);

	private const string PLAYER = "Player";
	private PlayerController playerControllerScript;

	private const string SPAWN_OBSTACLE = "SpawnObstacle";
	[SerializeField] private float startDelay = 2;
	[SerializeField] private float repeatRate = 1.5f;
	// Start is called before the first frame update
	void Start()
	{
		playerControllerScript = GameObject.Find(PLAYER).GetComponent<PlayerController>();
		InvokeRepeating(SPAWN_OBSTACLE, startDelay, repeatRate);
	}

	// Update is called once per frame
	void Update()
	{

	}

	private void SpawnObstacle()
	{
		int obstacleIndex = Random.Range(0, obstaclesPrefabs.Length);
	
		if (playerControllerScript.GetGameOver() == false)
		{
			Instantiate(obstaclesPrefabs[obstacleIndex], spawnPos, obstaclesPrefabs[obstacleIndex].transform.rotation);
		}
	}
}
