using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BarScript : MonoBehaviour {

	[SerializeField] private float fillAmount;

	[SerializeField] private Image content;

	public float InMin { get; set; }

	public float InMax { get; set; }

	/// <summary>
	/// Sets the value of value.
	/// </summary>
	/// <value>The value.</value>
	public float Value {
		set { 
			fillAmount = Map (value, InMin, InMax, 0, 1);
		}
	}
	
	// Update is called once per frame
	void Update () {
		HandleBar ();
	}

	/// <summary>
	/// Handles the EXPBar.
	/// </summary>
	private void HandleBar () {
		content.fillAmount = fillAmount;
	}

	/// <summary>
	/// Map the value of the stat between 0 and 1.
	/// </summary>
	/// <param name="val">Current value of stat.</param>
	/// <param name="inMin">In minimum.</param>
	/// <param name="inMax">In max.</param>
	/// <param name="outMin">Out minimum (0).</param>
	/// <param name="outMax">Out max (1).</param>
	private float Map (float val, float inMin, float inMax, float outMin, float outMax) {
		return (val - inMin) * (outMax - outMin) / (inMax - inMin) + outMin;
	}
}
