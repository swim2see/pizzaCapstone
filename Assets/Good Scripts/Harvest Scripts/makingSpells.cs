using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class makingSpells : MonoBehaviour {


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    //Heal Spell
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
            case 1:
                temp *= 1;
                break;
            case 2:
                temp *= 2;
                break;
            case 3:
                temp *= 3;
                break;
            case 4:
                temp *= 4;
                break;
            case 5:
                temp *= 5;
                break;
            default:
                break;
        }
        return temp;
    }
    //Damage
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
            case 1:
                temp *= 1;
                break;
            case 2:
                temp *= 2;
                break;
            case 3:
                temp *= 3;
                break;
            case 4:
                temp *= 4;
                break;
            case 5:
                temp *= 5;
                break;
            default:
                break;
        }
        return temp;
    }
    //Damage
    public float MeatSpell(int flavoring, int garnish) {

        //Randomizes damage inflicted, based on integer passed
        float temp = Random.Range(0f, Mathf.Abs(flavoring));

        switch (garnish)
        {
            case 1:
                temp *= 1;
                break;
             case 2:
                temp *= 2;
                break;
            case 3:
                temp *= 3;
                break;
            case 4:
                temp *= 4;
                break;
            case 5:
                temp *= 5;
                break;
            default:
                break;

        }

        return temp;
    }
    public float SauceSpell(int flavoring, int garnish)
    {

        //Randomizes damage inflicted, based on integer passed
        float temp = Random.Range(0f, Mathf.Abs(flavoring));

        switch (garnish)
        {
            case 1:
                temp *= 1;
                break;
            case 2:
                temp *= 2;
                break;
            case 3:
                temp *= 3;
                break;
            case 4:
                temp *= 4;
                break;
            case 5:
                temp *= 5;
                break;
            default:
                break;

        }

        return temp;
    }


}
