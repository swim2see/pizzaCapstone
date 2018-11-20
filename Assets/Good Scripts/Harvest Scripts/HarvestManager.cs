using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.EventSystems;

public class HarvestManager : MonoBehaviour {
    public static HarvestManager hm;
    [Header ("Bag Ints")]
    public int bread;
    public int cheese;
    public int sauce;
    public int meat;
    public int sock;
    public int numIngredients;

    [Header ("Ingredient Counts")]
    public int breadCount;
    public int cheeseCount;
    public int sauceCount;
    public int meatCount;
    public int sockCount;

    public int totalIngredients;
    [Header ("Text")]
    public Text breadText;
    public Text cheeseText;
    public Text sauceText;
    public Text meatText;
    public Text sockText;
    public Text spellText;
    public Text menuText;

    [Header ("Bag")]
    public List<int> bag = new List<int>();

    public GameObject allMyEnemies;
    public GameObject buttonTray;

    public GameObject enemyA;
    public GameObject enemyB;
    public GameObject enemyC;

    public GameObject bagObject;

    public GameObject[] buttons;
    public bool[] spellIngredient;

    public enemyHarvest eH;

    public int gameState;

    float collectTimer;
    public float totalCollectTimer;
    public Image radialTimer;
    public GameObject entireTimer;

    public GameObject optionSelectButtons;
    public GameObject fireSpellButton;

    float gameFeelTimer;

    public int castability;

    public Button cook;

    public enemyHarvest bossTurnActions;
    public GameObject bossEnemy;

    public HarvestPlayer p;
    public GameObject playergameObject;

    public GameObject ingredientMenu;

    bool listOut;

    // Use this for initialization
    void Start()
    {
        gameState = 0;
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
        listOut = false;
    }
	// Update is called once per frame
	void Update () {
        if (!listOut)
        {
            ingredientMenu.SetActive(false);
        }
        numIngredients = (int)(Mathf.Clamp01(breadCount) + Mathf.Clamp01(cheeseCount) + Mathf.Clamp01(sauceCount) + Mathf.Clamp01(meatCount) + Mathf.Clamp01(sockCount));
        //print(numIngredients);
        print(gameState);
        if(numIngredients >= 3)
        {
            cook.interactable = true;
        }
        else
        {
            cook.interactable = false;
        }

        if (gameState == 0)
        {
            allMyEnemies.SetActive(false);
            entireTimer.gameObject.SetActive(false);
            optionSelectButtons.SetActive(true);
            fireSpellButton.SetActive(false);
            buttonTray.SetActive(false);
            bossEnemy.SetActive(false);
            playergameObject.SetActive(false);
            
           
        }
        else
        {
            optionSelectButtons.SetActive(false);
        }
        if (gameState == 1)
        {
            fireSpellButton.SetActive(false);
            buttonTray.SetActive(false);
            allMyEnemies.SetActive(true);
            collectTimer += Time.deltaTime;
            radialTimer.fillAmount = collectTimer / totalCollectTimer;

            //GameObject[] numberOfEnemies = GameObject.FindGameObjectsWithTag("Enemy");
            //if (numberOfEnemies.Length < 16)
            //{
            //    for (int i = 0; i < 3; i++)
            //    {
            //        Instantiate(enemyA, transform.position, Quaternion.Euler(new Vector3 (0,0,-90)));
            //        Instantiate(enemyB, transform.position, Quaternion.identity);
            //        Instantiate(enemyC, transform.position, Quaternion.identity);
            //    }
                
            //}
            if (collectTimer >= totalCollectTimer)
            {
                gameState = 3;
                gameFeelTimer = 2;
                allMyEnemies.SetActive(false);
                entireTimer.SetActive(false);

            }
        }
        if (gameState == 2)
        {
            buttonTray.SetActive(true);
            fireSpellButton.SetActive(true);
            bossEnemy.SetActive(true);
            playergameObject.SetActive(true);
        }
       

        if (gameState == 3)
        {
            bossEnemy.SetActive(true);
            playergameObject.SetActive(true);
            gameFeelTimer -= Time.deltaTime;
            if (gameFeelTimer <= 0)
            {
                gameState = 4;
            }
        }
        if (gameState == 4)
        {
            bossEnemy.SetActive(true);
            playergameObject.SetActive(true);
            gameFeelTimer = 2;
            bossTurnActions.attack();
            gameState = 6;
            
            //gameFeelTimer = 3;
            
        }
        if (gameState == 6)
        {
            gameFeelTimer -= Time.deltaTime;
            if (gameFeelTimer <= 0)
            {
                
                gameState = 0;
            }
            
        }
       
        //ingredientTextA.text = "Ingredient A: " + ingredientCountA.ToString();
        //ingredientTextB.text = "Ingredient B: " + ingredientCountB.ToString();
        //ingredientTextC.text = "Ingredient C: " + ingredientCountC.ToString();
        breadText.text = "Bread: " + breadCount.ToString();
        cheeseText.text = "Cheese: " + cheeseCount.ToString();
        sauceText.text = "Sauce: " + sauceCount.ToString();
        meatText.text = "Meat: " + meatCount.ToString();
        sockText.text = "Socks: " + sockCount.ToString();
        
    }
   
