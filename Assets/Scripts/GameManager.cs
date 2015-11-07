using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {
	public static GameManager instance = null;//to make sure there is only 1
	private BoardManager board_mgr;

	public int level;
	// Use this for initialization
	void Awake () {
		if (instance == null) {
			instance = this;
		} else if(instance != this) {
			Destroy(gameObject);
		}
		DontDestroyOnLoad (gameObject);//here gameobject means this
		this.board_mgr = GetComponent<BoardManager> ();
		InitGame ();
	}

	void InitGame(){
		this.board_mgr.SetupScene (this.level);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
