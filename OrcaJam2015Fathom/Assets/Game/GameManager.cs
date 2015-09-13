using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

// ballbusters: legend of the silver surfer
public class GameManager : MonoBehaviour
{
    public GameObject GridPrefab;
 	public GameObject PlayerPrefab;

    public string FilePath;

	// Use this for initialization
	void Start()
	{
        var gridObject = (GameObject)Instantiate(GridPrefab, transform.position, transform.rotation);
        var grid = gridObject.GetComponent<Grid>();
        grid.InitializeGridFromFile(FilePath);
        
        List<Direction> startingDirections = new List<Direction>() { Direction.West, Direction.East, Direction.North, Direction.South };
        IEnumerable<Track> spawnTracks = grid.GetSpawnPositions();
        for (int i = 0; i < GameSettings.NumPlayers; i++) // todo: Later always make 4 players, but 4-NumPlayers are AI
        {
         	Track t = spawnTracks.ElementAt(i);
        	GameObject p = (GameObject)Instantiate(PlayerPrefab, t.gameObject.transform.position, Quaternion.identity);
        	PlayerController pc = p.GetComponent<PlayerController>();
			pc.InitStart(t, startingDirections[i]);
        }
	}
	
	// Update is called once per frame
	void Update()
	{
	
	}
}