    public void BagAddition()
    {
        bagObject.transform.DOPunchScale(new Vector3(1, 1, 0), .5f, 1, 0);
    }

    public void Button1()
    {
        if (!spellIngredient[0] && breadCount>0)
        {
            buttons[0].GetComponent<Image>().color = Color.red;
            totalIngredients++;
            spellIngredient[0] = true;
            bag.Remove(bread);
            breadCount--;
        }

    }
    public void Button2()
    {
        if (!spellIngredient[1] && cheeseCount > 0)
        {
            buttons[1].GetComponent<Image>().color = Color.red;
            totalIngredients++;
            spellIngredient[1] = true;
            bag.Remove(cheese);
            cheeseCount--;
        }
    }
    public void Button3()
    {
        if (!spellIngredient[2] && sauceCount > 0)
        {
            buttons[2].GetComponent<Image>().color = Color.red;
            totalIngredients++;
            spellIngredient[2] = true;
            bag.Remove(sauce);
            sauceCount--;
        }
    }
    public void Button4()
    {
        if (!spellIngredient[3] && meatCount > 0)
        {
            buttons[3].GetComponent<Image>().color = Color.red;
            totalIngredients++;
            spellIngredient[3] = true;
            bag.Remove(meat);
            meatCount--;
        }
    }
    public void Button5()
    {
        if (!spellIngredient[4] && sockCount > 0)
        {
            buttons[4].GetComponent<Image>().color = Color.red;
            totalIngredients++;
            spellIngredient[4] = true;
            bag.Remove(sock);
            sockCount--;
        }
    }
    public void ButtonSelectCollect()
    {
        gameState = 1;
        collectTimer = 0;
        entireTimer.gameObject.SetActive(true);
        //totalCollectTimer = 3;
        
    }
    public void ButtonSelectCook()
    {
        gameState = 2;

    }
    public void SendSpell()
    {
        if (totalIngredients == 3)
        {
            MakeASpell();
        }
       
    }
    public void MakeASpell()
    {
        //0= bread
        //1= cheese
        //2=sauce
        //3=meat
        //4=sock
        if(spellIngredient[0] && spellIngredient[1] && spellIngredient[2])
        {
            print("PIZZA TIME");
            spellText.text = "PIZZA SLAM!";
            eH.health -= 35;
           
        }
        if (spellIngredient[0] && spellIngredient[1] && spellIngredient[3])
        {
            print("PIZZA TIME");
            spellText.text = "Meatball Parmageddon";
            eH.health -= 25;


        }
        if (spellIngredient[0] && spellIngredient[1] && spellIngredient[4])
        {
            print("PIZZA TIME");
            spellText.text = "Socked Cheese";
            eH.health -= 3;

        }
        if (spellIngredient[1] && spellIngredient[2] && spellIngredient[3])
        {
            print("PIZZA TIME");
            spellText.text = "Chicken Parm Pulverizer";
            eH.health -= 20;

        }
        if (spellIngredient[0] && spellIngredient[2] && spellIngredient[3])
        {
            print("PIZZA TIME");
            spellText.text = "Meatball Submission";
            eH.health -= 15;

        }
        if (spellIngredient[1] && spellIngredient[3] && spellIngredient[4])
        {
            print("PIZZA TIME");
            spellText.text = "Food Abomination";
            eH.health -= 1;
        }
        if (spellIngredient[1] && spellIngredient[2] && spellIngredient[4])
        {
            print("PIZZA TIME");
            spellText.text = "Sock Soup w/ Cheese";
            eH.health -= 4;
        }

        if (spellIngredient[2] && spellIngredient[3] && spellIngredient[4])
        {
            print("PIZZA TIME");
            spellText.text = "Spaghetti and Feetballs (Spaghetti not included)";
            eH.health -= 8;
        }
        playergameObject.transform.DOPunchScale(new Vector3(0, 5f, 0), .5f, 1, 0);


        for (int i = 0; i < buttons.Length; i++)
        {
            buttons[i].GetComponent<Image>().color = Color.white;
        }
        totalIngredients = 0;
        spellIngredient[0] = false;
        spellIngredient[1] = false;
        spellIngredient[2] = false;
        spellIngredient[3] = false;
        spellIngredient[4] = false;


        gameState = 3;
        gameFeelTimer = 1f;
    }
    public void BagCheck()
    {
        
        if (gameState == 0 && listOut==false) {
            listOut = true;
            ingredientMenu.SetActive(true);
            menuText.text = "Bread: " + breadCount.ToString() + "\n" + "Cheese: " + cheeseCount.ToString() + "\n" + "Sauce: " + sauceCount.ToString() + "\n" + "Meat:" + meatCount.ToString() + "\n" + "Socks:" + sockCount.ToString();
        }
        if (listOut == true && Input.GetMouseButtonDown(0))
        {
            listOut = false;
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
