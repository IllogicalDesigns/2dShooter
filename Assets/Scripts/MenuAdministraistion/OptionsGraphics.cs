using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class OptionsGraphics : MonoBehaviour
{
	string upArrow = "▲";
	string dwnArrow = "▼";
	public GameObject[] dropMenus;//0=screen//1=AA
	public Text[] dropButtonText;//0=screen//1=AA
	//Screen Stuff
	Resolution[] myResez;						//rezez of current screen
	int currentRezInt;							//our index of current rez
	public Text screenResolutionText;			//what rez our we at
	public static bool fullScreenBool = true;	//our we fullscreen

	public GameObject buttonParents;			//The Parents for our baby buttons
	bool raiseMenu = false;
	public GameObject RezButton;				//ourPrefab
	int currentAAInt;
	public Text AAText;							//what AA Do we want

	public GameObject AAButtonParents;			//The Parents for our baby buttons
	bool raiseMenuAA = false;
	public string[] AAAmount;


	// Use this for initialization
	void Start ()
	{
		ScreenStart ();
		AAStuff ();
	}

	//Screen Rez
	void ScreenStart ()
	{
		//screen Stuff
		currentRezInt = PlayerPrefs.GetInt ("screenRez");
		myResez = Screen.resolutions;
		Debug.Log ("current Resolution is " + currentRezInt + " of " + myResez.Length);
		screenResolutionText.text = myResez [currentRezInt].width + " x " + myResez [currentRezInt].height;
		Screen.SetResolution (myResez [currentRezInt].width, myResez [currentRezInt].height, fullScreenBool);
		dropMenus [0].SetActive (false);
		dropButtonText [0].text = dwnArrow;
		raiseMenu = false;
		for (int i = 0; i < myResez.Length; i++) {
			GameObject tmpObject = Instantiate (RezButton, Vector3.zero, Quaternion.identity) as GameObject;
			tmpObject.transform.parent = buttonParents.transform;
			tmpObject.name = myResez [i].width.ToString () + " x " + myResez [i].height.ToString ();
			Button tmpButton = tmpObject.gameObject.GetComponent<Button> ();
			int index = i;
			tmpButton.onClick.AddListener (delegate {
				setResolution (index);
			});
			
		}
	}

	public void dropRezMenu ()
	{
		raiseMenu = !raiseMenu;
		raiseOrLowerRez ();
	}

	public void ChangeFullScreen ()
	{
		fullScreenBool = !fullScreenBool;
		Screen.SetResolution (myResez [currentRezInt].width, myResez [currentRezInt].height, fullScreenBool);
	}

	void setResolution (int index)
	{
		Screen.SetResolution (myResez [index].width, myResez [index].height, fullScreenBool);
		PlayerPrefs.SetInt ("screenRez", index);
		screenResolutionText.text = myResez [currentRezInt].width + " x " + myResez [currentRezInt].height;
	}

	void raiseOrLowerRez ()
	{
		for (int i = 0; i<dropMenus.Length; i++) {
			dropMenus [i].SetActive (false);
			dropButtonText [i].text = "▼";
		}
		if (raiseMenu) {
			dropMenus [0].SetActive (true);
			dropButtonText [0].text = upArrow;
		}
		if (!raiseMenu) {
			dropMenus [0].SetActive (false);
			dropButtonText [0].text = dwnArrow;
		}
	}
	//AA Stuff
	void setAA (int index)
	{
		int num = int.Parse (AAAmount [index]);
		Debug.Log (num);
		QualitySettings.antiAliasing = num;
		if (AAAmount[index] == "0") {
			AAText.name = "None";
		} else {
			AAText.name = AAAmount [index] + "X";
		}
	}

	void AAStuff ()
	{
		currentAAInt = PlayerPrefs.GetInt ("AAAmount");
		Debug.Log ("current AA is " + currentAAInt + " of " + AAAmount.Length);
		if (AAAmount[currentAAInt] == "0") {
			AAText.name = "None";
		} else {
			AAText.name = AAAmount [currentAAInt] + "X";
		}
		dropMenus [1].SetActive (false);
		dropButtonText [0].text = dwnArrow;
		raiseMenuAA = false;
		for (int i = 0; i < AAAmount.Length; i++) {
			GameObject tmpObject = Instantiate (RezButton, Vector3.zero, Quaternion.identity) as GameObject;
			tmpObject.transform.parent = AAButtonParents.transform;
			if (AAAmount[i] == "0") {
				tmpObject.name = "None";
			} else {
				tmpObject.name = AAAmount [i] + "X";
			}
			Button tmpButton = tmpObject.gameObject.GetComponent<Button> ();
			int index = i;
			tmpButton.onClick.AddListener (delegate {
				setAA (index);
			});
		}
	}

	public void dropAAMenu ()
	{
		raiseMenuAA = !raiseMenuAA;
		raiseOrLowerAA ();
	}

	void raiseOrLowerAA ()
	{
		for (int i = 0; i<dropMenus.Length; i++) {
			dropMenus [i].SetActive (false);
			dropButtonText [i].text = "▼";
		}
		if (raiseMenuAA) {
			dropMenus [1].SetActive (true);
			dropButtonText [1].text = upArrow;
		}
		if (!raiseMenuAA) {
			dropMenus [1].SetActive (false);
			dropButtonText [1].text = dwnArrow;
		}
	}
}
