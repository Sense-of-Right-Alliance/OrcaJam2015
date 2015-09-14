using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class PlayerController : MonoBehaviour { 
    static List<Direction> directionPriorities = new List<Direction>()
    {
        Direction.North,
        Direction.East,
        Direction.South,
        Direction.West,
    };

    public GameObject BulletPrefab;
    public GameObject Special;
    public GameObject Explosion;
    
    public Transform FiringPoint;
    public GameObject SpriteObject;
    
    public Track currentTrack;
    public Track nextTrack;
    public Direction currentDirection;
    
    public float speed = 0.75f;

    public bool IsAlive = true;

    public int playerId = 1;
    // Use this for initialization
    void Start () 
    {
    
    }
    
    public void InitStart(Track startTrack, Direction startDir)
    {
        nextTrack = startTrack;
        currentDirection = startDir;
        HandleArriveAtNextTrack();
    }
    
    // Update is called once per frame
    void Update () 
    {
        if (IsAlive)
        {
            UpdateMovement(); // for testing!
            CheckFire();
            CheckSpecial();
        }
    }
        
    void UpdateMovement()
    {
        transform.Translate((nextTrack.transform.position - transform.position).normalized * speed * Time.deltaTime);
        
        float distance = (nextTrack.transform.position - transform.position).magnitude;
        if (distance < 0.1f) HandleArriveAtNextTrack();
            
        /*
        int inputDir = GetDir ();
        
        if (inputDir >= 0 && inputDir < 2)
        {
            Vector3 dir = new Vector3(inputDir == 0 ? 1 : -1, 0, 0);
            SetRotation (dir);
            Debug.Log ("Direction: " + dir.ToString());
            transform.Translate(dir * speed * Time.deltaTime);
        }
        else if (inputDir >= 2 && inputDir < 4)
        {
            Vector3 dir = new Vector3(0, inputDir == 2 ? -1 : 1, 0);
            SetRotation (dir);
            Debug.Log ("Direction: " + dir.ToString());
            transform.Translate(dir * speed * Time.deltaTime);
        }*/
    }
    
    public void HandleArriveAtNextTrack()
    {
        Direction? inputDir = GetDir();
        
        if (playerId == 2) Debug.Log ("P2 input dir = " + inputDir.ToString());
        
        Direction desiredDirection = inputDir ?? currentDirection;
        
        if (inputDir == currentDirection.Reverse()) desiredDirection = currentDirection;
        
        currentTrack = nextTrack;
        
        var directionForwards = new List<Direction>() { desiredDirection };
        var directionBackwards = new List<Direction>() { currentDirection.Reverse() };
        
        var directionPriority = directionForwards
            .Concat(directionPriorities.Except(directionForwards).Except(directionBackwards))
            .Concat(directionBackwards);

        var test = directionPriority.ToList();

        currentDirection = directionPriority
            .Where(u => currentTrack.GetNeighbour(u) == null ? false : currentTrack.GetNeighbour(u).IsTrack())
            .First();

        nextTrack = currentTrack.GetNeighbour(currentDirection) as Track;
        
        var blah = string.Join(",",test.Select (u => u.ToString()).ToArray());
        //Debug.Log ("Input Dir = " + inputDir.ToString() + " Chosen Dir = " + currentDirection.ToString() + " Priorities = " + blah);
        
		SetRotation (GetVectorFromDirection(currentDirection));
    }
    
	Vector3 GetVectorFromDirection(Direction d)
	{
		Vector3 dir = new Vector3(0,0,0);
		
		if (d == Direction.East) dir.x = 1;
		if (d == Direction.West) dir.x = -1;
		if (d == Direction.North) dir.y = 1;
		if (d == Direction.South) dir.y = -1;
		
		return dir;
	}
	
	void SetRotation(Vector3 target)
    {
        Vector3 vectorToTarget = target;// - SpriteObject.transform.position;
        float angle = Mathf.Atan2(vectorToTarget.y, vectorToTarget.x) * Mathf.Rad2Deg;
        Quaternion q = Quaternion.AngleAxis(angle - 90, Vector3.forward);
        SpriteObject.transform.rotation = q;//Quaternion.Slerp(transform.rotation, q, Time.deltaTime * speed);
    }
    
    
    
    void CheckFire()
    {
        if (Input.GetButtonDown ("A"+playerId) || (playerId == 1 && Input.GetKeyDown(KeyCode.Space)))
        {	
			GameObject bullet = (GameObject)Instantiate(BulletPrefab, FiringPoint.position, Quaternion.identity);
			bullet.transform.up = GetVectorFromDirection(currentDirection);
			bullet.GetComponent<Bullet>().InitSprite(playerId);
        }
    }
    
    void CheckSpecial()
    {
        if (Input.GetButtonDown ("Y"+playerId))
        {
        
        }
    }
    
    public Direction? GetDir()
    {
        float horzThumb = Input.GetAxis ("Horizontal"+playerId);
        float vertThumb = Input.GetAxis ("Vertical"+playerId);
        
        float horzPad = Input.GetAxis ("DPadH"+playerId);
        float vertPad = Input.GetAxis ("DPadV"+playerId);
        
        float horz = horzPad;
        float vert = vertPad*-1;
        if (Mathf.Abs (horzThumb) > Mathf.Abs(horzPad))
        {
            horz = horzThumb;
        }
        if (Mathf.Abs (vertThumb) > Mathf.Abs(vertPad))
        {
            vert = vertThumb;
        }
        
        // USE KEYBOARD IF KEYBOARD
        Direction? keyboard = CheckMovementKey ();
         if (keyboard != null) return keyboard;
        
        // HANDLE CONTROLLER WIZARDRY
        if (Mathf.Abs(horz) > Mathf.Abs(vert))
        {
            if (horz > 0.05) return Direction.East; // right
            else if (horz < -0.05) return Direction.West; // left
            else return null;
        } else {
            if (vert > 0.05) return Direction.South; // up
            else if (vert < -0.05) return Direction.North; // down
            else return null;
        }
    }
    
    public Direction? CheckMovementKey()
    {
        if (Input.GetKey (KeyCode.D)) return Direction.East;
        else if (Input.GetKey (KeyCode.A)) return Direction.West;
        else if (Input.GetKey (KeyCode.S)) return Direction.South;
        else if (Input.GetKey (KeyCode.W)) return Direction.North;
        
        return null;
    }
    
    public bool CheckPickup()
    {
        return Input.GetButtonDown ("X1") || Input.GetKey(KeyCode.F);
    }
    
    void HandleCollision()
    {
        Instantiate(Explosion, new Vector3(transform.position.x, transform.position.y, -5), transform.rotation);
        KillPlayer();
    }

    void KillPlayer()
    {
        gameObject.SetActive(false);
        IsAlive = false;
    }
    
    void OnTriggerEnter2D(Collider2D other) 
    {
        if (other.tag == "Bullet") HandleCollision();
    }
}
