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
	//use 0-8 to create a 9x9 map
	public int columns = 8;
	public int rows	   = 8;
	public Count brick_count = new Count(5,9);
	public Count food_count = new Count(1,5);
	public GameObject exit;
	//the following tiles store different sprites
	public GameObject[] floor_tiles;
	public GameObject[] wall_tiles;//out wall
	public GameObject[] food_tiles;
	public GameObject[] enemy_tiles;
	public GameObject[] brick_tiles;//inner wall

	private Transform board_holder;
	private List<Vector3> grid_positions = new List<Vector3>();
	// Use this for initialization
	void InitializeList(){
		// we want set walls in the centeer 6x6,
		grid_positions.Clear ();
		for (int x=1; x<columns-1; x++) {
			for(int y=1; y<rows-1;y++){
				grid_positions.Add(new Vector3(x,y,0f));
			}
		}
	}
	
	void BoardSetup(){
		board_holder = new GameObject ("Board").transform;
		for (int x = -1; x<columns+1; x++) {
			for(int y = -1;y<rows+1;y++){
				//choose a random sprite as 
				GameObject sprite_to_instantiate = floor_tiles[Random.Range(0,floor_tiles.Length)];
				if(x==-1||x==columns||y==-1||y==rows){
					//is the out wall
					sprite_to_instantiate = wall_tiles[Random.Range(0,wall_tiles.Length)];
				}
				GameObject instance =  Instantiate (sprite_to_instantiate,new Vector3(x,y,0f),Quaternion.identity) as GameObject;
				instance.transform.SetParent(board_holder);
			}
		}
	}

	Vector3 RandomPosition(){
		int random_index = Random.Range (0, grid_positions.Count);
		Vector3 random_position = grid_positions [random_index];
		grid_positions.RemoveAt (random_index);
		return random_position;
	}

	void LayoutObjectRandom(GameObject[] tile_array,int minimum,int maximum){
		//seems like setupBoard
		int obj_count = Random.Range (minimum, maximum + 1);
		for (int i=0; i<obj_count; i++) {
			Vector3 random_position = RandomPosition();
			GameObject tile_choice = tile_array[Random.Range(0,tile_array.Length)];
			Instantiate(tile_choice,random_position,Quaternion.identity);
		}
	}

	public void SetupScene(int level){
		BoardSetup ();//create out walls and floor

		InitializeList ();
		LayoutObjectRandom (brick_tiles, brick_count.minimum, brick_count.maximum);
		LayoutObjectRandom (food_tiles, food_count.minimum, food_count.maximum);
		//number of enemies is determined by level,based on a logarithmic progression
		int enemy_count = (int)Mathf.Log (level, 2f);

		LayoutObjectRandom (enemy_tiles, enemy_count, enemy_count);
		Instantiate (exit, new Vector3 (columns - 1, rows - 1, 0f), Quaternion.identity);
	}
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
