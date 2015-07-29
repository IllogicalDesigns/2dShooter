using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using TeamUtility.IO;

public class ContolOptions : MonoBehaviour {
	public Text[] controlTypeText;
	public GameObject[] player1Bindables;
	public GameObject[] player2Bindables;
	public GameObject[] player3Bindables;
	public GameObject[] player4Bindables;
	public enum controlType
	{
		Keyboard, Gamepad1, Gamepad2, Gamepad3, Gamepad4
	};
	public controlType p1Type;
	public controlType p2Type;
	public controlType p3Type;
	public controlType p4Type;
	private int p1I = 0;
	private int p2I = 0;
	private int p3I = 0;
	private int p4I = 0;
	int i = 0;
	//AxisConfiguration axisConfig = InputManager.GetAxisConfiguration("Jump");

	public void p1Iterate () {
		p1I ++;
		if (p1I >= 5)
			p1I = 0;
		Debug.Log (p1I);
		UpdateP1ControlType ();
	}
	public void p2Iterate () {
		p2I ++;
		if (p2I >= 5)
			p2I = 0;
		UpdateP2ControlType ();
	}
	public void p3Iterate () {
		p3I ++;
		if (p3I >= 5)
			p3I = 0;
		UpdateP3ControlType ();
	}
	public void p4Iterate () {
		p4I ++;
		if (p4I >= 5)
			p4I = 0;
		UpdateP4ControlType ();
	}

	// Use this for initialization
	void Start () {
		for (int i = 0; i < controlTypeText.Length; i++) {
			if(i == 0)
				controlTypeText[0].text = p1Type.ToString();
			if(i == 1)
				controlTypeText[1].text = p2Type.ToString();
			if(i == 2)
				controlTypeText[2].text = p3Type.ToString();
			if(i == 3)
				controlTypeText[3].text = p4Type.ToString();
		}
	}

	void UpdateP1ControlType () {
		if (p1I == 0)
			p1Type = controlType.Keyboard;
		if (p1I == 1)
			p1Type = controlType.Gamepad1;
		if (p1I == 2)
			p1Type = controlType.Gamepad2;
		if (p1I == 3)
			p1Type = controlType.Gamepad3;
		if (p1I == 4)
			p1Type = controlType.Gamepad4;
		updateNames ();
	}

	void UpdateP2ControlType () {
		if (p2I == 0)
			p2Type = controlType.Keyboard;
		if (p2I == 1)
			p2Type = controlType.Gamepad1;
		if (p2I == 2)
			p2Type = controlType.Gamepad2;
		if (p2I == 3)
			p2Type = controlType.Gamepad3;
		if (p2I == 4)
			p2Type = controlType.Gamepad4;
		updateNames ();
	}

	void UpdateP3ControlType () {
		if (p3I == 0)
			p3Type = controlType.Keyboard;
		if (p3I == 1)
			p3Type = controlType.Gamepad1;
		if (p3I == 2)
			p3Type = controlType.Gamepad2;
		if (p3I == 3)
			p3Type = controlType.Gamepad3;
		if (p3I == 4)
			p3Type = controlType.Gamepad4;
		updateNames ();
	}

	void UpdateP4ControlType () {
		if (p4I == 0)
			p4Type = controlType.Keyboard;
		if (p4I == 2)
			p4Type = controlType.Gamepad1;
		if (p4I == 3)
			p4Type = controlType.Gamepad2;
		if (p4I == 4)
			p4Type = controlType.Gamepad3;
		if (p4I == 5)
			p4Type = controlType.Gamepad4;
		updateNames ();
	}

	void updateTypeAndSticks () {

	}
	void updateNames () {
		for (int i = 0; i < controlTypeText.Length; i++) {
			if(i == 0)
				controlTypeText[0].text = p1Type.ToString();
			if(i == 1)
				controlTypeText[1].text = p2Type.ToString();
			if(i == 2)
				controlTypeText[2].text = p3Type.ToString();
			if(i == 3)
				controlTypeText[3].text = p4Type.ToString();
		}
	}
	
	// Update is called once per frame
	void Update () {

	
	}
}
