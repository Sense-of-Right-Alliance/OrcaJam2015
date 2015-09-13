using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

// ballbusters: legend of the silver surfer
public class PlayerSprites : MonoBehaviour
{
    public int PlayerID;
    public Sprite Player1Sprite;
    public Sprite Player2Sprite;
    public Sprite Player3Sprite;
    public Sprite Player4Sprite;

    public SpriteRenderer spriteRenderer;

    // Use this for initialization
    void Start()
    {
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
    void Update()
    {
        
    }
}
