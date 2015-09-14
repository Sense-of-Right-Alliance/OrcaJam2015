using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class Bullet : MonoBehaviour
{
    public int PlayerID;
	public float speed = 10;

    public Sprite Player1Sprite;
    public Sprite Player2Sprite;
    public Sprite Player3Sprite;
    public Sprite Player4Sprite;

    // Use this for initialization
    void Start() {
        //Debug.Log ("bullet dir = " + transform.up.ToString());
    }
    
    public void InitSprite(int playerId)
    {
    	PlayerID = playerId;
    	
		var spriteRenderer = GetComponent<SpriteRenderer>();
		switch (PlayerID)
		{
		case 1:
			spriteRenderer.sprite = Player1Sprite;
			break;
		case 2:
			spriteRenderer.sprite = Player2Sprite;
			break;
		case 3:
			spriteRenderer.sprite = Player3Sprite;
			break;
		case 4:
			spriteRenderer.sprite = Player4Sprite;
			break;
		}
    }
	
	// Update is called once per frame
	void Update() {
		Vector3 p = transform.position;
		transform.position = p + transform.up * speed * Time.deltaTime;
	}
	
	void OnTriggerEnter2D(Collider2D other) 
	{
		Destroy(gameObject);
	}
}
