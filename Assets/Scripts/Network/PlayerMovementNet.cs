using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class PlayerMovementNet : NetworkBehaviour
{
	[Range (0,20)]
	public float playerSpeed = 5f;
	public float jumpForce  = 50f;
	public float heightOffGround = 0.1f;
	private bool onGround = false;
	public LayerMask myMask;
	public LayerMask myGunMask;
	private float h = 0f;
	private Rigidbody2D meRid;
	private bool faceRight = true;
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
	[Range (0,2)]
	public float fireRate = 0.5f;
	private float timeSinceFire = 0f;
	public string horizontalName = "Horizontal";
	public string jumpName = "Jump";
	public string fireName = "Fire";
	public Animator myAnimin;
	
	// Use this for initialization
	void Start ()
	{
		meRid = gameObject.GetComponent<Rigidbody2D> ();
		Physics2D.IgnoreLayerCollision (8, 9, true);
		flash.SetActive (false);
		//horizontalName = "Horizontal" + gameObject.name;
		//fireName = "Fire" + gameObject.name;
		//jumpName = "Jump" + gameObject.name;
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (!isLocalPlayer)
			return;

		if (timeSinceFire > 0)
			timeSinceFire -= Time.deltaTime;
		RaycastHit2D hit = Physics2D.Raycast(transform.position, -Vector2.up, heightOffGround, myMask);
		if (hit.collider != null) {
			if (hit.collider.tag == "ground") {
				//if (Physics2D.Raycast (transform.position, -Vector2.up, heightOffGround)) {
				Debug.DrawRay (transform.position, Vector3.down * heightOffGround, Color.green);
				onGround = true;
			} else if (hit.collider.tag != "Player") {
				onGround = false;
				Debug.DrawRay (transform.position, Vector3.down * heightOffGround, Color.red);
			}
		}
		h = Input.GetAxis (horizontalName);
		if (h > 0)
			faceRight = true;
		if (h < 0)
			faceRight = false;
		if (Input.GetButton (fireName) && (timeSinceFire <= 0f))
			CmdFireGun ();
		if (onGround && Input.GetButtonDown(jumpName)) {
			Jump();
		}
		
	}
	
	void FixedUpdate ()
	{
		if (!isLocalPlayer)
			return;

		if (meRid.velocity.magnitude > maxspeed) 
			meRid.velocity = Vector2.ClampMagnitude (meRid.velocity, maxspeed);
		meRid.AddRelativeForce (Vector2.right * playerSpeed * h);
		if (faceRight) {
			/*if(gun.transform.position != rightPos.position)
				gun.transform.position = rightPos.position;
			if(gun.transform.rotation != rightPos.rotation)
				gun.transform.rotation = rightPos.rotation;*/
			myAnimin.SetBool("FaceRight", true);
			
		} else {
			/*if(gun.transform.position != leftPos.position)
				gun.transform.position = leftPos.position;
			if(gun.transform.rotation != leftPos.rotation)
				gun.transform.rotation = leftPos.rotation;*/
			myAnimin.SetBool("FaceRight", false);
		}
		
	}
	IEnumerator FlashGun(){
		flash.SetActive (true);
		yield return new WaitForSeconds (0.01f);
		flash.SetActive (false);
	}
	void Jump () {
		meRid.AddForce (jumpForce * Vector2.up, ForceMode2D.Impulse);
		onGround = false;
	}
	[Command]
	void CmdFireGun () {
		timeSinceFire = fireRate;
		meRid.AddForce (-(gun.transform.localPosition * gunRecoil), ForceMode2D.Impulse);
		AudioSource.PlayClipAtPoint(shootySound, transform.position, 0.5f);
		//StartCoroutine (FlashGun());
		myAnimin.SetTrigger ("Fire");
		GameObject tmpHell = (GameObject)Instantiate(shell, gun.transform.position, Quaternion.identity);
		NetworkServer.Spawn(tmpHell);
		if (faceRight) {
			RaycastHit2D hit = Physics2D.Raycast (rightPos.position, Vector2.right, Mathf.Infinity, myGunMask);
			Debug.DrawRay (transform.position, Vector2.right * 100f, Color.red);
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
					HealthNet tmpHealth = null;
					tmpHealth = hit.collider.gameObject.GetComponent<HealthNet>();
					if(tmpHealth != null)
						tmpHealth.ApplyDamage(pistolDMG);
					else
						Debug.Log("tmpHealthNull " + hit.collider.name);
				}
			}
		}
	}
}

