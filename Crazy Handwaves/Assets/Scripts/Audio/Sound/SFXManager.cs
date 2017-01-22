using UnityEngine;
using System.Collections;

public class SFXManager : MonoBehaviour {

	// List of AudioSources
	[SerializeField] private AudioSource buttonClick, commercial;

	// Used to check if the SFX Manager already exists
	public static SFXManager sfxManagerExists;

	// Use this for initialization
	void Awake () {
		// If the SFX Manager doesn't exist make
		// it exist and don't destroy it on load
		// otherwise destroy it.
		if (sfxManagerExists == null) {
			DontDestroyOnLoad (transform.gameObject);
			sfxManagerExists = this;
		} else {
			if (sfxManagerExists != this) {
				Destroy (gameObject);
			}
		}
	}

	/// <summary>
	/// Plays the button click sound effect.
	/// </summary>
	/// <returns>The button click sound effect.</returns>
	public AudioSource ButtonClick {
		get {
			return buttonClick;
		}
	}

	public AudioSource Commercial {
		get { 
			return commercial;
		}
	}
}