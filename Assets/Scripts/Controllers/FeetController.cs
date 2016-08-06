using UnityEngine;

public class FeetController : MonoBehaviour {
	private CatController catController;
	
	public void Start() {
		catController = transform.parent.GetComponent<CatController>();
	}

	void OnTriggerEnter2D() {
		if (catController) {
			catController.isOnGround = true;
		}
		Debug.Log("Is On Ground");
	}
	
	void OnTriggerStay2D() {
		if (catController) {
			catController.isOnGround = true;
		}
	}
	
	void OnTriggerExit2D() {
		if (catController) {
			catController.isOnGround = false;
		}
		Debug.Log("Is not On Ground");
	}
}
