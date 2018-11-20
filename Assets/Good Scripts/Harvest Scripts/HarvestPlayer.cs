using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class HarvestPlayer : MonoBehaviour {
    public static HarvestPlayer hp;
    public float health;
    public float maxHealth;
    public Image playerHealthBar;
	// Use this for initialization
	void Start () {
        hp = this;
        health = 100;
        maxHealth = 100;
	}
	
	// Update is called once per frame
	void Update () {
        playerHealthBar.fillAmount = health / maxHealth;
	}
}
