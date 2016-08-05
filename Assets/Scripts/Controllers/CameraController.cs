using System;
using System.Linq;
using UnityEngine;

public class CameraController : MonoBehaviour {
	private float xMin = Mathf.Infinity, xMax = -Mathf.Infinity, yMin = Mathf.Infinity, yMax = -Mathf.Infinity;

	public int[] allowedLayers;

	public float dampTime = 0.15f;
	private Vector3 velocity = Vector3.zero;
	public Transform target;

	public void Start() {
		if(allowedLayers == null) {
			allowedLayers = new int[0];
		}
		allowedLayers.Concat(new int[] {LayerMask.NameToLayer("Static Environment")});
		foreach(GameObject go in UnityEngine.Object.FindObjectsOfType<GameObject>()) {
			Renderer renderer = go.GetComponent<Renderer>();
			if(renderer || go.layer.isOneOf(allowedLayers)) {
				Bounds bounds = renderer.bounds;
				if(xMin > bounds.min.x) {
					xMin = bounds.min.x;
				} else if(xMax < bounds.max.x) {
					xMax = bounds.max.x;
				}

				if(yMin > bounds.min.y) {
					yMin = bounds.min.y;
				} else if(yMax < bounds.max.y) {
					yMax = bounds.max.y;
				}
			}
		}

		float vertExtent = Camera.main.orthographicSize;    
		float horzExtent = vertExtent * Screen.width / Screen.height;

		xMin += horzExtent;
		xMax -= horzExtent;
		yMin += vertExtent;
		yMax -= vertExtent;

		Debug.LogFormat("Bounds set to x: {0}, {1}; y: {2}, {3};", xMin, xMax, yMin, yMax);

		if(target == null) {
			Debug.Break();
			throw new Exception("No target for camera set!");
		}
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