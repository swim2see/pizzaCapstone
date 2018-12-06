using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.EventSystems;
using UnityScript.Lang;
using UnityEngine.SceneManagement;

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
    
    [Header ("Enemy Counts")]
    public GameObject[] numberOfEnemies;
    public GameObject[] breadEnemyCount;
    public GameObject[] cheeseEnemyCount;
    public GameObject[] sauceEnemyCount;
    public GameObject[] meatEnemyCount;
    public GameObject[] sockEnemyCount;

    public int totalIngredients;
    [Header ("Text")]
    public Text breadText;
    public Text cheeseText;
    public Text sauceText;
    public Text meatText;
    public Text sockText;
    public Text spellText;
    public Text menuText;

    [Header ("Bag Object")]
    public List<int> bag = new List<int>();
    public GameObject bagObject;
    public GameObject ingredientMenu;
    bool listOut;


    [Header("GameObject Parents")]
    public GameObject allMyEnemies;
    public GameObject buttonTray;

    [Header ("Enemy Prefabs")]
    public GameObject enemyA;
    public GameObject enemyB;
    public GameObject enemyC;
    public GameObject enemyD;

    [Header ("Public Classes")]
    public enemyHarvest eH;
    public HarvestPlayer p;
    public enemyHarvest bossTurnActions;
    public makingSpells ms;

    public int gameState;

    [Header ("Timers")]
    float collectTimer;
    public float totalCollectTimer;
    public Image radialTimer;
    public GameObject entireTimer;
    float gameFeelTimer;

    [Header("GameState Functions")]
    public GameObject optionSelectButtons;
    public GameObject fireSpellButton;
    public GameObject[] buttons;
    public bool[] spellIngredient;
    public Button cook;

    [Header("Sound")]
    public AudioSource source;
    public AudioClip error;
    public AudioClip success;
    public AudioClip spellActivate;
    public AudioClip ingAddedSound;

    //public int castability;
    
    public GameObject bossEnemy;
    public GameObject playergameObject;

    public bool sockDrop;

    // Use this for initialization
    void Start()
    {
        //ensures enemies don't activate when game starts
        allMyEnemies.SetActive(false);
        entireTimer.gameObject.SetActive(false);
        
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

        source = GetComponent<AudioSource>();
    }
	// Update is called once per frame
	void Update () {
        if (bossEnemy == null || playergameObject==null)
        {
            SceneManager.LoadScene("Overworld");
        }
        if (!listOut)
        {
            ingredientMenu.SetActive(false);
        }
        numIngredients = (int)(Mathf.Clamp01(breadCount) + Mathf.Clamp01(cheeseCount) + Mathf.Clamp01(sauceCount) + Mathf.Clamp01(meatCount) + Mathf.Clamp01(sockCount));
        //print(numIngredients);
        //print(gameState);
        if(numIngredients >= 3)
        {
            cook.interactable = true;
        }
        else
        {
            cook.interactable = false;
        }

	    //Cook/Collect selection menu 
        if (gameState == 0)
        {
            //allMyEnemies.SetActive(false);
            //entireTimer.gameObject.SetActive(false);
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
	    //Harvesting portion 
        if (gameState == 1)
        {
            bossEnemy.SetActive(true);
            //fireSpellButton.SetActive(false);
            //buttonTray.SetActive(false);
            allMyEnemies.SetActive(true);
            collectTimer += Time.deltaTime;
            radialTimer.fillAmount = collectTimer / totalCollectTimer;
            
            breadEnemyCount = GameObject.FindGameObjectsWithTag("Bread");
            cheeseEnemyCount = GameObject.FindGameObjectsWithTag("Cheese");
            sauceEnemyCount= GameObject.FindGameObjectsWithTag("Sauce");
            meatEnemyCount= GameObject.FindGameObjectsWithTag("Meat");
            sockEnemyCount= GameObject.FindGameObjectsWithTag("Sock");

            //FIGURE OUT HOW TO ADD ARRAYS TOGETHER
            //Figured it out

            numberOfEnemies = breadEnemyCount.Concat(cheeseEnemyCount).Concat(sauceEnemyCount)
                .Concat(meatEnemyCount).Concat(sockEnemyCount).ToArray();
            
            //numberOfEnemies += breadEnemyCount;
            if (sockDrop == true)
            {
                for(int j = 0; j < 3; j++)
                {
                    GameObject spawnSock = Instantiate(HarvestManager.hm.enemyD, new Vector3(j*3, j*2, 0), Quaternion.identity) as GameObject;
                    spawnSock.transform.parent = GameObject.Find("Enemies").transform;
                    if (j == 2)
                    {
                        sockDrop = false;
                    }
                }
                
            }
            //put reinstantiate code here if stuff dont work
            if (numberOfEnemies.Length < 9)
            {
                for (int i = 0; i < 2; i++)
                {
                    GameObject type1 = Instantiate(enemyA, new Vector3(0, 0, 0), Quaternion.identity) as GameObject;
                    type1.transform.parent = GameObject.Find("Enemies").transform;
                    GameObject type2 = Instantiate(enemyB, new Vector3(0, 0, 0), Quaternion.identity) as GameObject;
                    type2.transform.parent = GameObject.Find("Enemies").transform;
                    GameObject type3 = Instantiate(enemyC, new Vector3(0, 0, 0), Quaternion.identity) as GameObject;
                    type3.transform.parent = GameObject.Find("Enemies").transform;
                }

            }
                if (collectTimer >= totalCollectTimer)
            {
                gameState = 3;
                gameFeelTimer = 2;
                //Deactivates the enemies and timer
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

        //click and drag boxes to the respective target boxes (base, flavoring, garnish)
        //have boxes snap back to original position if they are not within a certain range of the targets
        //if allowing for multiple copies, instantiate new ingredient underneath old ingredient 
        //have book pop up showing the ingredients, turn pages to see what the ingredients do as various attributes 
        //box shaped icons that will fit into the target boxes 
        //incorprate the book, have tabs for ingredients

        //base is a separate equation that changes with intensity
        //one of the base effects could be protecting other ingredients
        //eating certain ingredients makes you sick 
        //standard buffs/debuffs
        
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

    //Bread button
    public void Button1()
    {
        SelectIngredient(0);
    }

    //Cheese button
    public void Button2()
    {
        SelectIngredient(1);
    }
    
    //sauce button
    public void Button3()
    {
        SelectIngredient(2);
    }
    
    //meat button
    public void Button4()
    {
        SelectIngredient(3);
    }
    
    //sock button 
    public void Button5()
    {
        SelectIngredient(4);
    }
    
    //Detects which ingredients have been selected
    public void SelectIngredient(int x)
    {
        //0= bread
        //1= cheese
        //2=sauce
        //3=meat
        //4=sock
        
        if (!spellIngredient[x])
        {   
            if (totalIngredients >= 3)
            {
                source.PlayOneShot(error);
            }
            
            if (x == 0 && breadCount > 0 && totalIngredients < 3)
            {
                buttons[x].GetComponent<Image>().color = Color.red;
                totalIngredients++;
                spellIngredient[x] = true;
                bag.Remove(bread);
                breadCount--;
            } else if (x == 1 && cheeseCount > 0 && totalIngredients < 3)
            {
                buttons[x].GetComponent<Image>().color = Color.red;
                totalIngredients++;
                spellIngredient[x] = true;
                bag.Remove(cheese);
                cheeseCount--;
            }
            else if (x == 2 && sauceCount > 0 && totalIngredients < 3)
            {
                buttons[x].GetComponent<Image>().color = Color.red;
                totalIngredients++;
                spellIngredient[x] = true;
                bag.Remove(sauce);
                sauceCount--;
            } else if (x == 3 && meatCount > 0 && totalIngredients < 3)
            {
                buttons[x].GetComponent<Image>().color = Color.red;
                totalIngredients++;
                spellIngredient[x] = true;
                bag.Remove(meat);
                meatCount--;
            } else if (x == 4 && sockCount > 0 && totalIngredients < 3)
            {
                buttons[x].GetComponent<Image>().color = Color.red;
                totalIngredients++;
                spellIngredient[x] = true;
                bag.Remove(sock);
                sockCount--;
            } 
        }
        
        //Allows you to deselect ingredients
        else if (spellIngredient[x])
        {
            buttons[x].GetComponent<Image>().color = Color.clear;
            totalIngredients--;
            spellIngredient[x] = false;
            if (x == 0)
            {
                bag.Add(bread);
                breadCount++;
            } else if (x == 1)
            {
                bag.Add(cheese);
                cheeseCount++;
            }
            else if (x == 2)
            {
                bag.Add(sauce);
                sauceCount++;
            } else if (x == 3)
            {
                bag.Add(meat);
                meatCount++;
            } else if (x == 4)
            {
                bag.Add(sock);
                sockCount++;
            }
        }
    }
    
    
    public void ButtonSelectCollect()
    {
        gameState = 1;
        collectTimer = 0;
        entireTimer.gameObject.SetActive(true);
        source.PlayOneShot(success);
        
        
       
        //totalCollectTimer = 3;
        
    }
    
    //Activates the cook gameState
    public void ButtonSelectCook()
    {
        gameState = 2;
        source.PlayOneShot(success);
    }
    
    //allows you to fire a spell
    public void SendSpell()
    {
        if (totalIngredients == 3)
        {
            MakeASpell();
            source.PlayOneShot(spellActivate);
            
        }
        else
        {
           source.PlayOneShot(error);
        }


    }
    public void MakeASpell()
    {
        //0= bread
        //1= cheese
        //2=sauce
        //3=meat
        //4=sock

        eH.health -= ms.MeatSpell(10, 1);
        //array of floats that store the values of the flavoring of each ingredient
        //for garnish, just pass in a number

        

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
            HarvestPlayer.hp.health += 10;
            eH.health -= 3;

        }
        if (spellIngredient[0] && spellIngredient[2] && spellIngredient[3])
        {
            print("PIZZA TIME");
            spellText.text = "Meatball Submission";
            eH.health -= 15;

        }
        if (spellIngredient[0] && spellIngredient[2] && spellIngredient[4])
        {
            print("PIZZA TIME");
            spellText.text = "SOMETHING";
            eH.health -= 15;

        }
        if (spellIngredient[0] && spellIngredient[3] && spellIngredient[4])
        {
            print("PIZZA TIME");
            spellText.text = "Meatball Submission";
            eH.health -= 15;

        }
        if (spellIngredient[1] && spellIngredient[2] && spellIngredient[3])
        {
            print("PIZZA TIME");
            spellText.text = "Chicken Parm Pulverizer";
            eH.health -= 20;

        }
        if (spellIngredient[1] && spellIngredient[2] && spellIngredient[4])
        {
            print("PIZZA TIME");
            spellText.text = "Sock Soup w/ Cheese";
            eH.health -= 4;
        }
        if (spellIngredient[1] && spellIngredient[3] && spellIngredient[4])
        {
            print("PIZZA TIME");
            spellText.text = "Food Abomination";
            eH.health -= 1;
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
            
        }else if (listOut == true)
        {
            ingredientMenu.SetActive(false);
            listOut = false;
        }
        
    }
}
