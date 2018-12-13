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
    public Text endGameText;

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
    public float collectTimer;
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
    public AudioClip takeDamageSound;
    public AudioClip removeIngredientSound;
    public AudioClip magicCastSpell;
    public AudioClip ingPickUp;

    [Header("Make Spells")]
    public int baseNumber;
    public int flavoring;
    public int[] flavoringValues;
    public int garnish;
    //public int castability;
    
    public GameObject bossEnemy;
    public GameObject playergameObject;

    public bool sockDrop;

    string firstIngredient;
    string secondIngredient;
    string thirdIngredient;

    string spellName1;
    string spellName2;
    string spellName3;

    public GameObject textBox;
    // Use this for initialization
    void Start()
    {
        //ensures enemies don't activate when game starts
        allMyEnemies.SetActive(false);
        entireTimer.gameObject.SetActive(false);
        
        gameState = 0;
        eH.randomize = true;
        hm = this;
        Cursor.lockState = CursorLockMode.Locked;
        listOut = false;

        source = GetComponent<AudioSource>();
    }
	// Update is called once per frame
	void Update () {

        
        if (bossEnemy == null)
        {
            endGameText.text = "You sliced him! You diced him!";
            endGameText.color = Color.green;
            Time.timeScale = 0;
            //SceneManager.LoadScene("Overworld");
        } else if(playergameObject==null)
        {
            endGameText.text = "Enjoy a slice of heaven!";
            endGameText.color = Color.red;
            Time.timeScale = 0;
        }
	    else
	    {
	        endGameText.text = " ";
	    }

	    if (!listOut)
        {
            ingredientMenu.SetActive(false);
        }
        numIngredients = (int)(Mathf.Clamp01(breadCount) + Mathf.Clamp01(cheeseCount) + Mathf.Clamp01(sauceCount) + Mathf.Clamp01(meatCount) + Mathf.Clamp01(sockCount));
     
        if(numIngredients >= 3)
        {
            cook.interactable = true;
            cook.GetComponent<Image>().color = Color.white;
        }
        else
        {
            cook.interactable = false;
        }

	    //Cook/Collect selection menu 
        if (gameState == 0)
        {
         
            optionSelectButtons.SetActive(true);
            optionSelectButtons.GetComponent<RectTransform>().DOAnchorPos(new Vector2(0, 18), 1.5f).SetEase(Ease.OutBack);
            fireSpellButton.SetActive(false);
            buttonTray.SetActive(false);
            //bossEnemy.SetActive(false);
            //playergameObject.SetActive(false);
            
           
        }
        else
        {
            optionSelectButtons.SetActive(false);
        }
	    //Harvesting portion 
        if (gameState == 1)
        {
            bossEnemy.SetActive(true);
            playergameObject.SetActive(false);
            //fireSpellButton.SetActive(false);
          
            allMyEnemies.SetActive(true);
            collectTimer += Time.deltaTime;
            radialTimer.fillAmount = collectTimer / totalCollectTimer;
            
            breadEnemyCount = GameObject.FindGameObjectsWithTag("Bread");
            cheeseEnemyCount = GameObject.FindGameObjectsWithTag("Cheese");
            sauceEnemyCount= GameObject.FindGameObjectsWithTag("Sauce");
            meatEnemyCount= GameObject.FindGameObjectsWithTag("Meat");
            sockEnemyCount= GameObject.FindGameObjectsWithTag("Sock");

           

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
            //UNCOMMENT THIS
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
                eH.enemyBarks.text = "";
                textBox.SetActive(false);
            }
        }
        if (gameState == 2)
        {
            buttonTray.SetActive(true);
            buttonTray.GetComponent<RectTransform>().DOAnchorPos(new Vector2(281, -22), 1.5f).SetEase(Ease.OutBack);
            fireSpellButton.SetActive(true);
            bossEnemy.SetActive(true);
            playergameObject.SetActive(true);
            textBox.SetActive(true);
            spellText.text = firstIngredient + secondIngredient + thirdIngredient;
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
            textBox.SetActive(true);
            playergameObject.SetActive(true);
            gameFeelTimer = 2;
            bossTurnActions.attack();
            buttonTray.GetComponent<RectTransform>().DOAnchorPos(new Vector2(281, -278), .5f).SetEase(Ease.InBack);

            source.PlayOneShot(takeDamageSound);
            gameState = 6;
            firstIngredient = "";
            secondIngredient = "";
            thirdIngredient = "";
            spellText.text = "";
            //gameFeelTimer = 3;
        }
        if (gameState == 6)
        {
            gameFeelTimer -= Time.deltaTime;
            if (gameFeelTimer <= 0)
            {
                
                gameState = 0;
                eH.randomize = true;
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
        Vector3 currentScale = bagObject.GetComponent<RectTransform>().localScale;
        bagObject.GetComponent<RectTransform>().DOPunchScale(new Vector3(5,5,0), .5f, 10, 0).SetEase(Ease.InBounce)
           .OnComplete(() => bagObject.GetComponent<RectTransform>().localScale = currentScale);
    }

    //Bread button
    
    
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
                
                //spellIngredient[x] = true;
                bag.Remove(bread);
                breadCount--;

                if (totalIngredients == 0)
                {
                    baseNumber = x;
                    firstIngredient = "Bread Base +";
                    spellName1 = "Doughy ";
                }
                else if (totalIngredients == 1)
                {
                    flavoring = flavoringValues[x];
                    secondIngredient = "Bread Flavoring +";
                    spellName2= "Carboverloaded ";
                }
                else if (totalIngredients == 2)
                {
                    garnish = x;
                    thirdIngredient = "Bread Garnish";
                    spellName3 = "Breademption!";
                }
                totalIngredients++;
            } else if (x == 1 && cheeseCount > 0 && totalIngredients < 3)
            {
                buttons[x].GetComponent<Image>().color = Color.red;
                
                //spellIngredient[x] = true;
                bag.Remove(cheese);
                cheeseCount--;

                if (totalIngredients == 0)
                {
                    baseNumber = x;
                    firstIngredient = "Cheese Base +";
                    spellName1 = "Cheesy ";
                }
                else if (totalIngredients == 1)
                {
                    flavoring = flavoringValues[x];
                    secondIngredient = "Cheese Flavoring + ";
                    spellName2 = "Mozzified ";
                }
                else if (totalIngredients == 2)
                {
                    garnish = x;
                    thirdIngredient = "Cheese Garnish";
                    spellName3 = "Lactoverkill!";
                }
                totalIngredients++;
            }
            else if (x == 2 && sauceCount > 0 && totalIngredients < 3)
            {
                buttons[x].GetComponent<Image>().color = Color.red;
               
               // spellIngredient[x] = true;
                bag.Remove(sauce);
                sauceCount--;

                if (totalIngredients == 0)
                {
                    baseNumber = x;
                    firstIngredient = "Sauce Base + ";
                    spellName1 = "Saucy ";
                }
                else if (totalIngredients == 1)
                {
                    flavoring = flavoringValues[x];
                    secondIngredient = "Sauce Flavoring + ";
                    spellName2= "Red ";
                }
                else if (totalIngredients == 2)
                {
                    garnish = x;
                    thirdIngredient = "Sauce Garnish";
                    spellName3= "Saucery!";
                }
                totalIngredients++;
            } else if (x == 3 && meatCount > 0 && totalIngredients < 3)
            {
                buttons[x].GetComponent<Image>().color = Color.red;
                
               // spellIngredient[x] = true;
                bag.Remove(meat);
                meatCount--;

                if (totalIngredients == 0)
                {
                    baseNumber = x;
                    firstIngredient = "Meat Base + ";
                    spellName1 = "Meaty ";
                }
                else if (totalIngredients == 1)
                {
                    flavoring = flavoringValues[x];
                    secondIngredient = "Meat Flavoring + ";
                    spellName2 = "Meatballistic ";
                }
                else if (totalIngredients == 2)
                {
                    garnish = x;
                    thirdIngredient = "Meat Garnish(?)";
                    spellName3 = "Beefstruction!";
                }
                totalIngredients++;
            } else if (x == 4 && sockCount > 0 && totalIngredients < 3)
            {
                buttons[x].GetComponent<Image>().color = Color.red;
                
               // spellIngredient[x] = true;
                bag.Remove(sock);
                sockCount--;

                if (totalIngredients == 0)
                {
                    baseNumber = x;
                    firstIngredient = "Sock Base + ";
                    spellName1= "Disgusting ";
                }
                else if (totalIngredients == 1)
                {
                    flavoring = flavoringValues[x];
                    secondIngredient = "Sock Flavoring +";
                    spellName2 = "Questionable ";
                }
                else if (totalIngredients == 2)
                {
                    garnish = x;
                    thirdIngredient = "Sock Garnish";
                    spellName3 = "Abombination?!";
                }
                totalIngredients++;
            } 
        }
        
        //Allows you to deselect ingredients
        else if (spellIngredient[x])
        {
            buttons[x].GetComponent<Image>().color = Color.clear;
            totalIngredients--;
            spellIngredient[x] = false;
            source.PlayOneShot(removeIngredientSound);
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
            source.PlayOneShot(magicCastSpell);
            
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


        //array of floats that store the values of the flavoring of each ingredient
        //for garnish, just pass in a number
        print("Flavoring: " + flavoring + "| Garnish:" + garnish);
        if (baseNumber == 0)
        {
            
            if (flavoring == flavoringValues[1] && garnish == 2)
            {
                eH.health -= ms.BreadSpell(flavoring, garnish) * 2;
                spellText.text = "PIZZAPOCALYPSE!";
            }
            else
            {
                eH.health -= ms.BreadSpell(flavoring, garnish);
                spellText.text = spellName1 + spellName2 + spellName3;
            }
            
        }
        if (baseNumber==1)
        {
            
            if (flavoring == flavoringValues[3] && garnish == 2)
            {
                p.health += ms.CheeseSpell(flavoring, garnish);
                spellText.text = "Meatball Parmageddon";
            }
            else
            {
                p.health += ms.CheeseSpell(flavoring, garnish) * 2;
                spellText.text = spellName1 + spellName2 + spellName3;
            }

        }

        
        if (baseNumber==2)
        {
            if (flavoring == flavoringValues[1] && garnish == 3)
            {
                p.health += ms.SauceSpell(flavoring, garnish)*1.5f;
                spellText.text = "Sauce Salvation";
            }
            //HarvestPlayer.hp.health += 10;
            else
            {
                p.health += ms.SauceSpell(flavoring, garnish);
                spellText.text = spellName1 + spellName2 + spellName3;
            }
        }

        if (baseNumber==3)
        {
            if (flavoring == flavoringValues[0] && garnish == 2)
            {
                eH.health -= ms.MeatSpell(flavoring, garnish);
                spellText.text = "Meatball Submission";
            }
            else
            {
                eH.health -= ms.MeatSpell(flavoring, garnish);
                spellText.text = spellName1 + spellName2 + spellName3;
            }
        }

        if (baseNumber==4)
        {
            if(flavoring!=flavoringValues[1] || garnish != 1){
                p.health -= ms.SockSpell(flavoring, garnish);
                eH.health += ms.SockSpell(flavoring, garnish);
                spellText.text = spellName1 + spellName2 + spellName3;
            }else{
                spellText.text = "Socked Cheese";
                p.health -= ms.SockSpell(flavoring, garnish);
                eH.health += ms.SockSpell(flavoring, garnish);
            }
            

        }
        //if (spellIngredient[0] && spellIngredient[3] && spellIngredient[4])
        //{
        //    print("PIZZA TIME");
        //    spellText.text = "Meatball Submission";
        //    eH.health -= 15;

        //}
        //if (spellIngredient[1] && spellIngredient[2] && spellIngredient[3])
        //{
        //    print("PIZZA TIME");
        //    spellText.text = "Chicken Parm Pulverizer";
        //    eH.health -= 20;

        //}
        //if (spellIngredient[1] && spellIngredient[2] && spellIngredient[4])
        //{
        //    print("PIZZA TIME");
        //    spellText.text = "Sock Soup w/ Cheese";
        //    eH.health -= 4;
        //}
        //if (spellIngredient[1] && spellIngredient[3] && spellIngredient[4])
        //{
        //    print("PIZZA TIME");
        //    spellText.text = "Food Abomination";
        //    eH.health -= 1;
        //}

        //if (spellIngredient[2] && spellIngredient[3] && spellIngredient[4])
        //{
        //    print("PIZZA TIME");
        //    spellText.text = "Spaghetti and Feetballs (Spaghetti not included)";
        //    eH.health -= 8;
        //}
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
