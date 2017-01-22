using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HandMovement : MonoBehaviour {

	[SerializeField] private Stat exp;

	[SerializeField] private float mouseX, speed, incrementer, pressPause;

	[SerializeField] private int rand, minRand, maxRand, lvlInc, randLvl, randPart, randTransform, div, randLvlUnicorn, randPntUnicorn;

	[SerializeField] private GameObject[] level1, level2, level3, level4, trumpParts, handParts, unicornParts, chess;

	[SerializeField] private Text level;

	[SerializeField] private MusicManager musicManager;

	private Stack<GameObject> saveChanges = new Stack<GameObject> ();

	private GameObject trump, pause, unicorn, surfaceSplash, temp;

	private Rigidbody rb;

	private Camera cam;

	private Vector3 origPos, randPosChng, randLocScaChng;

	[SerializeField] private bool trumpActive, turnCam, paused, pitchDec;

	void Awake () {
		cam = Camera.main;
		rb = gameObject.GetComponent <Rigidbody> ();

		// Collect all required game objects
		level1 = GameObject.FindGameObjectsWithTag ("Level1");
		level2 = GameObject.FindGameObjectsWithTag ("Level2");
		level3 = GameObject.FindGameObjectsWithTag ("Level3");
		level4 = GameObject.FindGameObjectsWithTag ("Level4");
		trump = GameObject.FindGameObjectWithTag ("Trump");
		trumpParts = GameObject.FindGameObjectsWithTag ("TrumpParts");
		pause = GameObject.FindGameObjectWithTag ("Pause");
		unicornParts = GameObject.FindGameObjectsWithTag ("UnicornParts");
		unicorn = GameObject.FindGameObjectWithTag ("Unicorn");
		surfaceSplash = GameObject.FindGameObjectWithTag ("SurfaceSplash");
		chess = GameObject.FindGameObjectsWithTag ("ChessPieces");
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
		exp.CurrentVal = 0;
		exp.InMax = rand;
		exp.InMin = 0;
		level.text = "Level: " + lvlInc;
		randLvlUnicorn = 0;
		randPntUnicorn = 0;
		pitchDec = true;
		randPosChng = Vector3.zero;
		randLocScaChng = Vector3.zero;

		musicManager.EarlyLevels.Play ();
		musicManager.EarlyLevels.loop = true;

		if (unicorn != null)
			unicorn.SetActive (false);

		if (pause != null)
			pause.SetActive (false);

		if (trump != null)
			trump.SetActive (false);

		if (surfaceSplash != null)
			surfaceSplash.SetActive (false);
	}

	// Update is called once per frame
	void Update () {
		pressPause = Input.GetAxis ("PauseGame");

		if (Input.GetKeyDown (KeyCode.Q) && Input.GetKeyDown (KeyCode.A) && Input.GetKeyDown (KeyCode.P)) {
			incrementer += 9999f;
		}

		if (pressPause != 0 && !paused) {
			Time.timeScale = 0f;

			paused = true;

			pause.SetActive (true);
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

			exp.CurrentVal = incrementer;

			rb.AddTorque (0f, 0f, mouseX * speed * -1);

			if (turnCam) {
				cam.transform.Rotate (0f, 0f, mouseX * (speed / 3) * -1);
			}
		}

		if (incrementer >= rand) {
			lvlInc++;
			level.text = "Level: " + lvlInc;

			if (pitchDec) {
				musicManager.EarlyLevels.pitch -= .04f;
			} else {
				musicManager.EarlyLevels.pitch += .04f;
			}

			if (musicManager.EarlyLevels.pitch <= 0f) {
				pitchDec = false;
			}

			minRand += Random.Range (1, lvlInc + 1) * lvlInc;
			maxRand += Random.Range (1, lvlInc + 1) * lvlInc;

			rand += Random.Range (minRand, maxRand + 1);

			exp.InMin = exp.InMax;
			exp.InMax = rand;

			if (!trumpActive) {
				randLvl = Random.Range (0, 4);
				randTransform = Random.Range (0, 4);

				if (randTransform >= 0 && randTransform <= 2) {
					randPosChng = new Vector3 ((float)lvlInc * Random.Range (0f, (float)(lvlInc + 1)), (float)lvlInc * Random.Range (0f, (float)(lvlInc + 1)), (float)lvlInc * Random.Range (0f, (float)(lvlInc + 1)));
				} else {
					randLocScaChng = new Vector3 (((float)lvlInc * Random.Range (0f, (float)(lvlInc + 1))) / div, ((float)lvlInc * Random.Range (0f, (float)(lvlInc + 1))) / div, ((float)lvlInc * Random.Range (0f, (float)(lvlInc + 1))) / div);
				}

				int randFing = Random.Range (0, 5);

				if (randLvl == 0) {
					if (randTransform == 0 || randTransform == 1 || randTransform == 2) {
						level1 [randFing].transform.position += randPosChng;
					} else {
						level1 [randFing].transform.localScale += randLocScaChng;
					}
				} else if (randLvl == 1) {
					if (randTransform == 0 || randTransform == 1 || randTransform == 2) {
						level2 [randFing].transform.position += randPosChng;
					} else {
						level2 [randFing].transform.localScale += randLocScaChng;
					}
				} else if (randLvl == 2) {
					if (randTransform == 0 || randTransform == 1 || randTransform == 2) {
						level3 [randFing].transform.position += randPosChng;
					} else {
						level3 [randFing].transform.localScale += randLocScaChng;
					}
				} else {
					if (randTransform == 0 || randTransform == 1 || randTransform == 2) {
						level4 [randFing].transform.position += randPosChng;
					} else {
						level4 [randFing].transform.localScale += randLocScaChng;
					}
				}

				saveChanges.Push (gameObject);

				if (lvlInc >= 10 && Random.Range (minRand, maxRand) >= (maxRand + minRand) * (3 / 4)) {
					trump.SetActive (true);

					trumpActive = true;

					transform.position = new Vector3 (999f, 999f, 999f);
				}
			} else {
				randLvl = Random.Range (0, 16);
				int randPnt = Random.Range (0, 3);

				if (unicorn.activeSelf) {
					randLvlUnicorn = Random.Range (0, 15);
					randPntUnicorn = Random.Range (0, 2);
				}

				Vector3 randRot = new Vector3 (Random.Range (0f, 360f), Random.Range (0f, 360f), Random.Range (0f, 360f));
				Vector3 randLocSca = new Vector3 (((float)lvlInc * Random.Range (0f, (float)(lvlInc + 1))) / div, ((float)lvlInc * Random.Range (0f, (float)(lvlInc + 1))) / div, ((float)lvlInc * Random.Range (0f, (float)(lvlInc + 1))) / div);

				if (randPnt == 0) {
					trumpParts [randLvl].transform.Rotate (randRot);
				} else if (randPnt == 1) {
					trumpParts [randLvl].transform.localScale += randLocSca;
				} else {
					StartCoroutine (trumpColorShifter (trumpParts [randLvl].GetComponent<MeshRenderer> ()));
					//trumpParts[randLvl].GetComponent<MeshRenderer> ().material.color = new Color (Random.Range (0f, 1f), Random.Range (0f, 1f), Random.Range (0f, 1f));
				}

				if (unicorn.activeSelf && randPntUnicorn == 0) {
					unicornParts [randLvlUnicorn].transform.Rotate (randRot);
				} else {
					unicornParts [randLvlUnicorn].transform.localScale += randLocSca;
				}

				if (lvlInc >= 20 && Random.Range (minRand, maxRand) >= (maxRand + minRand) * (6 / 7)) {
					unicorn.SetActive (true);
				}

				saveChanges.Push (trump);

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

			if (lvlInc >= 25 && Random.Range (minRand, maxRand) >= (maxRand + minRand) * (5 / 7)) {
				surfaceSplash.SetActive (true);
			}

			if (lvlInc >= 17 && Random.Range (minRand, maxRand) >= (maxRand + minRand) * (1 / 4)) {
				int randChess = Random.Range (0, 21);

				chess[randChess].GetComponent<expantion> ().expanding ();
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
