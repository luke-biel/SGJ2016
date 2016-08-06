using UnityEngine;
using System.Collections;

public class RotateAround : MonoBehaviour {

	public float speed = 30.0f;
	public Transform pivot;
	public Vector3 Side;

	// Use this for initialization
	void Start () {
		Debug.Log("vector up, " + Vector3.up + ", left: " + Vector3.left);
	}
	
	void Update () {
		float tmpSpeed = speed;
		if (transform.position.y < -211 ) {
			tmpSpeed *= 5;
		}
		
		transform.RotateAround(pivot.position, Side, tmpSpeed * Time.deltaTime);
 }
}
