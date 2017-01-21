using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Buttons : MonoBehaviour {

	[SerializeField] private SFXManager sfxMan;

	private GameObject credits, nonCredits, pause;

	void Awake () {
		credits = GameObject.FindGameObjectWithTag ("Credits");
		nonCredits = GameObject.FindGameObjectWithTag ("NonCredits");
		pause = GameObject.FindGameObjectWithTag ("Pause");

		if (sfxMan == null) {
			sfxMan = GameObject.FindGameObjectWithTag ("SFXMan").GetComponent<SFXManager> ();
		}
	}

	void Start () {
		if (credits != null) {
			credits.SetActive (false);
		}
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
	/// Starts the game.
	/// </summary>
	public void PlayGame () {
		playButtonClick ();

		SceneManager.LoadScene ("MainGame");
	}

	/// <summary>
	/// Plays the button click SFX.
	/// </summary>
	private void playButtonClick () {
		if (!sfxMan.ButtonClick.isPlaying) {
			sfxMan.ButtonClick.Play ();
		}
	}
}
