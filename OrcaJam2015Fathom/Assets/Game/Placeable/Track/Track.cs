using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

// ballbusters: legend of the silver surfer
public class Track : Placeable
{
    public Sprite Cross;
    public Sprite End;
    public Sprite Right;
    public Sprite Tee;
    public Sprite Straight;

    protected SpriteRenderer spriteRenderer = null;
    public SpriteRenderer Renderer { get { if (spriteRenderer == null) { spriteRenderer = GetComponent<SpriteRenderer>(); } return spriteRenderer; } }

    protected TrackType trackType;
    public TrackType TrackType { get { return trackType; } set { trackType = value; UpdateSprite(); } }
    protected Direction facingDirection;
    public Direction FacingDirection { get { return facingDirection; } set { facingDirection = value; UpdateRotation(); } }

    public override bool IsTrack() { return true; }

    protected void UpdateSprite()
    {
        Sprite sprite;
        switch (TrackType)
        {
            case TrackType.FourWayIntersection:
                sprite = Cross;
                break;
            case TrackType.DeadEnd:
                sprite = End;
                break;
            case TrackType.Turn:
                sprite = Right;
                break;
            case TrackType.TeeIntersection:
                sprite = Tee;
                break;
            case TrackType.Straight:
            default:
                sprite = Straight;
                break;
        }

        Renderer.sprite = sprite;
    }

    protected void UpdateRotation()
    {
        if (FacingDirection != Direction.North)
        {
            transform.Rotate(Vector3.forward * -90);
            if (FacingDirection != Direction.East)
            {
                transform.Rotate(Vector3.forward * -90);
                if (FacingDirection != Direction.South)
                {
                    transform.Rotate(Vector3.forward * -90);
                }
            }
        }
    }

    // Use this for initialization
    void Start()
	{

    }
	
	// Update is called once per frame
	void Update()
	{
	    
	}
}
