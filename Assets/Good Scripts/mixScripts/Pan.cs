using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class Pan : MonoBehaviour
{

	public int panDamage;
	private IEnumerator damageCoroutine;

	public int comboIdentity;

	public Text damageText;

	private List<GameObject> enteredIngredients = new List<GameObject>(); 

	public GameObject ingredientPrefab;
	public GameObject collided;

	public bool isColliding;

	public bool isAttacking;
	
	// Use this for initialization
	void Start ()
	{
		isAttacking = false;
	}
	
	// Update is called once per frame
	void Update ()
	{
		Debug.Log(enteredIngredients.Count);
		Debug.Log(comboIdentity +"combo");
		damageCoroutine = DamageDisplayTime(3f);
		

		/*foreach (GameObject ingredient in enteredIngredients)
		{
			//panDamage += ingredient.gameObject.GetComponent<Ingredient>().damage;
			comboIdentity += ingredient.gameObject.GetComponent<Ingredient>().ingIdentity;
		}*/

		if (enteredIngredients.Count == 3)
		{
			isAttacking = true;
			StartCoroutine(damageCoroutine);
		}
		else
		{
			damageText.text = " ";
		}


	}

	private void OnTriggerEnter2D(Collider2D other)
	{
		if (isAttacking == false)
		{

			if (other.gameObject.tag == "Ingredient")
			{
				if (other.gameObject.GetComponent<Ingredient>().ingChecked == false)
				{
					panDamage += other.gameObject.GetComponent<Ingredient>().damage;
					comboIdentity += other.gameObject.GetComponent<Ingredient>().ingIdentity;

					enteredIngredients.Add(other.gameObject);
					//Destroy(other.gameObject); 
					other.gameObject.GetComponent<Ingredient>().ingChecked = true;
					collided = other.gameObject;

					//collided.transform.position = new Vector3(collided.transform.position.x, 500f, 0f);
					other.gameObject.SetActive(false);

				}
			}
		}
	}
	
	public IEnumerator DamageDisplayTime(float waitTime)
	{
		//int letter = 0;
		//this combo identity thing is effectively a hard coded pizza recognizer. It must go for the final project
			if (comboIdentity == 16)
			{
				panDamage = 40;
				damageText.text = "PIZZA COMBO: " + panDamage;
				Debug.Log("pizza");
			}
			else
			{
				damageText.text = panDamage.ToString();
			}
			yield return new WaitForSeconds(waitTime);
			damageText.text = " ";
			enteredIngredients.Clear();
			panDamage = 0;
		isAttacking = false;

	}
}
