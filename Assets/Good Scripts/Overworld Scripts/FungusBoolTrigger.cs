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

	public bool distanceTrigger;
	// Use this for initialization
	void Awake () {
		flowchart.SetBooleanVariable("NPCSpeaker", false);
		flowchart = FindObjectOfType<Flowchart>();
		pHere = false;
		distanceTrigger = true;
	}
	
	// Update is called once per frame
	void Update () {

        if (Input.GetKeyDown(KeyCode.E))
        {
            print("pressed");
        }

		//talking/interacting with world objects 
		if (pHere == true && Input.GetKeyDown(KeyCode.E))
		{
			flowchart.ExecuteBlock("TalkBlock");
            buttonPressed = true;
			inMenu = true;
		}

        if (Input.GetKeyDown(KeyCode.E))
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

			if (distanceTrigger == true)
			{
				inMenu = true;
				flowchart.ExecuteBlock("GetOverHere");
			}
		}
	}
	
	private void OnTriggerExit2D(Collider2D other)
	{
		if (other.tag == "Player")
		{
			flowchart.SetBooleanVariable("NPCSpeaker", false);
			Debug.Log("flowchart");
			pHere = false; 
		}
	}

	//This is called in the fungus flowchart to restore movement to the player
	public void RegainControl()
	{
		inMenu = false;
	}

	//Sets an objects BoxCollider to take up one grid square of space
	public void ResetTriggerSize()
	{
		BoxCollider2D curBC = GetComponent<BoxCollider2D>();
		curBC.size = new Vector2(5,5);
		distanceTrigger = false;
		//pHere = false;
	}

	/*public void ChangeBool()
	{
		if (pHere)
		{
			flowchart.SetBooleanVariable("NPCSpeaker", true);
		}
	}*/
}
