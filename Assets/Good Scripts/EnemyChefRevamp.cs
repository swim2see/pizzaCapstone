using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
                enemyBarks.text = "I'm bracing myself!";
            }
        }
        if (health < 10)
        {
            heal();
        }

        if (health <= 0)
        {
            Destroy(this.gameObject);
        }

    }



    public void heal()
    {
        health += (Random.Range(5, 10));//heal by ten percent
        mana -= (maxMana * .5f);
        enemyBarks.text = "Ayy, I'm healing here!";
    }
    public void normAttack()
    {
        print("IM REALLY LAYING THE HURT");
        GameManager.gm.p.health -= (int)Random.Range(10, 15);
        enemyBarks.text = "Spaghett' lost!";
    }


}
