using UnityEngine;
using System.Collections;

public class GameMgr : MonoBehaviour {
    // singleton
    public static GameMgr instance = null;
    public BoardMgr bdmgrScript;
    
    // why not set it in player calss?
    public int playerFoodPoints = 100;
    // 
    [HideInInspector] public bool playersTurn = true;
    
    public int level = 3;
    void Awake(){
        if(instance == null){
            instance = this;
        }else if(instance != this){
           Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
        
         bdmgrScript = GetComponent<BoardMgr>();
    }
    
	// Use this for initialization
	void Start () {
	   bdmgrScript.SetupScene(level);
	}
	public void GameOver(){
        enabled = false;
    }
	
}
