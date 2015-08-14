using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using System.Collections;
using System.Collections.Generic;

public class GMNet : NetworkBehaviour {
	public List<GameObject> respawnPoints = new List<GameObject> ();
	public GameObject[] playerPrefabs;
	public GameObject[] enables;//0=endSlate
	public Text[] textables;//0=nameEndSlate/1=rndEndText
	public string[] endSlatePwnWords;
	public DynamicCenterZoomCam myDynCamZC;
	private bool alreadyWon = false;
	public List<GameObject> players = new List<GameObject> ();
	
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
		Time.timeScale = 1;
		alreadyWon = false;
		enables[0].SetActive(false);
		int i = 1;
		respawnPoints.AddRange (GameObject.FindGameObjectsWithTag ("Respawn"));
		players.AddRange (GameObject.FindGameObjectsWithTag ("Client"));
		while (i<players.Count+1) {
			GameObject tmpPlayer = (GameObject)Instantiate(playerPrefabs[i-1], respawnPoints[i-1].transform.position, Quaternion.identity);
			tmpPlayer.name = i.ToString();
			Debug.Log("spawned Player " + tmpPlayer.name);
			NetworkServer.Spawn(tmpPlayer);
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
