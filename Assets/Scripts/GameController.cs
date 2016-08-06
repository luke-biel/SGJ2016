using System;
using UnityEngine;
using System.Collections.Generic;

public class GameController : MonoBehaviour {
	private List<Demon> demons;

	private static GameController game;
	
	void Awake() {
		if(game != null) {
			throw new Exception("Multiple game objects!");
		}
		game = this;
	}

	public int getNumberOfDemonsInRange(CatController cat) {
		int count = 0;
		foreach(Demon demon in demons) {
			if(cat.interactionRange <= Vector2.Distance(cat.transform.position, demon.transform.position)) {
				count++;
			} 
		}
		return count;
	}
}