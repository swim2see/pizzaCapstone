using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public abstract class Enemy : MonoBehaviour
{
    //Key enemy attributes
    [Header("Attributes")]
    public float health;
    public float maxHealth;
    public float mana;
    public float maxMana;
    public bool isDefending;
    public GameObject glowObj;

    public Text enemyBarks;

    //Prefabs & Visuals
    [Header("Visuals")]
    public Image enemyHealthBar;

    public abstract void attack();

    public void takeDamage(float dmg)
    {
        if (isDefending)
        {
            health -= dmg / 2;
        }
        else
        {
            health -= dmg;
        }
    }
    public void takeDamage(float dmg, int type)
    {
        if (isDefending && type != 2)
        {
            health -= dmg / 2;
        }
        else
        {
            health -= dmg;
        }
        transform.DOPunchPosition(new Vector3(0,.5f,0),  .5f,  20,  1, false);

       //transform.DOMoveY(10f, 1f).SetEase(Ease.InOutBounce).OnComplete(defend);
    }

    public void defend()
    {
        isDefending = true;
    }

    public void glow()
    {
        glowObj.SetActive(true);
    }

    public void unglow()
    {
        glowObj.SetActive(false);
    }
}
