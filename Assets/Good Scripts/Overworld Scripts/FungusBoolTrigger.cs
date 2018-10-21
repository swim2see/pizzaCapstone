using System.Collections;
using System.Collections.Generic;
using Fungus;
using UnityEngine;

public class FungusBoolTrigger : MonoBehaviour
{

	public Flowchart flowchart;
	public bool pHere;
	
	// Use this for initialization
	void Awake () {
		flowchart.SetBooleanVariable("NPCSpeaker", false);
		flowchart = FindObjectOfType<Flowchart>();
		pHere = false;
	}
	
	// Update is called once per frame
	void Update () {
		
		if (pHere == true && Input.GetKeyDown(KeyCode.E))
		{
			flowchart.ExecuteBlock("TalkBlock");
		}
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
