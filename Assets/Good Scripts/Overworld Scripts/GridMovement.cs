using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class GridMovement : MonoBehaviour
{
	private Vector3 startPos;
	private Transform mTransform;

	public AnimationCurve yCurve;
	public float curveScale = 0.01f;

	// Use this for initialization
	void Start ()
	{
		//Gathers the player's location and starting position
		mTransform = this.transform;
		startPos = mTransform.localPosition;
	}
	
	// Update is called once per frame
	void Update ()
	{
		float t = Time.deltaTime;
		float y = yCurve.Evaluate(t);

		Vector3 targetPos = Vector3.zero;
		targetPos.y = startPos.y + y;
		mTransform.localPosition = targetPos;
	}
}
