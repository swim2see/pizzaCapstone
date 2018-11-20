using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Bag : MonoBehaviour {

	private Ingredient[] ingredientList;
	//public int ingIdentity; 

	public Image ingredientPrefab;

	public GameObject pan;

	public Text damageText;

	[Header("Ingredient Images")]
	public Sprite cheese;
	public Sprite milk;
	public Sprite bread;
	public Sprite tomato;
	public Sprite nail;
	public Sprite keys;
	
	
	
	// Use this for initialization
	void Start () {
		//
		generateIngredients(30);
	}
	
	// Update is called once per frame
	void Update ()
	{
		
	}

	public void generateIngredients(int num)
	{
		ingredientList = new Ingredient[num];

		for (int i = 0; i < num; i++)
		{
			
			Vector3 temp = new Vector3(i+  Random.Range(-40f,10f), Random.Range(-35f,15f), 0f) + gameObject.transform.position;
			ingredientList[i] = Instantiate(ingredientPrefab, gameObject.transform.position - temp, Quaternion.identity).GetComponent<Ingredient>();
			//this makes the ingredients part of the canvas
			ingredientList[i].transform.SetParent (GameObject.FindGameObjectWithTag("Bag").transform, false);
			ingredientList[i].ingIdentity = Random.Range(1, 8);

		}
	}

	
}
