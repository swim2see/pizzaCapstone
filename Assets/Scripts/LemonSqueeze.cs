using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LemonSqueeze : MonoBehaviour
{

	public int squeezeCount;

	public int juicedLemons;

	private float timer;

	public int gameState;

	public Text gameText;
	public Text lemonPower;
	public Text theTime;

	private bool R1P;
	private bool R2P;
	private bool L1P;
	private bool L2P;
	
	
	// Use this for initialization
	void Start ()
	{
		timer = 3;
		gameState = 1;
	}
	
	// Update is called once per frame
	void Update ()
	{
		lemonPower.text = squeezeCount.ToString();
		
		//gamestate 0 is UI navigation. Everything else is a microgame
		if (gameState != 0)
		{
			timer -= Time.deltaTime;
			
			if (timer <= 0)
			{
				gameState = 0;
				squeezeCount = 0;
			}

		} 
		else if (gameState == 0)
		{
			timer = 3;
			//
			if (Input.GetButtonDown("joystick button 9"))
			{
				squeezeCount = 0;
				gameState = 1;
			}
		}

		theTime.text = timer.ToString();
		gameText.text = (gameState.ToString()); 
		
		lemonSqueezing();
	}

	//lemon squeezing minigame. 
	void lemonSqueezing()
	{
		//ensure code is only active when the lemon squeeze attack is selected.
		if (gameState == 1)
		{
			//checks to see which of the shoulder buttons have been pressed.
			if (Input.GetButtonDown("joystick button 4"))
			{
				L1P = true;
			}
			if (Input.GetButtonDown("joystick button 5"))
			{
				R1P = true;
			}
			if (Input.GetButtonDown("joystick button 6"))
			{
				L2P = true;
			}
			if (Input.GetButtonDown("joystick button 7"))
			{
				R2P = true;
			}
			
			if (L1P == true && R1P == true && L2P == true && R2P == true)
			{
				squeezeCount += 1;
				L1P = false;
				R1P = false;
				L2P = false;
				R2P = false;
			}
			
		}
			
	}
}
