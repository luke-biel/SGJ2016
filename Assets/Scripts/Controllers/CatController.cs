using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using System;

[RequireComponent(typeof(Rigidbody2D), typeof(Collider2D))]
public class CatController : MonoBehaviour {
	const float NEAR_ZERO = 0.01f;
	public enum Axe {
		VERTICAL,
		HORIZONTAL,
		SLEEP,
		JUMP,
		PROBE,
		ATTACK
	}

	public bool mobilePad = false;
	public float speed = 10.0f;
	public float jumpVelocity = 10.0f;

	public float sonarRange = 10.0f;

	[Disabled]
	public bool disableJumping;
	public bool isOnGround;
	private bool lockDrain = false;

	private Rigidbody2D catRigidbody;
	private float height;

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
		setAnimation("falling", catRigidbody.velocity.y);
		setAnimation("running", catRigidbody.velocity.x);
		if(!mobilePad) {
			foreach(Axe anAxe in Enum.GetValues(typeof(Axe))) {
				axes[anAxe] = Mathf.Clamp(Input.GetAxis(axeToString(anAxe)), -1, 1);
			}
		}
		if(axes[Axe.SLEEP] != 0) {
			Demon demon = getClosestPossesedItem();
			if(demon != null) {
				if(!lockDrain) {
					setAnimation(Axe.SLEEP, true);
				}
				demon.drainPower(Time.deltaTime, this);
				lockDrain = true;
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
		Physics2D.IgnoreLayerCollision( 
			LayerMask.NameToLayer("Player"),
            LayerMask.NameToLayer("OneWayPlatform"), 
            !isOnGround || catRigidbody.velocity.y > 0 || axes[Axe.VERTICAL] < 0
        );
	}

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


	public IEnumerator enableJumping() {
		setAnimation(Axe.JUMP, true);
		yield return new WaitForSeconds(0.5f);
		disableJumping = false;
		setAnimation(Axe.JUMP, false);
	}

	private void setAnimation(Axe anAxe, bool? state) {
		GetComponent<Animator>().SetBool(axeToString(anAxe), state != null ? state.Value : Mathf.Abs(axes[anAxe]) > NEAR_ZERO);
	}

	private void setAnimation(string field, float f) {
		GetComponent<Animator>().SetFloat(field, Mathf.Abs(f));
	}

	public Demon getClosestPossesedItem() {
		Demon[] demons = GameObject.FindObjectsOfType<Demon>();
		float distance = Mathf.Infinity;
		Demon choosenOne = null;
		foreach(Demon demon in demons) {
			float dist = Vector2.Distance(demon.transform.position, transform.position);
			if(demon.isPossesed && dist < distance && dist <= sonarRange) {
				choosenOne = demon;
			}
		}
		return choosenOne;
	}

	public void stopSleeping() {
		setAnimation(Axe.SLEEP, false);
		StartCoroutine(releaseDrain());
	}

	public IEnumerator releaseDrain() {
		yield return new WaitForSeconds(0.75f);
		lockDrain = false;
	}
}