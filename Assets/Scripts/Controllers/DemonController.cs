using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class DemonController : MonoBehaviour {
	private Demon[] demons;
	public float delayBetweenSpawns = 5;
	public int wave = 3;

	[Disabled]
	public bool isGameInProgress = true; 

	public static float granLife;

	public void Start() {
		demons = GameObject.FindObjectsOfType<Demon>();
		StartCoroutine(spawnTimer());
		granLife = 1000;
	}

	public void spawnDemons() {
		if(!isGameInProgress) {
			return;
		}
		List<Demon> notPossesedItems = getNotPossesedItems().ToList();
		Random rand = new Random();
		for(int i = 0; i < notPossesedItems.Count && i < wave; i++) {
			int x = Random.Range(0, notPossesedItems.Count - 1);
			try {
				Demon d = notPossesedItems[x];
				notPossesedItems.RemoveAt(x);
				d.posses();
			} catch {
				i--;
			}
		}
		StartCoroutine(spawnTimer());
	}

	public IEnumerable<Demon> getPossesedItems() {
		return demons.Where((demon) => {
			return demon.isPossesed;
		});
	}

	public IEnumerable<Demon> getNotPossesedItems() {
		return demons.Where((demon) => {
			return !demon.isPossesed;
		});
	}

	public IEnumerator spawnTimer() {
		yield return new WaitForSeconds(delayBetweenSpawns);
		spawnDemons();
	}

	public static void drainGransLife(float amt) {
		//granLife -= amt;
		if(granLife <= 0) {
			GameObject.Find("Camera").GetComponent<DemonController>().gameOver();
		}
	}

	public void gameOver() {
		GameObject go = Instantiate(Resources.Load<GameObject>("nicky"));
		go.transform.position = GameObject.Find("Cat").transform.position;
		Time.timeScale = 0;
	}
}