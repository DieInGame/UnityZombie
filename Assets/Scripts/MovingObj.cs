using UnityEngine;
using System.Collections;

public abstract class MovingObj : MonoBehaviour {
    // pay attention to the difference between abstruct and virtual
    
    public float moveTime = 0.1f;
    public LayerMask blockingLayer;
    
    private BoxCollider2D box;
    private Rigidbody2D rb2D;
    private float inverseMoveTime;
     
    
	// Use this for initialization
	protected virtual void Start () {
        box = GetComponent<BoxCollider2D>();
        rb2D= GetComponent<Rigidbody2D>();
        inverseMoveTime = 1f/moveTime;
	}
	
    protected bool Move (int xDir, int yDir,out RaycastHit2D hit){
        Vector2 start = transform.position;
        Vector2 end   = start + new Vector2(xDir,yDir);
        box.enabled = false;
        
        hit = Physics2D.Linecast (start,end,blockingLayer);
        // this raycast works at layer blocking which holds Bricks.
        box.enabled = true;
        
        if(hit.transform == null){
            StartCoroutine(SmoothMove(end));
            return true;
        }
        return false;
        
    }
    
    protected IEnumerator SmoothMove(Vector3 end){
        // 枚举类型
        float sqrRemainingDist = (transform.position -end).sqrMagnitude;
        while (sqrRemainingDist > float.Epsilon)
        {
            Vector3 newPos = Vector3.MoveTowards (rb2D.position,end, inverseMoveTime*Time.deltaTime);
            rb2D.MovePosition(newPos);
            sqrRemainingDist = (transform.position -end).sqrMagnitude;
            yield return null;
            // notice: yield returns data when it is shown but not break the loop
        }
    }
	
    protected virtual void AttemptMove <T>(int xDir,int yDir)
    where T: Component {//I think it should be GameObject;
        RaycastHit2D hit;
        bool canMove = Move(xDir,yDir,out hit);//Pay attention to the hit
        if(hit.transform == null) return;
        T hitComponent = hit.transform.GetComponent<T>();
        if(!canMove && hitComponent!=null){
            OnCantMove(hitComponent);
        }
    }
    // declare a OnCantMove for differet collisions.p vs enemy,p vs brick/wall
	
    protected abstract void OnCantMove<T> (T component) 
	    where T : Component;
        // this function is abstract,it should not have a function body
}
