using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class makingSpells : MonoBehaviour {


	// Use this for initialization
	void Start () {
		//BREAD attribute does this: Highest potency as BASE, very weak as GARNISH (+20 damage, +5, *1)
        //CHEESE attribute does this: Highest potency as GARNISH, Weak potency as BASE (+5,+10,*3)
        //SAUCE attribute does this: Highest potency as BASE (+25, +5,*1.5)
        //MEAT attribute does this: Highest potency as FLAVORING (+10, +25, *.25)
        //SOCK attribute does this: Damages self in healing spells, heals enemies in damaging spells 

	}
	
	// Update is called once per frame
	void Update () {
		
	}

    //Damage Spell
    public float BreadSpell(int flavoring, int garnish)
    {
        float temp = 0;
        if (flavoring > 0) {
             temp = Random.Range(0f,flavoring);
        }
        else if(flavoring<0)
        {
             temp = Random.Range(flavoring, 0f);
        }
        switch (garnish)
        {
            case 0:
                temp *= 1;
                break;
            case 1:
                temp *= 3;
                break;
            case 2:
                temp *= 1.5f;
                break;
            case 3:
                temp *= 4;
                break;
            case 4:
                temp *= 5;
                break;
            default:
                break;
        }
        return temp + 20;
    }
    //Heal
    public float CheeseSpell(int flavoring, int garnish)
    {
        float temp = 0;
        if (flavoring > 0)
        {
            temp = Random.Range(0f, flavoring);
        }
        else if (flavoring < 0)
        {
            temp = Random.Range(flavoring, 0f);
        }
        switch (garnish)
        {
            case 0:
                temp *= 1;
                break;
            case 1:
                temp *= 3;
                break;
            case 2:
                temp *= 1.5f;
                break;
            case 3:
                temp *= 4;
                break;
            case 4:
                temp *= 5;
                break;
            default:
                break;
        }
        return temp+5;
    }
    //Heal
    public float SauceSpell(int flavoring, int garnish)
    {

        //Randomizes damage inflicted, based on integer passed
        float temp = Random.Range(0f, Mathf.Abs(flavoring));

        switch (garnish)
        {
            case 0:
                temp *= 1;
                break;
            case 1:
                temp *= 3;
                break;
            case 2:
                temp *= 1.5f;
                break;
            case 3:
                temp *= 4;
                break;
            case 4:
                temp *= 5;
                break;
            default:
                break;
        }

        return temp + 25;
    }
    //Damage
    public float MeatSpell(int flavoring, int garnish) {

        //Randomizes damage inflicted, based on integer passed
        float temp = Random.Range(0f, Mathf.Abs(flavoring));

        switch (garnish)
        {
            case 0:
                temp *= 1;
                break;
             case 1:
                temp *= 3;
                break;
            case 2:
                temp *= 1.5f;
                break;
            case 3:
                temp *= 4;
                break;
            case 4:
                temp *= 5;
                break;
            default:
                break;

        }

        return temp/2;
    }
    
    //Damage(???)
    public float SockSpell(int flavoring, int garnish)
    {

        //Randomizes damage inflicted, based on integer passed
        float temp = Random.Range(0f, Mathf.Abs(flavoring));

        switch (garnish)
        {
            case 0:
                temp *= 1;
                break;
            case 1:
                temp *= 3;
                break;
            case 2:
                temp *= 1.5f;
                break;
            case 3:
                temp *= 4;
                break;
            case 4:
                temp *= 5;
                break;
            default:
                break;
        }

        return temp;
    }


}
