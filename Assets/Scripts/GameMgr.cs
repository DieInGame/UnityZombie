using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class GameMgr : MonoBehaviour {
    
    // singleton
    public static GameMgr instance = null;
    public BoardMgr bdmgrScript;
    
    // why not set it in player calss?
    public int playerFoodPoints = 100;
    // 
    [HideInInspector] public bool playersTurn = true;
    public float turnDelay =.1f;
    public float levelStartDelay = 2f;
    private int level = 3;
    
    private Text levelText;
    private GameObject levelImage;
    private bool doingSetup;
    private List <Enemy> enemies = new List<Enemy>();
    private bool enemyIsMoving;
   
    void Awake(){
        if(instance == null){
            instance = this;
        }else if(instance != this){
           Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
        
         bdmgrScript = GetComponent<BoardMgr>();
         InitGame();
    }
    
    // It will be Obsolete
    // void OnLevelWasLoaded(int index){
    //     level ++;
    //     Debug.Log(level);
    //     InitGame();
    // }
   
    
	// Use this for initialization
	void InitGame () {
     
        doingSetup = true;
        levelImage = GameObject.Find("Image");
        levelText  = GameObject.Find("LvText").GetComponent<Text>();
        levelText.text = "Day :" + level;
        levelImage.SetActive(true);
        Invoke("HideLevelImg",levelStartDelay);
        
       enemies.Clear();
	   bdmgrScript.SetupScene(level);
       
	}
    
    private void HideLevelImg(){
        levelImage.SetActive(false);
        doingSetup = false;
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
