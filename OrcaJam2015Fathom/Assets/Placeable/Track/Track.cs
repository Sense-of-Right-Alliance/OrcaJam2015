using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

// ballbusters: legend of the silver surfer
public class Track : Placeable
{
    public TrackType TrackType;
    public Direction FacingDirection;

    public override bool IsTrack() { return true; }

    // Use this for initialization
    void Start()
	{
	
	}
	
	// Update is called once per frame
	void Update()
	{
	
	}
}
