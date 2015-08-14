using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class HealthNet : NetworkBehaviour
{
	[Range (0,100)]
	[SyncVar]
	public int health = 100;

	public GameObject deadReplacement;
	public float timescaleDeath = 0.6f;
	public float timeSloMo = 1f;
	// Use this for initialization
	void Start ()
	{
		
	}
	IEnumerator Death ()
	{
		Time.timeScale = timescaleDeath;
		Rigidbody2D tmpRidg = gameObject.GetComponent<Rigidbody2D> ();
		if (tmpRidg != null) {
			tmpRidg.AddForce (Vector2.up * 5f, ForceMode2D.Impulse);
		}
		yield return new WaitForSeconds (timeSloMo);
		Time.timeScale = 1f;
		gameObject.name = "dead";
		GameObject tmpDead = (GameObject)Instantiate(deadReplacement, transform.position, Quaternion.identity);
		NetworkServer.Spawn(tmpDead);
		Destroy(gameObject);
	}
	// Update is called once per frame
	void Update ()
	{
		if (!isLocalPlayer) {
			return;
		}
		if (health <= 0) {
			StartCoroutine (Death ());
			health = 1000000000;
		}
	}
	
	public void ApplyDamage (int DMG)
	{
		if (!isServer)
			return;

		health = health - DMG;
	}
}
