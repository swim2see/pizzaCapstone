using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pupils : MonoBehaviour {

	GameObject player;
	Rigidbody2D rb;
	public float spd;
	public float distance = 100f;
	bool isDragging;

	private int animationFrames = 20;
	private int frameCount;
	
	
	private Vector3 startingPosition;

	private Vector3 targetPos;
	
	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody2D>();
		//cam = GameObject.Find("Main Camera").GetComponent<CamControl>();
		player = GameObject.FindWithTag("Player");
		startingPosition = transform.localPosition;
		targetPos = startingPosition;
	}
	
	// Update is called once per frame
	void Update () {
		
		GameObject playerObj;
		Vector3 playerPos;
		playerObj = GameObject.FindWithTag("Player");
		playerPos = playerObj.transform.position;
		Vector3 vel;

		Vector3 localMouse = transform.InverseTransformPoint(Camera.main.ScreenToWorldPoint(Input.mousePosition));
			//transform.InverseTransformPoint(Camera.main.ScreenToWorldPoint(Input.mousePosition));
		Vector3 mouseDir = Vector3.Normalize(localMouse - startingPosition);
		targetPos = distance * mouseDir;
		transform.localPosition = targetPos;

		/*if (Vector2.Distance(startingPosition, transform.position) < 0.00005f)
		{
			transform.position = Vector2.Lerp(transform.position, Camera.main.ScreenToWorldPoint(Input.mousePosition), moveSpeed);
		}*/

	}

	void Awake()
	{
		transform.localPosition = startingPosition;
	}


}
