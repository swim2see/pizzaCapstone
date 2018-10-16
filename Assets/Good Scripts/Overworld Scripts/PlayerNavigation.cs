using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerNavigation : MonoBehaviour {

	private float speed = 2.0f;
	private new Vector3 direction;
	private Transform position;
 
	void Start() {
		direction = transform.position;
		position = transform;
	}
 
	void Update() {
 
		if (Input.GetKey(KeyCode.D) && position.position == direction) {
			direction += Vector3.right;
		}
		if (Input.GetKey(KeyCode.W) && position.position == direction) {
			direction += Vector3.up;
		}
		if (Input.GetKey(KeyCode.A) && position.position == direction) {
			direction += Vector3.left;
		}
		if (Input.GetKey(KeyCode.S) && position.position == direction) {
			direction += Vector3.down;
		}
     
		transform.position = Vector3.MoveTowards(transform.position, direction, Time.deltaTime * speed);
	}   
}
