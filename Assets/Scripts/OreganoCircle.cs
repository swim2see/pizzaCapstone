using System.Collections;
using System.Collections.Generic;
using System.Timers;
using UnityEngine;
//using UnityEngine.Experimental.UIElements;
using UnityEngine.UI;

public class OreganoCircle : MonoBehaviour
{

	public bool butAct;
	public bool butDead;
	public int butResult = 0;

	private int inputCount = 3;
	public int correctInputs;

	public Image neutralCirc;
	public Sprite check;
	public Sprite fail;
	public Sprite neutralityCircle;

	public int score;
	
	public Minigames mg;
	
	
	// Use this for initialization
	void Start ()
	{
		//allows us to change the image of the buttons
		neutralCirc = GetComponent<Image>();
		//Time.timeScale = .1f;
	}
	
	// Update is called once per frame
	void Update()
	{
			//ensures that the already activated buttons won't be reactivated when the bar passes over the other buttons
			if (butDead == true)
			{
				butAct = false;
			}
	
			if (Input.GetButtonDown("joystick button 1"))
			{
				inputCount -= 1;
				//Debug.Log(inputCount);
			}
	
			if (butAct == true && Input.GetButtonDown("joystick button 1") && inputCount >= 0)
			{
				butResult = 1;
				neutralCirc.sprite = check;
				score += 5;
				correctInputs += 1; 
			}
	
			Debug.Log("Butt: " + butDead);
			if (butAct == true)
			{
				StartCoroutine("ActiveFrames");
				Debug.Log(butAct);
				//Debug.Log(butDead);
			}
		}

	private void OnTriggerEnter2D(Collider2D other)
	{
		if (other.gameObject.tag == "OreganoBar")
		{	
			butAct = true;
			Debug.Log("colliding!");
		}
		
	}

	//sets the amount of the time each button is active
	private IEnumerator ActiveFrames()
	{
		//.8 seconds (the amount of time the bar spends in each button) is equal to 48 frames.
		float timer = .5f;
		yield return new WaitForSeconds(timer);
		if (butResult != 1)
		{
			butResult = 2;
			neutralCirc.sprite = fail;
		}

		butAct = false;
		butDead = true;
	}

	public void Reset()
	{
		butAct = false;
		butDead = false;
		neutralCirc.sprite = neutralityCircle;
		inputCount = 3;
		butResult = 0;
		Destroy(this.gameObject);
	}


}
