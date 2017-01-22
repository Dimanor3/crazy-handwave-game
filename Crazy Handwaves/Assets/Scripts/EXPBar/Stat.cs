using UnityEngine;
using System.Collections;
using System;

[Serializable]
public class Stat {
	[SerializeField] private BarScript bar;

	[SerializeField] private float currentVal, inMin, inMax;

	public float CurrentVal {
		get { 
			return currentVal;
		}

		set {
			this.currentVal = value;
			bar.Value = currentVal;
		}
	}

	public float InMin {
		get { 
			return inMin;
		}

		set { 
			this.inMin = value;
			bar.InMin = inMin;
		}
	}

	public float InMax {
		get { 
			return inMax;
		}

		set { 
			this.inMax = value;
			bar.InMax = inMax;
		}
	}
}
