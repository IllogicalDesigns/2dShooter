using UnityEngine;
using System.Collections;

public class GunEnemeyAi : MonoBehaviour {
	public GameObject player;
	[Range (0,10)]
	public float speed = 5f;
	public float jumpForce  = 50f;
	public float heightOffGround = 0.1f;
	public bool onGround = false;
	public LayerMask myMask;
	public LayerMask myGunMask;
	private float h = 0f;
	private Rigidbody2D meRid;
	public bool faceRight = true;
	public GameObject gun;
	public Transform rightPos;
	public Transform leftPos;
	public float gunRecoil = 10f;
	public GameObject shell;
	[Range (0,100)]
	public int pistolDMG = 25;
	public float maxspeed = 15f;
	public GameObject flash;
	public AudioClip shootySound;


	// Use this for initialization
	void Start () {
		meRid = gameObject.GetComponent<Rigidbody2D> ();
		Physics2D.IgnoreLayerCollision (8, 9, true);
		flash.SetActive (false);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	IEnumerator FlashGun(){
		flash.SetActive (true);
		yield return new WaitForSeconds (0.01f);
		flash.SetActive (false);
	}
	void FireGun () {
		meRid.AddForce (-(gun.transform.localPosition * gunRecoil), ForceMode2D.Impulse);
		AudioSource.PlayClipAtPoint(shootySound, transform.position, 0.5f);
		StartCoroutine (FlashGun());
		Instantiate (shell, gun.transform.position, Quaternion.identity);
		if (faceRight) {
			RaycastHit2D hit = Physics2D.Raycast (rightPos.position, Vector2.right, Mathf.Infinity, myGunMask);
			Debug.DrawRay (transform.position, Vector2.right * 10f, Color.red);
			if (hit.collider != null) {
				if(hit.collider.tag == "Player" || hit.collider.tag == "Enemy"){
					Health tmpHealth = null;
					tmpHealth = hit.collider.gameObject.GetComponent<Health>();
					if(tmpHealth != null)
						tmpHealth.ApplyDamage(pistolDMG);
					else
						Debug.Log("tmpHealthNull " + hit.collider.name);
				}
			}
		} else {
			RaycastHit2D hit = Physics2D.Raycast (leftPos.position, -Vector2.right, Mathf.Infinity, myGunMask);
			Debug.DrawRay (transform.position, -Vector2.right * 10f, Color.red);
			if (hit.collider != null) {
				if(hit.collider.tag == "Player" || hit.collider.tag == "Enemy"){
					Health tmpHealth = null;
					tmpHealth = hit.collider.gameObject.GetComponent<Health>();
					if(tmpHealth != null)
						tmpHealth.ApplyDamage(pistolDMG);
					else
						Debug.Log("tmpHealthNull " + hit.collider.name);
				}
			}
		}
	}
}
