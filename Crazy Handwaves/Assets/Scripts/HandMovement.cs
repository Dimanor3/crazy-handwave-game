using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandMovement : MonoBehaviour {

	[SerializeField] private float mouseX, speed, incrementer;

	[SerializeField] private int rand, minRand, maxRand, lvlInc, randLvl, randPart;

	[SerializeField] private GameObject[] level1, level2, level3, level4, trumpParts, handParts;

	private GameObject trump;

	private Rigidbody rb;

	private Camera cam;

	[SerializeField] private bool trumpActive;

	void Awake () {
		cam = Camera.main;
		rb = gameObject.GetComponent <Rigidbody> ();
	}

	void Start () {
		// Initialize necessary variables
		speed = 5f;
		incrementer = 0f;
		lvlInc = 1;
		minRand = 100;
		maxRand = 1000;
		randLvl = 0;
		rand = Random.Range (minRand, maxRand + 1);
		trumpActive = false;

		// Collect all required game objects
		level1 = GameObject.FindGameObjectsWithTag ("Level1");
		level2 = GameObject.FindGameObjectsWithTag ("Level2");
		level3 = GameObject.FindGameObjectsWithTag ("Level3");
		level4 = GameObject.FindGameObjectsWithTag ("Level4");
		trump = GameObject.FindGameObjectWithTag ("Trump");
		trumpParts = GameObject.FindGameObjectsWithTag ("TrumpParts");

		trump.SetActive (false);
	}

	// Update is called once per frame
	void Update () {
		mouseX = Input.GetAxis ("Mouse X");

		if (mouseX != 0) {
			if (mouseX > 0) {
				incrementer += mouseX;
			}

			if (mouseX < 0) {
				incrementer += -mouseX;
			}

			rb.AddTorque (0f, 0f, mouseX * speed * -1);
		}

		if (Input.GetKeyDown (KeyCode.A)) {
			trump.SetActive (true);

			trumpActive = true;

			transform.position = new Vector3 (999f, 999f, 999f);
		}

		if (incrementer >= rand) {
			lvlInc++;

			minRand += Random.Range (1, lvlInc + 1) * lvlInc;
			maxRand += Random.Range (1, lvlInc + 1) * lvlInc;

			rand += Random.Range (minRand, maxRand + 1);

			if (!trumpActive) {
				randLvl = Random.Range (0, 4);

				if (randLvl == 0) {
					level1 [Random.Range (0, 5)].transform.position += new Vector3 ((float)lvlInc * Random.Range (0f, (float)(lvlInc + 1)), (float)lvlInc * Random.Range (0f, (float)(lvlInc + 1)), (float) lvlInc * Random.Range (0f, (float)(lvlInc + 1)));
				} else if (randLvl == 1) {
					level2 [Random.Range (0, 5)].transform.position += new Vector3 ((float)lvlInc * Random.Range (0f, (float)(lvlInc + 1)), (float)lvlInc * Random.Range (0f, (float)(lvlInc + 1)), (float)lvlInc * Random.Range (0f, (float)(lvlInc + 1)));
				} else if (randLvl == 2) {
					level3 [Random.Range (0, 5)].transform.position += new Vector3 ((float)lvlInc * Random.Range (0f, (float)(lvlInc + 1)), (float)lvlInc * Random.Range (0f, (float)(lvlInc + 1)), (float)lvlInc * Random.Range (0f, (float)(lvlInc + 1)));
				} else {
					level4 [Random.Range (0, 5)].transform.position += new Vector3 ((float)lvlInc * Random.Range (0f, (float)(lvlInc + 1)), (float)lvlInc * Random.Range (0f, (float)(lvlInc + 1)), (float)lvlInc * Random.Range (0f, (float)(lvlInc + 1)));
				}

				if ((lvlInc >= 10 && Random.Range (minRand, maxRand) >= (maxRand + minRand) * (3 / 4))) {
					trump.SetActive (true);

					trumpActive = true;

					transform.position = new Vector3 (999f, 999f, 999f);
				}
			} else {
				randLvl = Random.Range (0, 16);
				int randPnt = Random.Range (0, 3);

				if (randPnt == 0) {
					trumpParts[randLvl].transform.Rotate (Random.Range (0f, 360f), Random.Range (0f, 360f), Random.Range (0f, 360f));
				} else if (randPnt == 1) {
					trumpParts[randLvl].transform.localScale += new Vector3 ((float)lvlInc * Random.Range (0f, (float)(lvlInc + 1)), (float)lvlInc * Random.Range (0f, (float)(lvlInc + 1)), (float) lvlInc * Random.Range (0f, (float)(lvlInc + 1)));
				} else {
					trumpParts[randLvl].GetComponent<MeshRenderer> ().material.color = new Color (Random.Range (0f, 1f), Random.Range (0f, 1f), Random.Range (0f, 1f));
				}
			}

			if (lvlInc >= 5 && Random.Range (minRand, maxRand) >= (maxRand + minRand) / 2) {
				cam.clearFlags = CameraClearFlags.SolidColor;

				cam.backgroundColor = new Color (Random.Range (0f, 1f), Random.Range (0f, 1f), Random.Range (0f, 1f));
			}
		}
	}
}
