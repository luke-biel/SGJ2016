using UnityEngine;
using System.Collections;

public class quit : MonoBehaviour {
	
	void FixedUpdate () {
		if (Input.GetButtonDown ("Cancel"))
			Application.Quit ();
	}
}
