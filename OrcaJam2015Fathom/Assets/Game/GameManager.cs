using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

// ballbusters: legend of the silver surfer
public class GameManager : MonoBehaviour
{
    public GameObject GridPrefab;
    public string FilePath;

	// Use this for initialization
	void Start()
	{
        var gridObject = (GameObject)Instantiate(GridPrefab, transform.position, transform.rotation);
        var grid = gridObject.GetComponent<Grid>();
        grid.InitializeGridFromFile(FilePath);
	}
	
	// Update is called once per frame
	void Update()
	{
	
	}
}
