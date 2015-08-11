using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class buttonNamer : MonoBehaviour {
	public string staticName = "staticName";
	public Text myText;
	bool alreadyDone = false;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (!alreadyDone && gameObject.name != staticName) {
			myText.text = gameObject.name;
			alreadyDone = true;
		}
	}
}
