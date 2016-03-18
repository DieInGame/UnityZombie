using UnityEngine;
using System;
using System.Collections.Generic;
using Random = UnityEngine.Random;



public class BoardMgr : MonoBehaviour {
    [Serializable]
    public class Count
    {
        public int minimum;
        public int maximum;
        
        public Count(int min,int max){
            minimum = min;
            maximum = max;
        }
        
    }
    
    public int columns = 8;
    public int rows    = 8;
    public Count food  = new Count(1,5);
    public Count wall  = new Count(5,9);
    public GameObject[] floorTiles;
    public GameObject[] brickTiles;
    public GameObject[] enemyTiles;
    public GameObject[] outWallTiles;
    
    private Transform
    
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
