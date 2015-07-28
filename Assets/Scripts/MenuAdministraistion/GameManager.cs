using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class GameManager : MonoBehaviour {
	public List<GameObject> respawnPoints = new List<GameObject> ();
	public GameObject[] playerPrefabs;
	public GameObject[] enables;//0=endSlate
	public Text[] textables;//0=nameEndSlate/1=rndEndText
	public string[] endSlatePwnWords;
	public DynamicCenterZoomCam myDynCamZC;
	private bool alreadyWon = false;

	public void restartLevel ()
	{
		Application.LoadLevel(Application.loadedLevelName.ToString());
	}
	public void back2MainMenu ()
	{
		Application.LoadLevel("MainMenu");
	}
	// Use this for initialization
	void Start () {
		alreadyWon = false;
		enables[0].SetActive(false);
		int i = 1;
		respawnPoints.AddRange (GameObject.FindGameObjectsWithTag ("Respawn"));
		//while (i<MainMenu.playerCount) {
		while (i<5) {
			GameObject tmpPlayer;
			tmpPlayer = Instantiate(playerPrefabs[i-1], respawnPoints[i-1].transform.position, respawnPoints[i-1].transform.rotation) as GameObject;
			tmpPlayer.name = i.ToString();
			PlayerMovement tmpPlayerMovement = tmpPlayer.GetComponent<PlayerMovement> ();
			tmpPlayerMovement.horizontalName = "Horizontal" + tmpPlayer.name;
			tmpPlayerMovement.fireName = "Fire" + tmpPlayer.name;
			tmpPlayerMovement.jumpName = "Jump" + tmpPlayer.name;
			Debug.Log("spawned Player " + tmpPlayer.name);
			i++;
		}
	}
	
	// Update is called once per frame
	void Update () {
	if (myDynCamZC.objects2Track.Count < 2 && !alreadyWon) {
			alreadyWon = true;
			enables[0].SetActive(true);
			textables[0].text = ("Player " + myDynCamZC.objects2Track[myDynCamZC.objects2Track.Count - 1].gameObject.name.ToString());
			textables[1].text = endSlatePwnWords[Random.Range(0,endSlatePwnWords.Length)];
		}
	}
}
