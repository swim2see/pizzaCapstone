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

    public bool finalAttack;

    public bool dead;

    public bool randomize;
    public float ranBark;
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
        print(health);

        if (HarvestManager.hm.gameState != 1 && HarvestManager.hm.gameState!=0)
        {
            print("WHY");
            gameObject.GetComponent<RectTransform>().DOAnchorPos(new Vector2(133, 110), .5f);
            //transform.DOMove(new Vector2(133, 103), .5f);
        }
        else if(HarvestManager.hm.gameState==1)
        {
            enemyBarks.text = "Are you just...picking up food? In front of me?";
            RectTransform rectPosition = gameObject.GetComponent<RectTransform>();
            //HarvestManager.hm.bossEnemy.SetActive(true);
            //transform.position = new Vector2(-12, -184);
            if (rectPosition.anchoredPosition.x != 10)
            {
                gameObject.tag = "Untagged";
            }
            else
            {
                gameObject.tag = "The Boss";
            }
            gameObject.GetComponent<RectTransform>().DOAnchorPos(new Vector2(10, -192), .5f);
            
        }else if (HarvestManager.hm.gameState == 0)
        {
            gameObject.GetComponent<RectTransform>().DOAnchorPos(new Vector2(133, 110), .5f);
            if (randomize == true)
            {
                 ranBark = Random.Range(0f, 1f);
                randomize = false;
            }
            print(ranBark);
            if (ranBark >= 0f && ranBark< .25f) {
                enemyBarks.text = "Make your move kid!";
            }else if(ranBark>=.25f && ranBark < .5f)
            {
                enemyBarks.text = "I'm waiting!";
            }else if (ranBark >= .5f && ranBark<.75f)
            {
                enemyBarks.text = "Hurry it up so I can slam ya!";
            }
            else
            {
                enemyBarks.text = "Less thinking, more me hitting you!";
            }
            
        }
        
        //print(health / maxHealth);
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
           spawnEnemies();
            enemyBarks.text = "You want socks pal? WELL I GOT EM FOR YA!";
        }
    }
    if (health < 10)
    {
            if (Random.Range(0, 1) > .5f)
            {
                heal();
            }
            else
            {
                normAttack();
                finalAttack = true;
            }
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
    health += (Random.Range(5, 7));//heal by ten percent
    //mana -= (maxMana * .5f);
    enemyBarks.text = "Ayy, I'm healing here!";

}

public void ResetEnemyBark()
{
    enemyBarks.text = "";
}

public void spawnEnemies()
    {
        HarvestManager.hm.sockDrop = true;
        
    }
public void normAttack()
{
    transform.DOPunchScale(new Vector3(0, 5f, 0), .5f, 1, 0);



    if (finalAttack != true)
    {
        HarvestManager.hm.p.health -= (int)Random.Range(15, 20);
    }
    else
    {
        //print("halfattack" + halfAttack);
        HarvestManager.hm.p.health -= (int)Random.Range(25, 40);
            enemyBarks.text = "I've had enough of this rigaramole!";
    }

    if (HarvestManager.hm.gameState == 4)
    {
        int barkNumber = Random.Range(0, 10);
        enemyBarks.text = possibleBarks[barkNumber].ToString();

    }

    else
    {
        enemyBarks.text = "";
    }
}


}
