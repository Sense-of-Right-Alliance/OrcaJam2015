using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.UI;

// ballbusters: legend of the silver surfer
public class PlayerPanel : MonoBehaviour
{
	public enum State
	{
		Empty,
		Waiting,
		Ready
	}
	
	public GameObject Prompt;
	public GameObject Check;
	public GameObject Title;
	
	public int playerId = 1;
	
	public State state = State.Empty;
	
	// Use this for initialization
	void Start()
	{
		SetState (State.Empty);
	}
	
	void SetState(State newState)
	{
		state = newState;
		
		switch(state)
		{
			case (State.Empty):
				UpdatePrompt();
				HideCheck();
				break;
			case (State.Waiting):
				UpdatePrompt();
				HideCheck();
				break;
			case (State.Ready):
				UpdatePrompt();
				ShowCheck();
				break;
		}
	}
	
	void UpdatePrompt()
	{
		if (state == State.Ready) 
		{
			Prompt.SetActive(false);
		} else {
			Prompt.SetActive(true);
			Prompt.GetComponent<Text>().text = state == State.Empty ? "Press A to Join" : "Press A to Ready";
		}
	}
	
	void HideCheck()
	{
		Check.SetActive(false);
	}
	
	void ShowCheck()
	{
		Check.SetActive(true);
	}
	
	// Update is called once per frame
	void Update()
	{
		switch(state)
		{
			case (State.Empty):
				if (Input.GetButtonDown ("A"+playerId) || (playerId == 1 && Input.GetKeyDown(KeyCode.Space))) SetState(State.Waiting);
				break;
			case (State.Waiting):
				if (Input.GetButtonDown ("B"+playerId) || (playerId == 1 && Input.GetKeyDown(KeyCode.Escape))) SetState(State.Empty);
				if (Input.GetButtonDown ("A"+playerId) || (playerId == 1 && Input.GetKeyDown(KeyCode.Space))) SetState(State.Ready);
				break;
			case (State.Ready):
				if (Input.GetButtonDown ("B"+playerId) || (playerId == 1 && Input.GetKeyDown(KeyCode.Escape))) SetState(State.Waiting);
				break;
		}
	}
}
