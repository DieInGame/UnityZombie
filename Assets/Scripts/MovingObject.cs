using UnityEngine;
using System.Collections;

public abstract class MovingObject : MonoBehaviour {

	public float move_time = 0.1f;
	public LayerMask blocking_layer;
	
	private BoxCollider2D  box_collider;
	private Rigidbody2D    rigid_body;
	private float inverse_movetime;//something like distance per interval~speed
	protected virtual void Start () {
		box_collider = GetComponent<BoxCollider2D> ();
		rigid_body = GetComponent<Rigidbody2D> ();
		inverse_movetime = 1f/move_time;
	}

	protected bool Move (int x,int y,out RaycastHit2D hit){
		Vector2 start = transform.position;
		Vector2 end = start + new Vector2 (x, y);
		box_collider.enabled = false;
		hit = Physics2D.Linecast (start, end, blocking_layer);
		box_collider.enabled = true;

		if (hit.transform == null) {
			StartCoroutine (smoothMove (end));
			return true;
		}
		return false;
	}


	protected IEnumerable smoothMove(Vector3 end){
		float sqrRemainingDistance = (transform.position - end).sqrMagnitude;

		while (sqrRemainingDistance > float.Epsilon) {
			Vector3 newPos = Vector3.MoveTowards (rigid_body.position, end, inverse_movetime * Time.deltaTime);
			rigid_body.MovePosition(newPos);
			sqrRemainingDistance = (transform.position-end).sqrMagnitude;
			yield return null;
		}
	}

	protected virtual void attemptMove <T>(int x,int y)
		where T:Component{

	}



	protected abstract void onCantMove <T>(T component)
		where T:Component;
	
	// Update is called once per frame

}
