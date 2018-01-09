﻿using UnityEngine;

public class InverseControl : MonoBehaviour {

	public float timeOfInverseControlOfPlayer2 = 3.0f;
	private bool isTriggered = false;
	public GameObject inverseControl;
	public Sprite inverseControlSprite;
    public Material inversedControlMaterial;
    public Material standardMaterial;

    void Update () {

		isDestroyed ();

		if (isTriggered) {
			if (Player2Controller.p2GamePad) {
				if (Input.GetKeyDown ("joystick 2 button 6") && !Player1Controller.inverseControlUsed) {
<<<<<<< HEAD
                    FindObjectOfType<AudioManager>().Play("inverseControl");
					//P1ItemIcon.itemSprite = inverseControlSprite;
					//P1ItemIcon.iconColor = Color.red;
=======
>>>>>>> PO3
					P2ItemIcon.iconColor = Color.red;
					Player1Controller.inverseControl = true;
					P2ItemCountDown.started = true;
					Player1Controller.inverseControlUsed = true;
					P2ItemCountDown.itemText = "Inverse Control Used\n" + "Time Remaining: ";
				}
				if (Player1Controller.inverseControl) {
				    GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<SkinnedMeshRenderer>().material = inversedControlMaterial; 
					P2ItemCountDown.itemTimeRemaining = timeOfInverseControlOfPlayer2;
					timeOfInverseControlOfPlayer2 -= Time.deltaTime;
					if (timeOfInverseControlOfPlayer2 < 0) {
						P2ItemIcon.itemSprite = null;
						P2ItemCountDown.started = false;
						Player1Controller.inverseControlUsed = false;
						Player1Controller.inverseControl = false;
						P2ItemCountDown.itemText = "No item";
						isTriggered = false;
						StaticOptions.p2SpawnItems.Remove (inverseControl);
					    GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<SkinnedMeshRenderer>().material = standardMaterial;
                    }
				}
			} else if (!Player2Controller.p2GamePad) {
				if (Input.GetKeyDown (KeyCode.Alpha9) && !Player1Controller.inverseControlUsed) {
<<<<<<< HEAD
                    FindObjectOfType<AudioManager>().Play("inverseControl");
					//P1ItemIcon.itemSprite = inverseControlSprite;
					//P1ItemIcon.iconColor = Color.red;
=======
>>>>>>> PO3
					P2ItemIcon.iconColor = Color.red;
					Player1Controller.inverseControl = true;
					P2ItemCountDown.started = true;
					Player1Controller.inverseControlUsed = true;
					P2ItemCountDown.itemText = "Inverse Control Used\n" + "Time Remaining: ";
				}

				if (Player1Controller.inverseControl) {
				    GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<SkinnedMeshRenderer>().material = inversedControlMaterial;
					P2ItemCountDown.itemTimeRemaining = timeOfInverseControlOfPlayer2;
					timeOfInverseControlOfPlayer2 -= Time.deltaTime;
					if (timeOfInverseControlOfPlayer2 < 0) {
						P2ItemIcon.itemSprite = null;
						P2ItemCountDown.started = false;
						Player1Controller.inverseControlUsed = false;
						Player1Controller.inverseControl = false;
						P2ItemCountDown.itemText = "No item";
						isTriggered = false;
						StaticOptions.p2SpawnItems.Remove (inverseControl);
					    GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<SkinnedMeshRenderer>().material = standardMaterial;
                    }
				}
			}
		}
	}

	void LateUpdate() {
		if (!StaticOptions.p2SpawnItems.Exists (x => x.transform.position.y == inverseControl.transform.position.y)) {
			Destroy (inverseControl);
		}
	}

	void isDestroyed() {

		if(!StaticOptions.p1SpawnItems.Exists(x => x == inverseControl)) {
			P2ItemIcon.itemSprite = null;
			P2ItemCountDown.started = false;
			Player1Controller.inverseControlUsed = false;
			Player1Controller.inverseControl = false;
			P2ItemCountDown.itemText = "No item";
			isTriggered = false;
			StaticOptions.p2SpawnItems.Remove (inverseControl);
			Destroy (inverseControl);
			GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<SkinnedMeshRenderer>().material = standardMaterial;
		}
	}

	void OnCollisionEnter(Collision col) {
		if (col.gameObject.tag == "Player") {
			StaticOptions.p2SpawnItems.Remove (inverseControl);
			Destroy (inverseControl);
		}
	}

	void OnTriggerEnter(Collider col) {
		
		if (col.gameObject.tag == "block") {
<<<<<<< HEAD
            FindObjectOfType<AudioManager>().Play("godGetItem");
			StaticOptions.p2SpawnItems.Remove (inverseControl);
=======
>>>>>>> PO3
			if (P2ItemCountDown.itemText != "No item") {
				Destroy (GameObject.FindGameObjectWithTag("p2TakenItem"));
			}
			inverseControl.tag = "p2TakenItem";
			isTriggered = true;
			if (Player2Controller.p2GamePad) {
				P2ItemCountDown.itemText = "Inverse Control\n" + "Press L2 to use";
			} else if (!Player2Controller.p2GamePad) {
				P2ItemCountDown.itemText = "Inverse Control\n" + "Press 9 to use";
			}
			P2ItemIcon.itemSprite = inverseControlSprite;
			inverseControl.GetComponent<SphereCollider> ().enabled = false;
			for (int i = 0; i < inverseControl.transform.childCount; i++) {
				inverseControl.transform.GetChild(i).gameObject.SetActive(false);
			}
		}
	}
}