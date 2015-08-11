using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MainMenu : MonoBehaviour
{
	public GameObject lvlSelect;
	public GameObject charSelect;
	public GameObject main;
	public GameObject options;
	public static int playerCount = 1;

	// Use this for initialization
	void Start ()
	{
		Time.timeScale = 1;
		main.SetActive (true);
		charSelect.SetActive (false);
		lvlSelect.SetActive (false);
		options.SetActive (false);
	}

	public void Main2CharSelect ()
	{
		main.SetActive (false);
		charSelect.SetActive (true);
		lvlSelect.SetActive (true);
		options.SetActive (false);
	}

	public void Back2Main ()
	{
		main.SetActive (true);
		charSelect.SetActive (false);
		lvlSelect.SetActive (false);
		options.SetActive (false);
		PlayerPrefs.Save ();
	}

	public void Main2Options ()
	{
		main.SetActive (false);
		charSelect.SetActive (false);
		lvlSelect.SetActive (false);
		options.SetActive (true);
	}

	public void ExitGame ()
	{
		Application.Quit ();
	}
}
