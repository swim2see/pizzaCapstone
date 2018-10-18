using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableObject : MonoBehaviour
{

	public bool inventory;
	public bool openable;
	public bool locked;
	public bool talks; //if true, the object can talk to the player

	public GameObject itemNeed;
	public string message;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
