using UnityEngine;
using System.Collections;

public class shell : MonoBehaviour
{
	public ConstantForce2D myForce;
	private Rigidbody2D myRid;
	public int lifetime = 1;
	public bool destroyThis = false;

	// Use this for initialization
	void Start ()
	{
		myRid = gameObject.GetComponent<Rigidbody2D> ();
		myRid.AddForce (Vector2.up * 5f, ForceMode2D.Impulse);
		Destroy (myForce, lifetime);
		if (destroyThis)
			Destroy (gameObject, lifetime * 10);
	}
	
	// Update is called once per frame
	void Update ()
	{
	
	}
}
