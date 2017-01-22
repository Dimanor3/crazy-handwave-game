using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Buttons : MonoBehaviour {

	[SerializeField] private SFXManager sfxMan;

	private GameObject credits, nonCredits, pause, howToPlay;

	void Awake () {
		credits = GameObject.FindGameObjectWithTag ("Credits");
		nonCredits = GameObject.FindGameObjectWithTag ("NonCredits");
		pause = GameObject.FindGameObjectWithTag ("Pause");
		howToPlay = GameObject.FindGameObjectWithTag ("HowToPlay");
	}

	void Start () {
		if (credits != null) {
			credits.SetActive (false);
		}

		if (howToPlay != null) {
			howToPlay.SetActive (false);
		}

		sfxMan = GameObject.FindGameObjectWithTag ("SFXMan").GetComponent<SFXManager> ();
	}

	/// <summary>
	/// Exits the game.
	/// </summary>
	public void ExitGame () {
		playButtonClick ();

		Application.Quit ();
	}

	/// <summary>
	/// Go to Main Menu.
	/// </summary>
	public void MainMenuButton () {
		playButtonClick ();

		if (credits != null) {
			credits.SetActive (false);
		}

		if (nonCredits != null) {
			nonCredits.SetActive (true);
		}

		if (howToPlay != null) {
			howToPlay.SetActive (false);
		}

		Time.timeScale = 1f;
		SceneManager.LoadScene ("MainMenu");
	}

	/// <summary>
	/// Go to Main Menu from credits.
	/// </summary>
	public void MainMenuButton2 () {
		playButtonClick ();

		if (credits != null) {
			credits.SetActive (false);
		}

		if (nonCredits != null) {
			nonCredits.SetActive (true);
		}

		if (howToPlay != null) {
			howToPlay.SetActive (false);
		}
	}

	/// <summary>
	/// Shows Credits.
	/// </summary>
	public void CreditsButton () {
		playButtonClick ();

		if (credits != null) {
			credits.SetActive (true);
		}

		if (nonCredits != null) {
			nonCredits.SetActive (false);
		}
	}

	/// <summary>
	/// Resumes the game.
	/// </summary>
	public void ResumeGame () {
		playButtonClick ();

		pause.SetActive (false);

		Time.timeScale = 1f;
	}

	/// <summary>
	/// How to play the game.
	/// </summary>
	public void HowToPlay () {
		playButtonClick ();

		if (nonCredits != null) {
			nonCredits.SetActive (false);
		}

		if (howToPlay != null) {
			howToPlay.SetActive (true);
		}
	}

	/// <summary>
	/// Starts the game.
	/// </summary>
	public void PlayGame () {
		playButtonClick ();
		
		SceneManager.LoadScene ("MainGame");
	}

	public void RestartLevel () {
		playButtonClick ();

		StartCoroutine ("restart");

		print (SceneManager.sceneCount);
	}

	/// <summary>
	/// Plays the button click SFX.
	/// </summary>
	private void playButtonClick () {
		if (!sfxMan.ButtonClick.isPlaying) {
			sfxMan.ButtonClick.Play ();
		}
	}

	IEnumerator restart () {
		SceneManager.LoadScene ("MainMenu");

		yield return new WaitForSeconds (1f);

		SceneManager.LoadScene ("MainGame");
	}
}
