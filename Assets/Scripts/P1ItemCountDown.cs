﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class P1ItemCountDown : MonoBehaviour {

	public static float itemTimeRemaining = 5f;
	public static string itemText = "No item";
	public static bool started = false;
	Text text;
    public GameObject player1hint;

	void Start () {
		text = GetComponent<Text>();
	}

	void Update () {

		text.text = itemText;

		if (started) {
			//text.text = itemText + Mathf.Round (itemTimeRemaining * 100f) / 100f;
            player1hint.SetActive(false);

		}
	}
}