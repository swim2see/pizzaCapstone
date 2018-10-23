using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class EnemyChefRevamp : Enemy
{
    void Start()
    {
        health = 100;
        maxHealth = 100;
        mana = 50;
        glowObj.SetActive(false);
    }
    void Update()
    {
        enemyHealthBar.fillAmount = health / maxHealth;
    }
    public override void attack()
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
                isDefending = true;

            }
        }
        if (health < 10)
        {
            heal();
        }

        if (health <= 0)
        {
            //Destroy(this.gameObject);
            gameObject.SetActive(false);
        }

    }



    public void heal()
    {
        health += (Random.Range(5, 10));//heal by ten percent
        mana -= (maxMana * .5f);
        enemyBarks.text = "Ayy, I'm healing here!";

    }

    public void ResetEnemyBark()
    {
        enemyBarks.text = "";
    }
    public void normAttack()
    {
        transform.DOPunchScale(new Vector3(0, .01f, 0), .5f, 1, 0);
        

        print("IM REALLY LAYING THE HURT");
        if (Player.p.effectInt != 2)
        {

        }
        GameManager.gm.p.health -= (int)Random.Range(10, 15);
        if (GameManager.gm.gameState == 3)
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