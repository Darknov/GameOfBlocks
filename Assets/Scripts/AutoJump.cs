using UnityEngine;
using System.Collections;

public class AutoJump : MonoBehaviour {

	public CharacterController characterController;

	public float predkoscPoruszania = 3.0f;
	public float wysokoscSkoku = 100.0f;
	private float aktualnaWysokoscSkoku = 0f;
	private bool wyjsciePoza;

	void Start () {
		wyjsciePoza = false;
		characterController = GetComponent<CharacterController>();
	}

	void Update() {
		klawiatura();
	}

	private void klawiatura(){
		float ruchPrzodTyl = Input.GetAxis("Vertical") * predkoscPoruszania;
		float ruchLewoPrawo = Input.GetAxis("Horizontal") * predkoscPoruszania;

		if(wyjsciePoza == true){
			aktualnaWysokoscSkoku = wysokoscSkoku;
		} else if (!characterController.isGrounded){
			aktualnaWysokoscSkoku += Physics.gravity.y * Time.deltaTime;
		}

		Debug.Log (Physics.gravity.y);

		Vector3 ruch = new Vector3(ruchLewoPrawo, aktualnaWysokoscSkoku, ruchPrzodTyl);

		ruch = transform.rotation * ruch;

		characterController.Move(ruch * Time.deltaTime);
	}

	void OnCollisionEnter(Collision col) {
		if (col.gameObject.tag == "block") {
			wyjsciePoza = true;
		}
	}
}