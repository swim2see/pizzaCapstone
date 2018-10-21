using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{

	public Camera selfCamera;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	private void OnTriggerEnter2D(Collider2D other)
	{
		if (other.tag == "Player")
		{
			selfCamera.enabled = true;
		}
	}
	
	private void OnTriggerExit2D(Collider2D other)
	{
		if (other.tag == "Player")
		{
			selfCamera.enabled = false;
		}
	}
}
