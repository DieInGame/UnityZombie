﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Player : MovingObj {
    
    public int brickDamage = 1;
    public int pointPerFood = 10;
    public int pointPerSoda = 20;
    public float restartDelay = 1f;
    
    private Animator animator;
    private int food;
    
    public Text foodText;
	// Use this for initialization
	protected override void Awake () {
	    animator = GetComponent<Animator>();
        //food = GameMgr.instance.playerFoodPoints;
        foodText.text = "Food :" +food;
        
        base.Awake();
	}
	
    protected override void Start(){
        food = GameMgr.instance.playerFoodPoints;
    }
    
    private void OnDisable(){
        GameMgr.instance.playerFoodPoints = food;
    } 
	// Update is called once per frame
	void Update () {
        foodText.text = "Food :" +food;
	    if(!GameMgr.instance.playersTurn) return;
        int vertical =0;
        int horizontal=0;
        horizontal = (int) Input.GetAxisRaw("Horizontal");
        vertical  = (int) Input.GetAxisRaw("Vertical");
        
        if(horizontal!=0) vertical=0;
        if(horizontal!=0 || vertical !=0){
            AttemptMove<Brick>(horizontal,vertical);
        }
	}
    
    protected override void AttemptMove <T>(int xDir,int yDir){
        food --;
        base.AttemptMove <T> (xDir,yDir);
        // RaycastHit2D hit;
        CheckIfGameOver();
        GameMgr.instance.playersTurn = false;
        
    }
    
    private void OnTriggerEnter2D (Collider2D other){
        // when meet obj marked as isTrigger
        if(other.tag == "Exit"){
            Invoke("Restart",restartDelay);
            enabled = false;
            
        }
        if(other.tag == "Food"){
            food += pointPerFood;
            other.gameObject.SetActive(false);
        }
        if(other.tag == "Soda"){
            food += pointPerSoda;
            other.gameObject.SetActive(false);
        }
    }
    
    // When meet brick.
    protected override void OnCantMove <T> (T component){
        Brick hitBrick = component as Brick;
        hitBrick.DamageBrick(this.brickDamage);
        animator.SetTrigger("playerChop");
    }
    
    private void Restart(){
        // Application.LoadLevel(Application.loadedLevel);
        SceneManager.LoadScene("sample",LoadSceneMode.Single);
    }
    
    //when meet enemy
    public void LoseFood(int loss){
        food -= loss;
        animator.SetTrigger("playerHit");
        // add back animation here.
        CheckIfGameOver();
    }
    
    private void CheckIfGameOver(){
       if(food <= 0){
           GameMgr.instance.GameOver();
       } 
    }
}
