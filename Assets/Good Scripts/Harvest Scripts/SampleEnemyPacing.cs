using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SampleEnemyPacing : MonoBehaviour {
	
	GameObject player;
	Rigidbody2D rb;
	public float spd;
	public float distance;
	bool isDragging;
	private float distanceCounter;
	private Vector2 centerPosition;

	public ingredientClass thisIngredient;

	// Use this for initialization
	void Start()
	{
		rb = GetComponent<Rigidbody2D>();
		//cam = GameObject.Find("Main Camera").GetComponent<CamControl>();
		player = GameObject.FindWithTag("Player");
		transform.position = new Vector2(Random.Range(-6, 6), Random.Range(-3, 3));
		centerPosition = transform.position;
		distanceCounter = 0;
		//print(transform.position);


	}
	
	// Update is called once per frame
	void Update()
	{
		//tune the *3 to make the distance traveled greater
		if (!isDragging)
		{
			foreach (GameObject sauce in HarvestManager.hm.sauceEnemyCount)
			{
				transform.position = new Vector2(Mathf.Sin(distanceCounter) * 4 + centerPosition.x, centerPosition.y);
				distanceCounter += .001f;
				if (distanceCounter > 2f * Mathf.PI)
				{
					distanceCounter = 0;
				}
			}
		}
		else
		{
			transform.position = centerPosition;
		}

	}
	
	private void OnMouseDrag()
	{
		isDragging = true;
		Vector3 mousePos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, distance);
		Vector3 objectPos = Camera.main.ScreenToWorldPoint(mousePos);
		centerPosition.x = objectPos.x;
		centerPosition.y = objectPos.y;

		transform.position = objectPos;

	}
	
	private void OnMouseUp()
	{
		isDragging = false;
	}
	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.gameObject.tag == "bag")
		{
            
			if (isDragging)
			{
				HarvestManager.hm.bag.Add(HarvestManager.hm.meat);
				HarvestManager.hm.meatCount++;
				HarvestManager.hm.BagAddition();
				Destroy(gameObject);
			}
		}
	}
}

