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
    // public vars van be edited in Unity
    public int columns = 8;
    public int rows    = 8;
    public Count foodNum  = new Count(1,5);
    public Count brickNum  = new Count(5,9);
    public GameObject[] floorTiles;
    public GameObject[] brickTiles;
    public GameObject[] foodTiles;
    public GameObject[] enemyTiles;
    public GameObject[] outWallTiles;
    public GameObject[] exit;
    
    private Transform boardHandler;
    private List <Vector3> gridPos = new List<Vector3>();
    
    void InitList(){
        gridPos.Clear();
        // watch out here ,Clear does not equal to clear.It must be written in capital
        for(int x =1;x < columns -1 ; x++){
            for(int y =1; y< rows -1; y++){
                gridPos.Add(new Vector3(x,y,0f));
            }
        }
    }
    
    void BoardSetup(){
        boardHandler = new GameObject("Board").transform;
        for(int x =-1;x<columns+1;x++){
            for(int y =-1;y<rows+1;y++){
                GameObject toInstantiate = floorTiles[Random.Range(0, floorTiles.Length)];
                if(x==-1|x==columns|y==-1|y==rows){
                    toInstantiate = outWallTiles[Random.Range(0, outWallTiles.Length)];
                }
                GameObject instance = Instantiate(toInstantiate,new Vector3(x,y,0f),Quaternion.identity) as GameObject;
                instance.transform.SetParent(boardHandler);
            }
        }
        
    }
    // pup a vector3 from the array.
    Vector3 RandomPosition(){
        int randomIndex = Random.Range(0, gridPos.Count);
        Vector3 radomPos= gridPos[randomIndex];
        gridPos.RemoveAt(randomIndex);
        return radomPos;
    }
    // Put a tile 
    void LayoutObjectAtRandom(GameObject[] tileArray,int maximum,int minimum){
        int objCount = Random.Range(minimum,maximum+1);
        for(int i=0;i<objCount;i++){
            Vector3 radomPos = RandomPosition();
            GameObject tileChoice = tileArray[Random.Range(0,tileArray.Length)];
            Instantiate(tileChoice,radomPos,Quaternion.identity);
        }
    }
    public void SetupScene(int level){
        BoardSetup();
        InitList();
        LayoutObjectAtRandom(brickTiles,brickNum.minimum,brickNum.maximum);
        LayoutObjectAtRandom(foodTiles,foodNum.minimum,foodNum.maximum);
        int enemyNum = (int)Mathf.Log(level,2f);
        // Mathf.Log returns a float.
        //notice that enemy's count is raised with the level
        LayoutObjectAtRandom(enemyTiles,enemyNum,enemyNum+1);
        Instantiate(exit[Random.Range(0,exit.Length)],new Vector3(columns-1,rows-1,0f),Quaternion.identity);
         
    }
	// Use this for initialization 

}
