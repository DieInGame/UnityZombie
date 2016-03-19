using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameMgr : MonoBehaviour {
    // singleton
    public static GameMgr instance = null;
    public BoardMgr bdmgrScript;
    
    // why not set it in player calss?
    public int playerFoodPoints = 100;
    // 
    [HideInInspector] public bool playersTurn = true;
    public float turnDelay =.1f;
    
    public int level = 3;
    
    private List<Enemy> enemies = new List<Enemy>();
    private bool enemyIsMoving;
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
       enemies.Clear();
	   bdmgrScript.SetupScene(level);
	}
	public void GameOver(){
        enabled = false;
    }
    
    void Update(){
        if(playersTurn || enemyIsMoving)
        return;
        StartCoroutine(MoveEnemies());
    }
    
    public void AddEnemyToList(Enemy e){
        enemies.Add(e);
    }
	
    IEnumerator MoveEnemies(){
        enemyIsMoving = true;
        yield return new WaitForSeconds(turnDelay);
        if (enemies.Count == 0){
            yield return new WaitForSeconds(turnDelay);
        }
        for (int i = 0; i < enemies.Count; i++)
        {
            enemies[i].MoveEnemy();
            yield return new WaitForSeconds(enemies[i].moveTime);
        }
        // playersTurn = true;
         enemyIsMoving = false;
    }
}
