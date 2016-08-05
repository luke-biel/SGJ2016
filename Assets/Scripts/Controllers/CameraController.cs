using System;
using UnityEngine;

public class CameraController : MonoBehaviour {

	public float xMap = 100f, yMap = 100f;

	private float xMin, xMax, yMin, yMax;

	public float dampTime = 0.15f;
	private Vector3 velocity = Vector3.zero;
	public Transform target;

	public void Start() {
		if(target == null) {
			Debug.Break();
			throw new Exception("No target for camera set!");
		}
		float vertExtent = Camera.main.orthographicSize;    
		float horzExtent = vertExtent * Screen.width / Screen.height;

		xMin = horzExtent - xMap / 2.0f;
		xMax = xMap / 2.0f - horzExtent;
		yMin = vertExtent - yMap / 2.0f;
		yMax = yMap / 2.0f - vertExtent;
	}

	void Update () 
	{
		if (target)
		{
			Vector3 point = Camera.main.WorldToViewportPoint(target.position);
			Vector3 delta = target.position - Camera.main.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, point.z));
			Vector3 destination = transform.position + delta;
			transform.position = Vector3.SmoothDamp(transform.position, destination, ref velocity, dampTime);
		}
	}

	void LateUpdate() {
		Vector3 v3 = transform.position;
        v3.x = Mathf.Clamp(v3.x, xMin, xMax);
        v3.y = Mathf.Clamp(v3.y, yMin, yMax);
        transform.position = v3;
	}
 }