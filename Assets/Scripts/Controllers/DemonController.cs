using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DemonController : MonoBehaviour {
	private Demon[] demons;
	public float delayBetweenSpawns = 5;
	public int wave = 3;

	[Disabled]
	public bool isGameInProgress = true; 

	public void Start() {
		demons = GameObject.FindObjectsOfType<Demon>();
		StartCoroutine(spawnTimer());
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
}