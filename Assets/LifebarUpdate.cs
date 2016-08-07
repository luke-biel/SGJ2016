using UnityEngine;
using UnityEngine.UI;

public class LifebarUpdate : MonoBehaviour {

	void Update () {
		GetComponent<Text>().text = (int)((DemonController.granLife / 1000) * 100) + "%";
	}
}
