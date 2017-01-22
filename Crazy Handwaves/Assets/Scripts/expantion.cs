using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class expantion : MonoBehaviour {
	
	// Update is called once per frame
	public void expanding () {
		StartCoroutine ("expand");
	}

	IEnumerator expand () {
		transform.localScale += new Vector3 (.0005f, .0005f, .0005f);

		yield return new WaitForSeconds (1f);

		StartCoroutine ("expand");
	}
}
