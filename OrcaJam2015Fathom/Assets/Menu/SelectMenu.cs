using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.UI;

// ballbusters: legend of the silver surfer
public class SelectMenu : MonoBehaviour
{
	public GameObject StartTimerText;

	public GameObject[] PlayerPanels;
	
	float timer = 3;
	
	bool gameStarted = false;
	
	// Use this for initialization
	void Start()
	{
	
	}
	
	// Update is called once per frame
	void Update()
	{
		if (CheckPlayersReady())
		{
			StartTimerText.SetActive(true);
			timer -= Time.deltaTime;
			StartTimerText.GetComponent<Text>().text = "Start In " + Mathf.CeilToInt(timer).ToString();
			if (timer <= 0)
			{
				if (!gameStarted) StartGame();
			}
		} else {
			timer = 3;
			//StartTimerText.GetComponent<Text>().text = "Start In " + Mathf.CeilToInt(timer).ToString();
			StartTimerText.SetActive(false);
		}
	}
	
	void StartGame()
	{
		gameStarted = true;
		
		var panels = PlayerPanels.Select(u => u.GetComponent<PlayerPanel>());
		GameSettings.NumPlayers = panels.Count(u => u.state == PlayerPanel.State.Ready);
		
		Application.LoadLevel("title_scene");
	}
	
	bool CheckPlayersReady()
	{
		var panels = PlayerPanels.Select(u => u.GetComponent<PlayerPanel>());
		return panels.All(u => u.state != PlayerPanel.State.Waiting) && panels.Any(u => u.state == PlayerPanel.State.Ready);
	}
}
