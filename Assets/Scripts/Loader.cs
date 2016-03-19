using UnityEngine;
using System.Collections;

public class Loader : MonoBehaviour {
    public GameObject gamemgr;
    
	// Use this for initialization
	void Awake () {
	    if(GameMgr.instance == null){
            Instantiate(gamemgr);
        }
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
