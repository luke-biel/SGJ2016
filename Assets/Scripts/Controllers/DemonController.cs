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

	public void StartVoid() {
		demons = GameObject.FindObjectsOfType<Demon>();
	}

	public void spawnDemons() {
		if(!isGameInProgress) {
			return;
		}
		List<Demon> notPossesedItems = getNotPossesedItems().ToList();
		for(int i = 0; i < notPossesedItems.Count && i < wave; i++) {
			notPossesedItems[i].posses();
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