using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

// ballbusters: legend of the silver surfer
public class Expires : MonoBehaviour
{

	public float time = 1.0f;
	public bool startOnInit = true;
	
	bool active = false;

	// Use this for initialization
	void Start()
	{
		if (startOnInit) StartTimer ();
	}
	
	public void StartTimer()
	{
		active = true;
	}
	
	// Update is called once per frame
	void Update()
	{
		if (active)
		{
			time -= Time.deltaTime;
			if (time <= 0) Destroy (gameObject);
		}
	}
}
