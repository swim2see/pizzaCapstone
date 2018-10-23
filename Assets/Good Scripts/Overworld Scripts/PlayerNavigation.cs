using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerNavigation : MonoBehaviour {

	private float speed = 2.0f;
	private new Vector3 direction;
	private Transform position;

	public FungusBoolTrigger fT;
 
	void Start() {
		direction = transform.position;
		position = transform;
	}

	void Update()
	{

		if (fT.inMenu == false)
		{
			if ((Input.GetKey(KeyCode.D) || Input.GetAxisRaw("Horizontal") > 0) && position.position == direction)
			{
				transform.rotation = Quaternion.Euler(0, 0, -90);
				direction += Vector3.right;
			}

			if ((Input.GetKey(KeyCode.W) || Input.GetAxisRaw("Vertical") > 0) && position.position == direction)
			{
				transform.rotation = Quaternion.Euler(0, 0, 0);
				direction += Vector3.up;
			}

			if ((Input.GetKey(KeyCode.A) || Input.GetAxisRaw("Horizontal") < 0) && position.position == direction)
			{
				transform.rotation = Quaternion.Euler(0, 0, 90);
				direction += Vector3.left;
			}

			if ((Input.GetKey(KeyCode.S) || Input.GetAxisRaw("Vertical") < 0) && position.position == direction)
			{
				transform.rotation = Quaternion.Euler(0, 0, 180);
				direction += Vector3.down;
			}

			transform.position = Vector3.MoveTowards(transform.position, direction, Time.deltaTime * speed);
		}
	}
}
