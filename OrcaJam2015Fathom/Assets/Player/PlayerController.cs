using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

	public GameObject BulletPrefab;
	public GameObject Special;
	
	public Transform FiringPoint;
	public GameObject SpriteObject;
	
	public float speed = 1;

	int playerId = 1;
	// Use this for initialization
	void Start () 
	{
	
	}
	
	// Update is called once per frame
	void Update () 
	{
		UpdateMovement(); // for testing!
		CheckFire();
		CheckSpecial();
	}
	
	void UpdateMovement()
	{
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
		}
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
		if (Input.GetButtonDown ("A"+playerId))
		{
			GameObject bullet = (GameObject)Instantiate(BulletPrefab, FiringPoint.position, SpriteObject.transform.rotation);
		}
	}
	
	void CheckSpecial()
	{
		if (Input.GetButtonDown ("Y"+playerId))
		{
		
		}
	}
	
	public int GetDir()
	{
		float horzThumb = Input.GetAxis ("Horizontal"+playerId);
		float vertThumb = Input.GetAxis ("Vertical"+playerId);
		
		float horzPad = Input.GetAxis ("DPadH"+playerId);
		float vertPad = Input.GetAxis ("DPadV"+playerId);
		
		float horz = horzPad;
		float vert = vertPad;
		if (Mathf.Abs (horzThumb) > Mathf.Abs(horzPad))
		{
			horz = horzThumb;
		}
		if (Mathf.Abs (vertThumb) > Mathf.Abs(vertPad))
		{
			vert = vertThumb;
		}
		
		if (Mathf.Abs(horz) > Mathf.Abs(vert))
		{
			if (horz > 0.1) return 0; // right
			else if (horz < -0.1) return 1; // left
			else return -1;
		} else {
			if (vert > 0.1) return 2; // up
			else if (vert < -0.1) return 3; // down
			else return -1;
		}
	}
	
	public bool CheckPickup()
	{
		return Input.GetButtonDown ("X1");
	}
	
	void OnTriggerEnter2D(Collider2D other) 
	{
		if (other.tag == "Bullet") Destroy(gameObject);
	}
}
