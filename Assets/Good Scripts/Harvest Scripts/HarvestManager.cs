using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.EventSystems;

public class HarvestManager : MonoBehaviour {
    public static HarvestManager hm;
    public int bread;
    public int cheese;
    public int sauce;
    public int meat;
    public int sock;

    public int breadCount;
    public int cheeseCount;
    public int sauceCount;
    public int meatCount;
    public int sockCount;

    public int totalIngredients;

    public Text breadText;
    public Text cheeseText;
    public Text sauceText;
    public Text meatText;
    public Text sockText;

    public List<int> bag = new List<int>();
   

    public GameObject enemyA;
    public GameObject enemyB;
    public GameObject enemyC;

    public GameObject bagObject;

    public GameObject[] buttons;
    public bool[] spellIngredient;
    // Use this for initialization
    void Start()
    {
        hm = this;
        //  bag.Add(ingredientCountA);
        //  bag.Remove(3);
        //  int temp= bag[1];

        //  for(int i = 0; i < ingredientCountA; i++)
        //  {
        //      bag.Add();
        //  }
        //}
        //generateEnemies(3,5);
    }
	// Update is called once per frame
	void Update () {
        //ingredientTextA.text = "Ingredient A: " + ingredientCountA.ToString();
        //ingredientTextB.text = "Ingredient B: " + ingredientCountB.ToString();
        //ingredientTextC.text = "Ingredient C: " + ingredientCountC.ToString();
        breadText.text = "Bread: " + breadCount.ToString();
        cheeseText.text = "Cheese: " + breadCount.ToString();
        sauceText.text = "Sauce: " + breadCount.ToString();
        meatText.text = "Meat: " + breadCount.ToString();
        sockText.text = "Socks: " + breadCount.ToString();
        if (totalIngredients == 3)
        {
            makeASpell();
        }
    }
    public void BagAddition()
    {
        bagObject.transform.DOPunchScale(new Vector3(1, 1, 0), .5f, 1, 0);
    }

    public void Button1()
    {
        buttons[0].GetComponent<Image>().color = Color.red;
        totalIngredients++;
        spellIngredient[0] = true;
        bag.Remove(bread);
        breadCount--;

    }
    public void Button2()
    {
        buttons[1].GetComponent<Image>().color = Color.red;
        totalIngredients++;
        spellIngredient[1] = true;
        bag.Remove(sauce);
        sauceCount--;
    }
    public void Button3()
    {
        buttons[2].GetComponent<Image>().color = Color.red;
        totalIngredients++;
        spellIngredient[2] = true;
        bag.Remove(cheese);
        cheeseCount--;
    }
    public void Button4()
    {
        buttons[3].GetComponent<Image>().color = Color.red;
        totalIngredients++;
        spellIngredient[3] = true;
        bag.Remove(meat);
        meatCount--;
    }
    public void Button5()
    {
        buttons[4].GetComponent<Image>().color = Color.red;
        totalIngredients++;
        spellIngredient[4] = true;
        bag.Remove(sock);
        sockCount--;
    }
    public void makeASpell()
    {
        if(spellIngredient[0] && spellIngredient[1] && spellIngredient[2])
        {
            print("PIZZA TIME");
            for (int i = 0; i < buttons.Length; i++)
            {
                buttons[i].GetComponent<Image>().color = Color.white;
            }
        }
        if (spellIngredient[0] && spellIngredient[1] && spellIngredient[3])
        {
            print("PIZZA TIME");
            for (int i = 0; i < buttons.Length; i++)
            {
                buttons[i].GetComponent<Image>().color = Color.white;
            }
        }
        if (spellIngredient[0] && spellIngredient[1] && spellIngredient[4])
        {
            print("PIZZA TIME");
            for (int i = 0; i < buttons.Length; i++)
            {
                buttons[i].GetComponent<Image>().color = Color.white;
            }
        }
        if (spellIngredient[1] && spellIngredient[2] && spellIngredient[3])
        {
            print("PIZZA TIME");
            for (int i = 0; i < buttons.Length; i++)
            {
                buttons[i].GetComponent<Image>().color = Color.white;
            }
        }
        if (spellIngredient[1] && spellIngredient[2] && spellIngredient[4])
        {
            print("PIZZA TIME");
            for (int i = 0; i < buttons.Length; i++)
            {
                buttons[i].GetComponent<Image>().color = Color.white;
            }
        }
        if (spellIngredient[2] && spellIngredient[3] && spellIngredient[4])
        {
            print("PIZZA TIME");
            for (int i = 0; i < buttons.Length; i++)
            {
                buttons[i].GetComponent<Image>().color = Color.white;
            }
        }
        if (spellIngredient[1] && spellIngredient[2] && spellIngredient[5])
        {
            print("PIZZA TIME");
            for (int i = 0; i < buttons.Length; i++)
            {
                buttons[i].GetComponent<Image>().color = Color.white;
            }
        }
        if (spellIngredient[2] && spellIngredient[3] && spellIngredient[4])
        {
            print("PIZZA TIME");
            for (int i = 0; i < buttons.Length; i++)
            {
                buttons[i].GetComponent<Image>().color = Color.white;
            }
        }
        if (spellIngredient[2] && spellIngredient[3] && spellIngredient[5])
        {
            print("PIZZA TIME");
            for (int i = 0; i < buttons.Length; i++)
            {
                buttons[i].GetComponent<Image>().color = Color.white;
            }
        }
    
   


    }

    //public void generateEnemies(int min, int max)
    //{
    //    //Determines the Length of the List
    //    int num = (int)Random.Range(min, max);

    //    //Spawns list.length number of enemies
    //    for (int i = 0; i < num; i++)
    //    {

    //        //Grabs relevant component of Chef
    //        Vector3 temp = new Vector3(i * 4f, 0f, 0f) + gameObject.transform.position;
    //        Instantiate(enemyA, temp - new Vector3(3, 0, 0), Quaternion.identity);
    //        Instantiate(enemyB, temp - new Vector3(3, 0, 0), Quaternion.identity);
    //        Instantiate(enemyC, temp - new Vector3(3, 0, 0), Quaternion.identity);

    //        //Sets enemy instance to be child of this object


    //    }
    //}
}
