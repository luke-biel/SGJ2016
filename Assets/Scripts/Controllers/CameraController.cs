using System;
using UnityEngine;

public class CameraController : MonoBehaviour {

	private float xMin, yMin;
	public float xMax, yMax;

	public float dampTime = 0.15f;
	private Vector3 velocity = Vector3.zero;
	public Transform target;

	private float vertExtent, horzExtent;

	public void Start() {
		if (target == null) {
			Debug.Break();
			throw new Exception("No target for camera set!");
		}
		vertExtent = Camera.main.orthographicSize;
		horzExtent = vertExtent * Screen.width / Screen.height;
	}

	void Update() {
		if (target) {
			Vector3 point = Camera.main.WorldToViewportPoint(target.position);
			Vector3 delta = target.position - Camera.main.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, point.z));
			Vector3 destination = transform.position + delta;
			transform.position = Vector3.SmoothDamp(transform.position, destination, ref velocity, dampTime);
		}
	}

	void LateUpdate() {
		Vector3 v3 = transform.position;
		v3.x = Mathf.Clamp(v3.x, xMin + horzExtent, xMax - horzExtent);
		v3.y = Mathf.Clamp(v3.y, yMin + vertExtent, yMax - vertExtent);
		transform.position = v3;
	}
}