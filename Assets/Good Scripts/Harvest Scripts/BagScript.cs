using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BagScript : MonoBehaviour {


	public Text bagAdditionText;
	public GameObject bagAdditionHolder;
	public GameObject bagAdditionDestination;

	private Vector3 oldPos;
	
	// Use this for initialization
	void Start () {
		oldPos = bagAdditionHolder.GetComponent<RectTransform>().position;
		bagAdditionText.text = " ";
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	public IEnumerator IngCollected(string x)
	{
		
		
		for (int i = 0; i < 30; i++)
		{
			yield return new WaitForEndOfFrame();
			bagAdditionText.text = "+1 " + x;
			bagAdditionHolder.GetComponent<RectTransform>().position = new Vector3(bagAdditionHolder.GetComponent<RectTransform>().position.x, Mathf.Lerp(bagAdditionHolder.GetComponent<RectTransform>().position.y, bagAdditionDestination.transform.position.y, .05f), bagAdditionHolder.GetComponent<RectTransform>().position.z);

		}
	
		bagAdditionHolder.transform.position = oldPos;
		bagAdditionText.text = " ";
		
		/*var startTime = Time.realtimeSinceStartup;
		while (Time.realtimeSinceStartup < startTime + 2f)
		{
			//.gameObject.transform.localPosition
			/*var randomPoint = new Vector3(bagAdditionHolder.transform.localPosition.x, Random.Range(initialPos.y - 1, initialPos.y + 1), initialPos.z);
			target.localPosition = randomPoint;
			bagAdditionHolder.transform.localPosition.y += 0.1f;
			yield return null;
		}*/
		/*pendingShakeDuration = 0f;
		target.localPosition = initialPos; 
		isShaking = false;*/
	}
	
	
	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.gameObject.GetComponent<EnemyBehavior>().isDragging == true)
		{
			StartCoroutine((IngCollected((collision.gameObject.tag))));
			/*if (collision.gameObject.tag == "Sauce")
			{
				StartCoroutine(IngCollected("Sauce"));
			}

			if (collision.gameObject.tag == "Sock")
			{
				StartCoroutine(IngCollected("Sock"));
			}

			if (collision.gameObject.tag == "Meat")
			{
				StartCoroutine(IngCollected("Meat"));
			}

			if (collision.gameObject.tag == "Bread")
			{
				StartCoroutine(IngCollected("Bread"));
			}

			if (collision.gameObject.tag == "Cheese")
			{
				StartCoroutine(IngCollected("Cheese"));*/
		}
		
	}
}
	
	

