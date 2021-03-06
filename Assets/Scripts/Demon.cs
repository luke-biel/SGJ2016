using System;
using System.Collections;
using UnityEngine;

public class Demon : MonoBehaviour {
	public bool isPossesed;
	public bool wasPossesed;
	private GameObject smoke;
	[SerializeField, Disabled]
	private float power;
	private float ratio;
	private bool isBeingDrrained = false;

	void Start() {
		if (isPossesed) {
			posses();
		}
	}

	public void Update() {
		if (isPossesed) {
			DemonController.drainGransLife(Time.deltaTime);
		}
	}

	internal void posses() {
		if (wasPossesed) {
			return;
		}
		power = 100.0f;
		ratio = UnityEngine.Random.Range(0.4f, 1.1f);
		isPossesed = true;
		smoke = Instantiate(Resources.Load<GameObject>("WhiteSmoke"));
		float y = GetComponent<SpriteRenderer>().bounds.extents.y;
		smoke.transform.position = new Vector2(transform.position.x, transform.position.y - y);
		smoke.transform.localScale = new Vector3(
			GetComponent<SpriteRenderer>().bounds.extents.x / 6,
			transform.localScale.y,
			transform.localScale.z);
	}

	internal void exorcise() {
		if (smoke != null) {
			Destroy(smoke);
			wasPossesed = true;
		}
		isPossesed = false;
	}

	public void drainPower(float dt, CatController controler) {
		power -= dt * ratio * 10;
		Camera.main.GetComponent<CameraController>().shake = (100 - power) / 1000;
		if (power <= 0) {
			exorcise();
			controler.stopSleeping();
		}
	}

	public void onHit() {
		StartCoroutine(drop(gameObject));
	}

	private IEnumerator drop(GameObject go) {
		yield return null;
		exorcise();
		Destroy(this);
		go.AddComponent<Rigidbody2D>();
		go.AddComponent<BoxCollider2D>();
		go.layer = LayerMask.NameToLayer("Faller");
		Physics2D.IgnoreLayerCollision(go.layer, LayerMask.NameToLayer("OneWayPlatform"), true);
	}

	internal void CatSeesMe() {
	}
}