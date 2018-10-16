using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour {

    //player attributes go here
    [Header("Attributes")]
    public float health;
    public float maxHealth;
    public float mana;

    //All visual related info goes here
    [Header("UI/Visuals")]
    public Image playerHealthBar;


    // Use this for initialization
    void Start ()
    {
	    maxHealth = 100;
	    health = 100;
    }
	
	// Update is called once per frame
	void Update () {
        playerHealthBar.fillAmount = health / maxHealth;
		if (health <= 0)
		{
			Destroy(this.gameObject);
		}
	}
}
