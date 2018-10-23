using System.Collections;
using System.Collections.Generic;
using Fungus;
using UnityEngine;

public class FungusBoolTrigger : MonoBehaviour
{

	public Flowchart flowchart;
	public bool pHere;
	public bool inMenu;
    public bool buttonPressed;
	// Use this for initialization
	void Awake () {
		flowchart.SetBooleanVariable("NPCSpeaker", false);
		flowchart = FindObjectOfType<Flowchart>();
		pHere = false;
	}
	
	// Update is called once per frame
	void Update () {

        if (Input.GetButtonDown("Submit"))
        {
            print("pressed");
        }

		if (pHere == true && Input.GetButtonDown("Submit") && !buttonPressed)
		{
			flowchart.ExecuteBlock("TalkBlock");
            buttonPressed = true;
			inMenu = true;
		}

        if (Input.GetButton("joystick button 1"))
        {
            buttonPressed = true;
        }
        else
        {
            buttonPressed = false;
        }

        /*if (flowchart. == false)
		{
			inMenu = false;
		}*/
    }

	//Detects when you are standing next to an NPC
	private void OnTriggerEnter2D(Collider2D other)
	{
		if (other.tag == "Player")
		{
				flowchart.SetBooleanVariable("NPCSpeaker", true);
				//flowchart.ExecuteBlock("TalkBlock");
				Debug.Log("flowchart");
				pHere = true;
		}
	}
	
	private void OnTriggerExit2D(Collider2D other)
	{
		if (other.tag == "Player")
		{
			flowchart.SetBooleanVariable("NPCSpeaker", true);
			Debug.Log("flowchart");
			pHere = false; 
		}
	}

	/*public void ChangeBool()
	{
		if (pHere)
		{
			flowchart.SetBooleanVariable("NPCSpeaker", true);
		}
	}*/
}
