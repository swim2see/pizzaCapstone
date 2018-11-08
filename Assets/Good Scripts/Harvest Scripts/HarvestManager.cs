using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class HarvestManager : MonoBehaviour {
    public static HarvestManager hm;
    public int ingredientCountA;
    public int ingredientCountB;
    public int ingredientCountC;

    public Text ingredientTextA;
    public Text ingredientTextB;
    public Text ingredientTextC;

    public List<ingredientClass> bag = new List<ingredientClass>();

    public GameObject enemyA;
    public GameObject enemyB;
    public GameObject enemyC;

    public GameObject bagObject;
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
        ingredientTextA.text = "Ingredient A: " + ingredientCountA.ToString();
        ingredientTextB.text = "Ingredient B: " + ingredientCountB.ToString();
        ingredientTextC.text = "Ingredient C: " + ingredientCountC.ToString();
    }
    public void BagAddition()
    {
        bagObject.transform.DOPunchScale(new Vector3(1, 1, 0), .5f, 1, 0);
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
