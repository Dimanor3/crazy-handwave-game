using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrumpMovement : MonoBehaviour {

	[SerializeField] private float mouseX, speed;

	private Rigidbody rb;

	void Awake () {
		rb = gameObject.GetComponent <Rigidbody> ();
	}

	void Start () {
		// Initialize necessary variables
		speed = 5f;
	}

	// Update is called once per frame
	void Update () {
		mouseX = Input.GetAxis ("Mouse X");

		if (mouseX != 0) {
			rb.AddTorque (0f, 0f, mouseX * speed * -1);
		}
	}
}
