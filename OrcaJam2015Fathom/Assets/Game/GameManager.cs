using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.UI;

// ballbusters: legend of the silver surfer
public class GameManager : MonoBehaviour
{
    public GameObject GridPrefab;
 	public GameObject PlayerPrefab;

    public string FilePath;
    
    public List<GameObject> Players = new List<GameObject>();
    
    public Text GameTimerText;
    public float gameTimer = 1;
    
	bool gameOver = false;

	// Use this for initialization
	void Start()
	{
        var gridObject = (GameObject)Instantiate(GridPrefab, transform.position, transform.rotation);
        var grid = gridObject.GetComponent<Grid>();
        grid.InitializeGridFromFile(FilePath);
        
        List<Direction> startingDirections = new List<Direction>() { Direction.West, Direction.East, Direction.North, Direction.South };
        IEnumerable<Track> spawnTracks = grid.GetSpawnPositions();
        for (int i = 0; i < 4; i++) // todo: Later always make 4 players, but 4-NumPlayers are AI
        {
         	Track t = spawnTracks.ElementAt(i);
        	GameObject p = (GameObject)Instantiate(PlayerPrefab, t.gameObject.transform.position, Quaternion.identity);
        	PlayerController pc = p.GetComponent<PlayerController>();
        	pc.playerId = i+1;
			pc.InitStart(t, startingDirections[i]);
			
			Players.Add(p);
        }
        
        gameTimer = GameSettings.GameTime;
		GameTimerText.text = "Time Remaining: " + Mathf.CeilToInt(gameTimer).ToString ();
		
		gameOver = false;
	}
	
	// Update is called once per frame
	void Update()
	{
		CheckForWinner();
		if (!gameOver) UpdateTimer();
	}
	
	void UpdateTimer()
	{
		gameTimer -= Time.deltaTime;
		GameTimerText.text = "Time Remaining: " + Mathf.CeilToInt(gameTimer).ToString ();
		if (gameTimer <= 0)
		{
			gameOver = true;
			GameSettings.WinningPlayer = -1;
			Application.LoadLevel("end_scene");
		}
	}
	
	void CheckForWinner()
	{
		int aliveCount = 0;
		int aliveId = -1;
		for (int i = 0; i < Players.Count; i++)
		{
			PlayerController pc = Players[i].GetComponent<PlayerController>();
			if (pc.IsAlive) 
			{
				aliveId = pc.playerId;
				aliveCount++;
			}
		}
		
		if (Players.Count > 1 && aliveCount == 1)
		{
			gameOver = true;
			GameSettings.WinningPlayer = aliveId;
			Application.LoadLevel ("end_scene");
		}
		
		if (Players.Count > 1 && aliveCount <= 0)
		{
			gameOver = true;
			GameSettings.WinningPlayer = -1;
			Application.LoadLevel("end_scene");
		}
	}
}
