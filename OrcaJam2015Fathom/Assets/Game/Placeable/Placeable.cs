using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

// ballbusters: legend of the silver surfer
public class Placeable : MonoBehaviour
{
    public int XPosition { get; set; }
    public int YPosition { get; set; }
    public Grid Grid { get; set; }

    public Placeable GetNeighbour(Direction direction)
    {
        int x = XPosition;
        int y = YPosition;
        switch (direction)
        {
            case Direction.North:
                y -= 1;
                break;
            case Direction.East:
                x += 1;
                break;
            case Direction.South:
                y += 1;
                break;
            case Direction.West:
                x -= 1;
                break;
        }
        return Grid.GetAtPosition(x, y);
    }

    public virtual bool IsTrack() { return false; }

    // Use this for initialization
    void Start()
	{
	    
	}
	
	// Update is called once per frame
	void Update()
	{
	    
	}
}
