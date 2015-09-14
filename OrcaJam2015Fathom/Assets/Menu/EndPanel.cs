using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.UI;

// ballbusters: legend of the silver surfer
public class EndPanel : MonoBehaviour
{
	public GameObject EndText;
	// Use this for initialization
	void Start()
	{
		EndText.GetComponent<Text>().text = GameSettings.WinningPlayer == -1 ? "Nobody Wins!" : "Player " + GameSettings.WinningPlayer + " Wins!";
	}
	
	// Update is called once per frame
	void Update()
	{
		if (Input.GetKeyDown(KeyCode.Space)
		    || (Input.GetButtonDown ("B1")) || (Input.GetButtonDown ("B2"))
		    || (Input.GetButtonDown ("B3")) || (Input.GetButtonDown ("B4")))
		{
			GameSettings.WinningPlayer = -1;
			Application.LoadLevel("menu_scene");
		}
	}
}
