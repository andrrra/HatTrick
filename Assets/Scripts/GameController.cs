﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour {

	public Camera cam;
	public GameObject[] balls;
	public float timeLeft;
	public Text timerText;
	public GameObject gameOverText;
	public GameObject restartButton;
	public GameObject splashScreen;
	public GameObject startButton;
	public HatController hatController;


	private float maxWidth;
	private bool playing;


	// Use this for initialization
	void Start () {
		if (cam == null) {
			cam = Camera.main;
		}
		playing = false;
		Vector3 upperCorner = new Vector3 (Screen.width, Screen.height, 0.0f);
		Vector3 targetWidth = cam.ScreenToWorldPoint (upperCorner);
		float ballWidth = balls[0].GetComponent<Renderer> ().bounds.extents.x;
		maxWidth = targetWidth.x - ballWidth;
		UpdateText ();
	}


	void FixedUpdate() {
		if (playing) {
			timeLeft -= Time.deltaTime;
			if (timeLeft < 0) {
				timeLeft = 0;
				playing = false;
			}
			UpdateText ();
		}
	}


	void UpdateText() {
		timerText.text = "Time Left:\n" + Mathf.RoundToInt (timeLeft);
	}


	public void StartGame() {
		splashScreen.SetActive (false);
		startButton.SetActive (false);
		hatController.ToggleControl (true);
		StartCoroutine (Spawn ());
		playing = true;
	}


	IEnumerator Spawn() {
		yield return new WaitForSeconds (2.0f);
		while (timeLeft > 0) {
			GameObject ball = balls[Random.Range(0, balls.Length)];
			Vector3 spawnPosition = new Vector3 (
				                        Random.Range (-maxWidth, maxWidth),
				                        this.transform.position.y,
				                        0.0f
			                        );
			Quaternion spawnRotation = Quaternion.identity;
			Instantiate (ball, spawnPosition, spawnRotation);

			yield return new WaitForSeconds (Random.Range(1.0f, 2.0f));
		}
		yield return new WaitForSeconds (2.0f);
		gameOverText.SetActive (true);
		yield return new WaitForSeconds (2.0f);
		restartButton.SetActive (true);
	}

}
