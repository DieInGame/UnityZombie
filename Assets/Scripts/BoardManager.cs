using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using Random = UnityEngine.Random;

public class BoardManager : MonoBehaviour {

	//serializable to make it easy to store and transmit
	[Serializable]
	public class Count{
		public int minimum;
		public int maximum;

		public Count(int min,int max){
			minimum = min;
			maximum = max;
		}
	}

	public int columns = 8;
	public int rows	   = 8;
	public Count wall_count = new Count(5,9);
	public Count food_count = new Count(1,5);
	public GameObject exit;
	public GameObject[] floor_tiles;
	public GameObject[] wall_tiles;
	public GameObject[] food_tiles;
	public GameObject[] enemy_tiles;
	public GameObject[] brick_tiles;

	private Transform board_holder;
	private List<Vector3> grid_positions = new List<Vector3>();
	// Use this for initialization
	void InitializeList(){
		grid_positions.Clear ();
		for (int x=1; x<columns-1; x++) {
			for(int y=1; y<rows-1;y++){
				grid_positions.Add(new Vector3(x,y,0f));
			}
		}
	}

	void BoardStep(){
		board_holder = new GameObject ("Board").transform;
	
	}
				                
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
