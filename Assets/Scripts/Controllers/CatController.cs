using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using System;

[RequireComponent(typeof(Rigidbody2D), typeof(Collider2D))]
public class CatController : MonoBehaviour {
	public bool mobilePad = false;
	public float speed = 10.0f;
	public float jumpVelocity = 10.0f;
	public enum Axe {
		VERTICAL,
		HORIZONTAL,
		SLEEP,
		JUMP,
		PROBE,
		ATTACK
	}

	[Disabled]
	public bool disableJumping;
	public bool isOnGround;

	private Rigidbody2D catRigidbody;
	private float height;

	public string axeToString(Axe anAxe) {
		return anAxe.ToString().ToLower();
	}

	public Axe stringToAxe(string aString) {
		foreach(Axe anAxe in Enum.GetValues(typeof(Axe))) {
			if(aString == anAxe.ToString().ToLower()) {
				return anAxe;
			}
		}
		throw new Exception("Axe not found, remember that you have to use lowercase only!");
	}

	public Dictionary<Axe, float> axes;

	public void Awake() {
		axes = new Dictionary<Axe, float>();
		foreach(Axe anAxe in Enum.GetValues(typeof(Axe))) {
			// Init all axes and zero em out
			axes[anAxe] = 0;
		}
		catRigidbody = GetComponent<Rigidbody2D>();
		height = GetComponent<Collider2D>().bounds.extents.y;
	}

	void Update() {
		if(!mobilePad) {
			foreach(Axe anAxe in Enum.GetValues(typeof(Axe))) {
				axes[anAxe] = Mathf.Clamp(Input.GetAxis(axeToString(anAxe)), -1, 1);
			}
		}
	}

	void FixedUpdate() {
		float horizontalSpeed = axes[Axe.HORIZONTAL] * speed;
		catRigidbody.velocity = new Vector2(horizontalSpeed, catRigidbody.velocity.y);
		if(axes[Axe.JUMP] != 0 && !disableJumping && isOnGround) {
			catRigidbody.AddForce(new Vector2(0, jumpVelocity), ForceMode2D.Impulse);
			disableJumping = true;
			StartCoroutine(enableJumping());
		}
	}

	public IEnumerator enableJumping() {
		yield return new WaitForSeconds(0.5f);
		disableJumping = false;
	}
}