using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.UI;

// ballbusters: legend of the silver surfer
public class TitleScreen : MonoBehaviour
{
	static float frameTime = 0.5f;
	public Sprite[] titleImages;
	public Image image;
	
	int titleIndex = 0;
	
	float timer = 0.5f;
	
	// Use this for initialization
	void Start()
	{
		timer = frameTime;
	}
	
	// Update is called once per frame
	void Update()
	{
		timer -= Time.deltaTime;
		
		if (timer <= 0) 
		{
			timer = frameTime;
			
			titleIndex++;
			
			if (titleIndex > 2) Application.LoadLevel("game_scene");
			else {
				image.sprite = titleImages[titleIndex];
			}
		}
	}
}
