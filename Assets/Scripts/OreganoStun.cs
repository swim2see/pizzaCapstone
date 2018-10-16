using System.Collections;
using System.Collections.Generic;
using System.Timers;
using UnityEngine;
using UnityEngine.UI;

public class OreganoStun : MonoBehaviour
{
	
	public float maxTime = 100f;
	public float curTime = 0f;
	
	private BoxCollider2D barCollider;
	private Vector3 boxMovement;

	public Image Bar;

	public int gameState;
	public GameObject oreganoMinigame;
	
	public GameManager gm;
	public Minigames mg;

	// Use this for initializatio

	private void OnEnable()	
	{
			//an image's box collider doesn't scale in unity, so we have to scale it through code.
			barCollider = GetComponent<BoxCollider2D>();
			//barX = barCollider.transform.position.x;


			//Time.timeScale = .1f; 
			curTime = 0f;
			//at the start of script, 
			InvokeRepeating("increaseTime", 0f, 0.03f);
	}

	void increaseTime()
	{
		if (gameState == 1)
		{
			curTime += 1f;
			// The bar's fill amount is locked to 0-1, so we need to set its fill amount to a fraction
			float calcTime = curTime / maxTime;
			setPress(calcTime);
		}
	}

	void setPress(float theTime)
	{
		Bar.fillAmount = theTime;
		//this make's the bar's boxCollider larger over time. 304 is an arbitrary number that happens to have
		//the collider scale with the bar. There's a more optimal way to write this, but idk what it is.
		
			if (Bar.fillAmount < 1f)
			{
				barCollider.size = new Vector3(theTime * 17, 1, -1);
			}
			else
			{
				if (gm.timer <= .1f)
				{
				Bar.fillAmount = 0;
				//we must cancel the invoke, because it will continue to invoke even after the object is deactivated 
				CancelInvoke();

				OreganoCircle[] circles = mg.GetComponentsInChildren<OreganoCircle>();
				Debug.Log(circles.Length);

				foreach (OreganoCircle c in circles)
				{
					if (c.butResult == 1)
					{
						mg.score += 5;
					}

					c.Reset();
					Debug.Log(c.butDead);
				}

				//oCircles.Reset();
				oreganoMinigame.gameObject.SetActive(false);
			}
		}
	}
}
