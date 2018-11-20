using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
public class enemyHarvest : MonoBehaviour {
   

    [Header("Attributes")]
    public float health;
    public float maxHealth;
    public Image healthBar;
    public float mana;
    public float maxMana;
    public bool isDefending;
    //public GameObject glowObj;
    //public Image enemyImage;

    public Text enemyBarks;
    public string[] possibleBarks;

    public bool halfAttack;

    public bool dead;
    //Prefabs & Visuals
    [Header("Visuals")]
    public Image enemyHealthBar;
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

public void attack()
{
    if (health <= 100 && health > 75)
    {
        //perform attack
        normAttack();

    }
    if (health <= 75 && health > 50)
    {
        //perform attack or heal
        if (Random.Range(0, 1) > 0.5f)
        {
            normAttack();

        }
        else
        {
           normAttack();
            enemyBarks.text = "Take this!";
        }
    }
    if (health < 10)
    {
        heal();
    }

    if (health <= 0)
    {
        Destroy(gameObject);
        //Image im = this.gameObject.GetComponent<Image>();
        //Destroy(im);

    }

}



public void heal()
{
    health += (Random.Range(5, 10));//heal by ten percent
    //mana -= (maxMana * .5f);
    enemyBarks.text = "Ayy, I'm healing here!";

}

public void ResetEnemyBark()
{
    enemyBarks.text = "";
}

public void normAttack()
{
    transform.DOPunchScale(new Vector3(0, 5f, 0), .5f, 1, 0);



    if (halfAttack != true)
    {
        HarvestManager.hm.p.health -= (int)Random.Range(15, 20);
    }
    else
    {
        print("halfattack" + halfAttack);
        HarvestManager.hm.p.health -= (int)Random.Range(5, 7);
    }

    if (HarvestManager.hm.gameState == 4)
    {
        int barkNumber = Random.Range(0, 3);
        enemyBarks.text = possibleBarks[barkNumber].ToString();

    }

    else
    {
        enemyBarks.text = "";
    }
}


}
