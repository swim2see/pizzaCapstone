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

    
    public void BreadSpell(int flavoring, int garnish)
    {

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

            default:
                break;

        }

        return temp;
    }

     
}
