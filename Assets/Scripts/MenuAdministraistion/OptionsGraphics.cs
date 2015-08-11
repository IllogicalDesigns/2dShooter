using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class OptionsGraphics : MonoBehaviour {
	string upArrow = "▲";
	string dwnArrow = "▼";
	//Screen Stuff
	Resolution[] myResez;						//rezez of current screen
	int currentRezInt;							//our index of current rez
	public Text screenResolutionText;			//what rez our we at
	public static bool fullScreenBool = true;	//our we fullscreen
	public GameObject rezDropPanel;				//Drop-Down Menu'
	public GameObject buttonParents;			//The Parents for our baby buttons
	public Text rezButText;						//Our Button Text
	bool raiseMenu = false;
	public GameObject RezButton;				//ourPrefab
	//AA Stuff
	public Text AAText;							//what AA Do we want
	public GameObject AADropPanel;				//Drop-Down Menu'
	public GameObject AAButtonParents;			//The Parents for our baby buttons
	public Text AAButText;						//Our Button Text
	bool raiseMenuAA = false;
	public string[] AAAmount;


	// Use this for initialization
	void Start () {
		ScreenStart ();
		AADropPanel.SetActive (false);
	}
	void ScreenStart () {
		//screen Stuff
		currentRezInt = PlayerPrefs.GetInt ("screenRez");
		myResez = Screen.resolutions;
		Debug.Log ("current Resolution is " + currentRezInt + " of " + myResez.Length);
		screenResolutionText.text = myResez [currentRezInt].width + " x " + myResez [currentRezInt].height;
		Screen.SetResolution (myResez [currentRezInt].width, myResez [currentRezInt].height, fullScreenBool);
		rezDropPanel.SetActive (false);
		rezButText.text = dwnArrow;
		raiseMenu = false;
		for(int i = 0; i < myResez.Length; i++){
			GameObject tmpObject = Instantiate(RezButton, Vector3.zero, Quaternion.identity) as GameObject;
			tmpObject.transform.parent = buttonParents.transform;
			tmpObject.name = myResez[i].width.ToString() + " x " + myResez[i].height.ToString();
			Button tmpButton = tmpObject.gameObject.GetComponent<Button>();
			int index = i;
			tmpButton.onClick.AddListener(delegate{setResolution(index);});

		}
	}
	void setResolution (int index){
		Screen.SetResolution (myResez [index].width, myResez [index].height, fullScreenBool);
		PlayerPrefs.SetInt ("screenRez", index);
	}
	void raiseOrLowerRez () {
		if (raiseMenu) {
			rezDropPanel.SetActive (true);
			rezButText.text = upArrow;
		}
		if (!raiseMenu) {
			rezDropPanel.SetActive (false);
			rezButText.text = dwnArrow;
		}
	}
	public void dropAAMenu () {
		raiseMenuAA = !raiseMenuAA;
		raiseOrLowerAA ();
	}
	void raiseOrLowerAA () {
		if (raiseMenuAA) {
			AADropPanel.SetActive (true);
			AAButText.text = upArrow;
		}
		if (!raiseMenuAA) {
			AADropPanel.SetActive (false);
			AAButText.text = dwnArrow;
		}
	}
	public void dropRezMenu () {
		raiseMenu = !raiseMenu;
		raiseOrLowerRez ();
	}
	public void ChangeFullScreen (){
		fullScreenBool = !fullScreenBool;
		Screen.SetResolution (myResez [currentRezInt].width, myResez [currentRezInt].height, fullScreenBool);
	}
	public void ChangeResolution (bool increasing)
	{
		if (increasing && (currentRezInt + 1) != (myResez.Length)) {
			currentRezInt ++;
			screenResolutionText.text = myResez [currentRezInt].width + " x " + myResez [currentRezInt].height;
			Screen.SetResolution (myResez [currentRezInt].width, myResez [currentRezInt].height, fullScreenBool);
			Debug.Log ("increase Resolution");
		} else if (!increasing && (currentRezInt - 1) != - 1) {
			currentRezInt --;
			screenResolutionText.text = myResez [currentRezInt].width + " x " + myResez [currentRezInt].height;
			Screen.SetResolution (myResez [currentRezInt].width, myResez [currentRezInt].height, fullScreenBool);
			Debug.Log ("decrease Resolution");
		}
	}
}
