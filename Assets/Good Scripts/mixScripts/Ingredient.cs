using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Ingredient : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
	public int ingIdentity;

	public GameObject pan;

	public int damage;

	public Image ingImage;

	public bool ingChecked;

	public Sprite cheese;
	public Sprite milk;
	public Sprite bread;
	public Sprite tomato;
	public Sprite nail;
	public Sprite keys;
	public Sprite coins;

	void Start()
	{
		ingImage = GetComponent<Image>();
		ingChecked = false; 

		if (ingIdentity == 1)
		{
			ingImage.sprite = coins;
			damage = 0;
		} else 
		if (ingIdentity == 2)
		{
			ingImage.sprite = nail;
			damage = 0;
		}
		else 
		if (ingIdentity == 3)
		{
			ingImage.sprite = keys;
			damage = 0;
		} else 
		if (ingIdentity == 4)
		{
			ingImage.sprite = bread;
			damage = 3;
		}  else 
		if (ingIdentity == 5)
		{
			ingImage.sprite = tomato;
			damage = 3;
		} else 
		if (ingIdentity == 6)
		{
			ingImage.sprite = milk;
			damage = 3;
		} else 
		if (ingIdentity == 7)
		{
			ingImage.sprite = cheese;
			damage = 3;
		}
	}


	public void OnBeginDrag(PointerEventData eventData) {
		Debug.Log ("OnBeginDrag");
	}

	public void OnDrag(PointerEventData eventData) {
		Debug.Log ("OnDrag");

		this.transform.position = eventData.position;
	}
	
	public void OnEndDrag(PointerEventData eventData) {
		Debug.Log ("OnEndDrag");
	}
}
