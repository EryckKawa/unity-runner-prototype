using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
	[SerializeField] private PlayerController playerControllerScript;
	[SerializeField] private TMP_Text scoreText;

	[SerializeField] private Transform startingPoint;
	[SerializeField] private float lerpSpeed;
	[SerializeField] private float scoreIncrement = 1.0f;
	private float score;

	// Start is called before the first frame update
	void Start()
	{
		score = 0;
		UpdateScoreText();

		playerControllerScript.SetGameOver(false);
		StartCoroutine(PlayIntro());
	}

	private IEnumerator PlayIntro()
	{
		Vector3 startPos = playerControllerScript.transform.position;
		Vector3 endPos = startingPoint.position;
		float journeyLenght = Vector3.Distance(startPos, endPos);
		float startTime = Time.time;

		float distanceCovered = (Time.time - startTime) * lerpSpeed;
		float fractionOfJourney = distanceCovered / journeyLenght;

		playerControllerScript.GetComponent<Animator>().SetFloat("SpeedMultiplier", 0.5f);

		while (fractionOfJourney < 1)
		{
			distanceCovered = (Time.time - startTime) * lerpSpeed;
			fractionOfJourney = distanceCovered / journeyLenght;
			playerControllerScript.transform.position = Vector3.Lerp(startPos, endPos, fractionOfJourney);
			yield return null;
		}

		playerControllerScript.GetComponent<Animator>().SetFloat("SpeedMultiplier", 1.0f);
		playerControllerScript.SetGameOver(false);
	}

	// Update is called once per frame
	void Update()
	{
		ScoreIncrease();
	}

	private void ScoreIncrease()
	{
		if (!playerControllerScript.GetGameOver())
		{
			score += scoreIncrement * Time.deltaTime;
			UpdateScoreText();
		}
	}

	private void UpdateScoreText()
	{
		scoreText.text = "Score: " + Mathf.FloorToInt(score).ToString() + "m";
	}
}
