using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class enemyHarvest : MonoBehaviour {
    public float health;
    public float maxHealth;
    public Image healthBar;
	// Use this for initialization
	void Start () {
        health = 100;
        maxHealth = 100;
	}
	
	// Update is called once per frame
	void Update () {
        healthBar.fillAmount = health / maxHealth;
        print(health / maxHealth);
	}
}
