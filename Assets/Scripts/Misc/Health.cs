using UnityEngine;
using System.Collections;

public class Health : MonoBehaviour
{
	[Range (0,100)]
	public int
		health = 100;
	public GameObject deadReplacement;
	public float timescaleDeath = 0.6f;
	public float timeSloMo = 1f;
	// Use this for initialization
	void Start ()
	{
	
	}

	IEnumerator death ()
	{
		Time.timeScale = timescaleDeath;
		Rigidbody2D tmpRidg = gameObject.GetComponent<Rigidbody2D> ();
		if (tmpRidg != null) {
			tmpRidg.AddForce (Vector2.up * 5f, ForceMode2D.Impulse);
		}
		yield return new WaitForSeconds (timeSloMo);
		Time.timeScale = 1f;
		gameObject.name = "dead";
		Instantiate (deadReplacement, transform.position, Quaternion.identity);
		Destroy (gameObject);
	}
	// Update is called once per frame
	void Update ()
	{
		if (health <= 0) {
			StartCoroutine (death ());
			health = 1000000000;
		}
	}

	public void ApplyDamage (int DMG)
	{
		health = health - DMG;
	}
}
