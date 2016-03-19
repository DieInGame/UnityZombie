using UnityEngine;
using System.Collections;

public class Brick : MonoBehaviour {

    public Sprite dmgSprite;//used for condition when player break the bricks
    public int hp = 4;
    private SpriteRenderer spriteRenderer;
	// Use this for initialization
	void Awake () {
	    spriteRenderer= GetComponent<SpriteRenderer>();
        
	}
	
	public void DamageBrick(int loss){
        spriteRenderer.sprite = dmgSprite;
        hp -= loss;
        if (hp <= 0)
        {
            gameObject.SetActive(false);
        }
    }
}
