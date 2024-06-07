using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
	[SerializeField] private GameObject obstaclePrefab;
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
		if (playerControllerScript.GetGameOver() == false)
		{
			Instantiate(obstaclePrefab, spawnPos, obstaclePrefab.transform.rotation);
		}
	}
}
