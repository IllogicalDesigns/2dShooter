using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class LevelSelect : MonoBehaviour
{
	public Text nameTxt;
	public Image lvlImage;
	public Text desText;
	public string[] lvlNames;
	public Sprite[] lvlSprites;
	public string[] lvlDescipts;
	private int lvl2Display;

	public void loadSelectedlvl ()
	{
		Application.LoadLevel (lvl2Display.ToString ());
	}

	public void LvlUp ()
	{
		lvl2Display ++;
		lvl2Display = Mathf.Clamp (lvl2Display, 0, lvlNames.Length - 1);
		nameTxt.text = lvlNames [lvl2Display];
		lvlImage.sprite = lvlSprites [lvl2Display];
		desText.text = lvlDescipts [lvl2Display];
	}

	public void LvlDown ()
	{
		lvl2Display --;
		lvl2Display = Mathf.Clamp (lvl2Display, 0, lvlNames.Length - 1);
		nameTxt.text = lvlNames [lvl2Display];
		lvlImage.sprite = lvlSprites [lvl2Display];
		desText.text = lvlDescipts [lvl2Display];
	}

	// Use this for initialization
	void Start ()
	{
		lvl2Display = Random.Range (0, lvlNames.Length);
		nameTxt.text = lvlNames [lvl2Display];
		lvlImage.sprite = lvlSprites [lvl2Display];
		desText.text = lvlDescipts [lvl2Display];

	
	}
	
	// Update is called once per frame
	void Update ()
	{
	
	}
}
