﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class P2ItemIcon : MonoBehaviour {

	public static Sprite itemSprite = null;
	public static Color iconColor = Color.white;
    public GameObject partEffect;
    Image image;
    public GameObject player2Hint;
    public bool isIconActive = false;
    private bool boom = true;
    private Sprite checkPickup;

    void Start () {
		image = GetComponent<Image>();
	}

	void Update() {

        isIconActive = itemSprite != null;

        if (itemSprite != null) {
            player2Hint.SetActive(true);

            if (checkPickup != itemSprite)
            {
                boom = true;
            }
            if (boom)
            {

                Instantiate(partEffect, new Vector3(17.5f, 0, 6.2f), new Quaternion());

               // Instantiate(partEffect, GameObject.FindGameObjectWithTag("godsRing").transform.position, GameObject.FindGameObjectWithTag("godsRing").transform.rotation);
                if(FindObjectOfType<AudioManager>()!=null)FindObjectOfType<AudioManager>().Play("godGetItem");
                boom = false;
            }
            image.color = iconColor;
			image.enabled = true;
			image.sprite = itemSprite;
            checkPickup = itemSprite;

        }
        else {
			iconColor = Color.white;
			image.enabled = false;
		}
	}
}