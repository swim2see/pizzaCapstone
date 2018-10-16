using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LemonSqueezeMinigame : MonoBehaviour
{

	public int squeezeCount;

	public int juicedLemons;

	public float timer;

	public int gameState;

	public Text gameText;

	

    private bool R1P;
    private bool R2P;
    private bool L1P;
    private bool L2P;

    public Text squeezedText;

   
    // Use this for initialization
    void Start ()
	{
		timer = 3;
		gameState = 0;
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (gameState == 1)
		{
			timer -= Time.deltaTime;
		} 
		else if (gameState == 0)
		{
			timer = 3f;
            //squeezeCount = 0;
           
		}

		
        if (timer <= 0f)
        {
            timer = 0f;
            gameState = 0;
                  }
        //gameText.text = (gameState.ToString()); 




		buttonInputs();
	}

	//registers all of the button inputs
	void buttonInputs()
    {
        //ensure code is only active when the lemon squeeze attack is selected.
        if (gameState == 1)
        {
            //checks to see which of the shoulder buttons have been pressed.
            if (Input.GetButtonDown("joystick button 4"))
            {
                squeezeCount += 1;
            }
            if (Input.GetButtonDown("joystick button 5"))
            {
                squeezeCount += 1;
            }
            //if (Input.GetButtonDown("joystick button 6"))
            //{
            //    squeezeCount += 1;
            //}
            //if (Input.GetButtonDown("joystick button 7"))
            //{
            //    squeezeCount += 1;
            //}

           

        }

    }
}
