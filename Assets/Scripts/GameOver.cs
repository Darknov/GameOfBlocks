﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GameOver : MonoBehaviour {

	public float restartDelay = 2f;

	Animator anim;
	float restartTimer;

	void Start () {
		anim = GetComponent<Animator>();
    }

	void Update () {
		if (GameObject.FindGameObjectWithTag("countDown").GetComponent<CountDown>().timeRemaining <= 0) {
			anim.SetTrigger ("GameOver1");
			CountDown.started = false;
			restartTimer += Time.deltaTime;
			if (restartTimer >= restartDelay) {
				SceneManager.LoadScene (0);
				if (GameObject.FindGameObjectWithTag("countDown").GetComponent<CountDown>().timeRemaining != 60) {
					GameObject.FindGameObjectWithTag("countDown").GetComponent<CountDown>().timeRemaining = 60;
				}
			}
		}

		if (!GameObject.FindWithTag("Player")) {
			anim.SetTrigger ("GameOver2");
			CountDown.started = false;
			restartTimer += Time.deltaTime;
			if (restartTimer >= restartDelay) {
				SceneManager.LoadScene (0);
				if (GameObject.FindGameObjectWithTag("countDown").GetComponent<CountDown>().timeRemaining != 60) {
					GameObject.FindGameObjectWithTag("countDown").GetComponent<CountDown>().timeRemaining = 60;
				}
			}
		}
	}
}
