using UnityEngine;
using System.Collections;

public class LevelLooper : MonoBehaviour
{
	public GameObject otherLooper;
	public enum locationType
	{
		left,
		right,
		up,
		down
	}
	public locationType screenLocation;

	void OnDrawGizmosSelected ()
	{
		Vector2 newLoopedPos = Vector2.zero;
		if (screenLocation == locationType.left)
			newLoopedPos = new Vector2 (otherLooper.transform.position.x - 1, otherLooper.transform.position.y);
		if (screenLocation == locationType.right)
			newLoopedPos = new Vector2 (otherLooper.transform.position.x + 1, otherLooper.transform.position.y);
		if (screenLocation == locationType.up)
			newLoopedPos = new Vector2 (otherLooper.transform.position.x, otherLooper.transform.position.y + 1);
		if (screenLocation == locationType.down)
			newLoopedPos = new Vector2 (otherLooper.transform.position.x, otherLooper.transform.position.y - 1);
		Gizmos.color = Color.blue;
		Gizmos.DrawSphere (transform.position, 0.25f);
	}

	void OnTriggerEnter2D (Collider2D other)
	{
		if (other.gameObject.tag == "Player" || other.gameObject.tag == "Shell") {
			Vector2 newLoopedPos = Vector2.zero;
			if (screenLocation == locationType.left)
				newLoopedPos = new Vector2 (otherLooper.transform.position.x - 1, other.transform.position.y);
			if (screenLocation == locationType.right)
				newLoopedPos = new Vector2 (otherLooper.transform.position.x + 1, other.transform.position.y);
			if (screenLocation == locationType.up)
				newLoopedPos = new Vector2 (other.transform.position.x, otherLooper.transform.position.y + 1);
			if (screenLocation == locationType.down)
				newLoopedPos = new Vector2 (other.transform.position.x, otherLooper.transform.position.y - 1);
			other.gameObject.transform.position = newLoopedPos;
		}

	}
}
