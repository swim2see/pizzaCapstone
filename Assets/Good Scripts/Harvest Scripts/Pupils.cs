using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pupils : MonoBehaviour {

	GameObject player;
	Rigidbody2D rb;
	public float spd;
	public float distance;
	bool isDragging;

	private float moveSpeed = 0.5f;

	private Vector3 startingPosition;
	
	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody2D>();
		//cam = GameObject.Find("Main Camera").GetComponent<CamControl>();
		player = GameObject.FindWithTag("Player");
		startingPosition = transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		
		GameObject playerObj;
		Vector3 playerPos;
		playerObj = GameObject.FindWithTag("Player");
		playerPos = playerObj.transform.position;
		Vector3 vel;
		if (Vector2.Distance(startingPosition, transform.position) < 0.00005f)
		{
			transform.position = Vector2.Lerp(transform.position, Camera.main.ScreenToWorldPoint(Input.mousePosition), moveSpeed);
		}
		
	}
}
