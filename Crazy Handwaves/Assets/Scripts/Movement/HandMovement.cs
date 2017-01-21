using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandMovement : MonoBehaviour {

	[SerializeField] private float mouseX, speed, incrementer, pressPause;

	[SerializeField] private int rand, minRand, maxRand, lvlInc, randLvl, randPart, randTransform, div;

	[SerializeField] private GameObject[] level1, level2, level3, level4, trumpParts, handParts;

	private GameObject trump, pause;

	private Rigidbody rb;

	private Camera cam;

	private Vector3 origPos;

	[SerializeField] private bool trumpActive, turnCam, paused, pressPauseBool;

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
		turnCam = false;
		origPos = transform.position;
		randTransform = -1;
		div = 10;
		paused = false;
		pressPauseBool = false;

		// Collect all required game objects
		level1 = GameObject.FindGameObjectsWithTag ("Level1");
		level2 = GameObject.FindGameObjectsWithTag ("Level2");
		level3 = GameObject.FindGameObjectsWithTag ("Level3");
		level4 = GameObject.FindGameObjectsWithTag ("Level4");
		trump = GameObject.FindGameObjectWithTag ("Trump");
		trumpParts = GameObject.FindGameObjectsWithTag ("TrumpParts");
		pause = GameObject.FindGameObjectWithTag ("Pause");

		pause.SetActive (false);
		trump.SetActive (false);
	}

	// Update is called once per frame
	void Update () {
		pressPause = Input.GetAxis ("PauseGame");

		if (pressPause != 0 && !paused && !pressPauseBool) {
			Time.timeScale = 0f;

			paused = true;

			pause.SetActive (true);

			pressPauseBool = true;
		}

		if (pressPause != 0 && paused && !pressPauseBool) {
			Time.timeScale = 1f;

			paused = false;

			pause.SetActive (false);

			pressPauseBool = true;
		}

		if (pressPause == 0 && pressPauseBool) {
			pressPauseBool = false;
		}

		if (!pause.activeSelf) {
			paused = false;
		}

		mouseX = Input.GetAxis ("Mouse X");

		if (mouseX != 0) {
			if (mouseX > 0) {
				incrementer += mouseX;
			}

			if (mouseX < 0) {
				incrementer += -mouseX;
			}

			rb.AddTorque (0f, 0f, mouseX * speed * -1);

			if (turnCam) {
				cam.transform.Rotate (0f, 0f, mouseX * (speed / 3) * -1);
			}
		}

		if (Input.GetKeyDown (KeyCode.A)) {
			turnCam = true;
		}

		if (incrementer >= rand) {
			lvlInc++;

			minRand += Random.Range (1, lvlInc + 1) * lvlInc;
			maxRand += Random.Range (1, lvlInc + 1) * lvlInc;

			rand += Random.Range (minRand, maxRand + 1);

			if (!trumpActive) {
				randLvl = Random.Range (0, 4);
				randTransform = Random.Range (0, 3);

				if (randLvl == 0) {
					if (randTransform == 0 || randTransform == 1) {
						print ("WORKING1");
						level1 [Random.Range (0, 5)].transform.position += new Vector3 ((float)lvlInc * Random.Range (0f, (float)(lvlInc + 1)), (float)lvlInc * Random.Range (0f, (float)(lvlInc + 1)), (float)lvlInc * Random.Range (0f, (float)(lvlInc + 1)));
					} else {
						level1 [Random.Range (0, 5)].transform.localScale += new Vector3 (((float) lvlInc * Random.Range (0f, (float)(lvlInc + 1))) / div, ((float) lvlInc * Random.Range (0f, (float)(lvlInc + 1))) / div, ((float) lvlInc * Random.Range (0f, (float)(lvlInc + 1))) / div);
					}
				} else if (randLvl == 1) {
					if (randTransform == 0 || randTransform == 1) {
						print ("WORKING2");
						level2 [Random.Range (0, 5)].transform.position += new Vector3 ((float)lvlInc * Random.Range (0f, (float)(lvlInc + 1)), (float)lvlInc * Random.Range (0f, (float)(lvlInc + 1)), (float)lvlInc * Random.Range (0f, (float)(lvlInc + 1)));
					} else {
						level2 [Random.Range (0, 5)].transform.localScale += new Vector3 (((float) lvlInc * Random.Range (0f, (float)(lvlInc + 1))) / div, ((float) lvlInc * Random.Range (0f, (float)(lvlInc + 1))) / div, ((float) lvlInc * Random.Range (0f, (float)(lvlInc + 1))) / div);
					}
				} else if (randLvl == 2) {
					if (randTransform == 0 || randTransform == 1) {
						print ("WORKING3");
						level3 [Random.Range (0, 5)].transform.position += new Vector3 ((float)lvlInc * Random.Range (0f, (float)(lvlInc + 1)), (float)lvlInc * Random.Range (0f, (float)(lvlInc + 1)), (float)lvlInc * Random.Range (0f, (float)(lvlInc + 1)));
					} else {
						level3 [Random.Range (0, 5)].transform.localScale += new Vector3 (((float) lvlInc * Random.Range (0f, (float)(lvlInc + 1))) / div, ((float) lvlInc * Random.Range (0f, (float)(lvlInc + 1))) / div, ((float) lvlInc * Random.Range (0f, (float)(lvlInc + 1))) / div);
					}
				} else {
					if (randTransform == 0 || randTransform == 1) {
						print ("WORKING4");
						level4 [Random.Range (0, 5)].transform.position += new Vector3 ((float)lvlInc * Random.Range (0f, (float)(lvlInc + 1)), (float)lvlInc * Random.Range (0f, (float)(lvlInc + 1)), (float)lvlInc * Random.Range (0f, (float)(lvlInc + 1)));
					} else {
						level4 [Random.Range (0, 5)].transform.localScale += new Vector3 (((float) lvlInc * Random.Range (0f, (float)(lvlInc + 1))) / div, ((float) lvlInc * Random.Range (0f, (float)(lvlInc + 1))) / div, ((float) lvlInc * Random.Range (0f, (float)(lvlInc + 1))) / div);
					}
				}

				if (lvlInc >= 10 && Random.Range (minRand, maxRand) >= (maxRand + minRand) * (3 / 4)) {
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
					trumpParts[randLvl].transform.localScale += new Vector3 (((float) lvlInc * Random.Range (0f, (float)(lvlInc + 1))) / div, ((float) lvlInc * Random.Range (0f, (float)(lvlInc + 1))) / div, ((float) lvlInc * Random.Range (0f, (float)(lvlInc + 1))) / div);
				} else {
					StartCoroutine (trumpColorShifter (trumpParts[randLvl].GetComponent<MeshRenderer> ()));
					//trumpParts[randLvl].GetComponent<MeshRenderer> ().material.color = new Color (Random.Range (0f, 1f), Random.Range (0f, 1f), Random.Range (0f, 1f));
				}

				if (lvlInc >= 12 && Random.Range (minRand, maxRand) >= (maxRand + minRand) * (3 / 4)) {
					trump.SetActive (false);

					trumpActive = false;

					transform.position = origPos;
				}
			}

			if (lvlInc >= 5 && Random.Range (minRand, maxRand) >= (maxRand + minRand) / 2) {
				cam.clearFlags = CameraClearFlags.SolidColor;

				StartCoroutine ("bgColorShifter");
			}

			if (lvlInc >= 15 && Random.Range (minRand, maxRand) >= (maxRand + minRand) * (5 / 6) && !turnCam) {
				turnCam = true;
			}
		}
	}

	/// <summary>
	/// Inverts the inputted color.
	/// </summary>
	/// <returns>The inverted color.</returns>
	/// <param name="color">Color to be inverted.</param>
	private Color invertColor (Color color) {
		return new Color (1f - color.r, 1f - color.g, 1f - color.b, 1);
	}

	IEnumerator trumpColorShifter (MeshRenderer mR) {
		Color trumpColor;

		trumpColor = new Color (Random.Range (0f, 1f), Random.Range (0f, 1f), Random.Range (0f, 1f));

		float t = 0f;

		while (t < 1f) {
			mR.material.color = Color.Lerp (trumpColor, invertColor (trumpColor), t);
			yield return new WaitForSeconds (.0001f);
			t += Time.deltaTime;
		}

		if (t >= 1f) {
			StartCoroutine (trumpColorShifter (mR));
		}
	}

	/// <summary>
	/// Shifts background color.
	/// </summary>
	/// <returns>Nothing.</returns>
	IEnumerator bgColorShifter () {
		Color bgColor;

		bgColor = new Color (Random.Range (0f, 1f), Random.Range (0f, 1f), Random.Range (0f, 1f));

		float t = 0f;

		//Color currentColor = cam.backgroundColor;

		while (t < 1f) {
			cam.backgroundColor = Color.Lerp (bgColor, invertColor (bgColor), t);
			yield return new WaitForSeconds (.0001f);
			t += Time.deltaTime;
		}

		if (t >= 1f) {
			StartCoroutine ("bgColorShifter");
		}
	}
}
