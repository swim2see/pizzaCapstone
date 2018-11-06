﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HarvestManager : MonoBehaviour {
    public static HarvestManager hm;
    public int ingredientCountA;
    public int ingredientCountB;
    public int ingredientCountC;

    public Text ingredientTextA;
    public Text ingredientTextB;
    public Text ingredientTextC;
    // Use this for initialization
    void Start () {
        hm = this;
	}
	
	// Update is called once per frame
	void Update () {
        ingredientTextA.text = "Ingredient A: " + ingredientCountA.ToString();
        ingredientTextB.text = "Ingredient B: " + ingredientCountB.ToString();
        ingredientTextC.text = "Ingredient C: " + ingredientCountC.ToString();
    }
}
