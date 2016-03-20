using UnityEngine;
using System.Collections;

public class Enemy : MovingObj {
    
    public int playerDamage;
    
    private Animator animator;
    public bool skipMove = false;// cant understand
    private Transform target;
    
    private Player p;
    
	// Use this for initialization
	protected override void Awake () {
        GameMgr.instance.AddEnemyToList(this);
	    animator = GetComponent<Animator>();
        
        base.Awake();	
	}
    protected override void Start(){
        target = GameObject.FindGameObjectWithTag("Player").transform;
    }
    
    protected override void AttemptMove <T> (int xDir, int yDir){
        if(skipMove){
            skipMove = false;
            return;
        }
        base.AttemptMove <T> (xDir,yDir);
        
        skipMove = true;
        GameMgr.instance.playersTurn = true;
    }
    // void Update (){
    //     MoveEnemy();
    // }
    
    public void MoveEnemy(){
        int xDir = 0;
        int yDir = 0;
        if(Mathf.Abs(target.position.x - transform.position.x) < 1/*float.Epsilon*/){
             yDir = target.position.y > transform.position.y ? 1: -1;
            //  Debug.Log(yDir);
        }else
        {
            xDir = target.position.x > transform.position.x ? 1: -1;
        }
        AttemptMove <Player> (xDir,yDir);
    }
    
    protected override void OnCantMove <T> (T component){
        Player hitPlayer = component as Player;
        animator.SetTrigger("enemyAtk");
        hitPlayer.LoseFood(this.playerDamage);
        target.Translate(-1f, 0f, 0f);
    }
}
