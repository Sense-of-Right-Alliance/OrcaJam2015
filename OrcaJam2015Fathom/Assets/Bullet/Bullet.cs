using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour {

	public float speed = 10;

	// Use this for initialization
	void Start () {
		Debug.Log ("bullet dir = " + transform.up.ToString());
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 p = transform.position;
		transform.position = p + transform.up * speed * Time.deltaTime;
	}
	
	void OnTriggerEnter2D(Collider2D other) 
	{
		Destroy(gameObject);
	}
}
