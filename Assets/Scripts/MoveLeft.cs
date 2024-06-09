using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveLeft : MonoBehaviour
{
    private const string PLAYER = "Player";
    private const string OBSTACLE = "Obstacle";

    [SerializeField] private float movSpeed;
    private float leftLimit = -15;
    private PlayerController playerControllerScript;

    // Start is called before the first frame update
    void Start()
    {
        playerControllerScript = GameObject.Find(PLAYER).GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (playerControllerScript.GetGameOver() == false)
        {
            float currentSpeed = movSpeed;
            if (playerControllerScript.IsRunning())
            {
                currentSpeed *= 2; // Double speed when running
            }

            transform.Translate(currentSpeed * Time.deltaTime * Vector3.left);
        }
        if (transform.position.x < leftLimit && gameObject.CompareTag(OBSTACLE))
        {
            Destroy(gameObject);
        }
    }
}
