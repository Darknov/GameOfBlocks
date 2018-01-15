﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class P1ItemIcon : MonoBehaviour {

	public static Sprite itemSprite = null;
	public static Color iconColor = Color.white;
    public GameObject partEffect;
    Image image;
    private bool boom = true;
    private Sprite checkPickup;

	void Start () {
		image = GetComponent<Image>();
	}

	void Update() {
		if (itemSprite != null) {
            if (checkPickup != itemSprite)
            {
                boom = true;
            }
            if (boom )
            {
                Instantiate(partEffect, GameObject.FindGameObjectWithTag("rabbitsRing").transform.position, GameObject.FindGameObjectWithTag("rabbitsRing").transform.rotation);
                FindObjectOfType<AudioManager>().Play("GettingItem");
                boom = false;
            }

            image.color = iconColor;
			image.enabled = true;
			image.sprite = itemSprite;
            checkPickup = itemSprite;
            
		} else {
			iconColor = Color.white;
			image.enabled = false;
            boom = true;
        }

	}
}
