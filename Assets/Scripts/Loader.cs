using UnityEngine;
using System.Collections;

public class Loader : MonoBehaviour {

	public GameObject game_mgr;

	// Use this for initialization
	void Awake () {
		if (GameManager.instance == null) {
			Instantiate(game_mgr);
		}
	}
}
