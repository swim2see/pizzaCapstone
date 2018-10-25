using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using DG.Tweening;
public class GameManager : MonoBehaviour
{

    //Singleton
    public static GameManager gm;
    public EventSystem es;

    [Header("Text")]
    public Text combatText;
    public Text descriptorText;
    public Text delayText;

    [Header("Minigames")]
    public Minigames mg;
    public Text timerText;
    public Text scoreText;
    public float timer;

    public Text minigameText; //Used to show numerical success on a given minigame

    //Determines which minigame is currently being played
    public int whichGame;
    public bool gameActive;
    public int gameState;       //0 - Select Attack, 1 - Select Target, 2 - Minigame, 3 - Enemy Turn


    [Header("Player Variables")]
    public Player p;
    private bool selectingAttack;


    [Header("The Artist formerly known as AI Manager")]
    public Enemy[] enemyList;//generates an array based on the enemy superclass
    public int playerSelect;//an int attached to the number of enemies that there are
    bool canISelectThings;
    public GameObject enemyPrefab;//what we are instantiating
    public GameObject hideUI;
    public Image enemyHealthBar;
    public bool movedStick;

    [Header("Ultimate Int Checks")]
    public bool[] comboAttack;
    public bool comboStateReached;

    [Header("Buttons")]
    public Button[] buttons;
    public GameObject allButtons;

    [Header("Pause/Game Feel")]
    public float pTimer;
    public float turnDelay;
    public float enemyDelay;
    public bool enemyTurn;
    public int index;

    [Header("Sounds")]
    public soundScript gameSoundsManager;
    public AudioClip mainsong;
    public AudioClip select;

    // Use this for initialization
    private void Awake()
    {
        gameSoundsManager.Play(mainsong,.75f);
    }
    void Start()
    {
        gm = this;
        timer = 3.5f;
        generateEnemies(2, 4);
        es = GameObject.Find("EventSystem").GetComponent<EventSystem>();

        
    }

    // Update is called once per frame
    void Update()
    {
        //Player selects their target
        if (gameState == 0)
        {
            
            for(int i=0; i<enemyList.Length; i++)
            {
                if (enemyList[i].dead)
                {

                    continue;
                }
                enemyList[i].resetStatusEffect();
            }
            for (var i = 0; i < buttons.Length; i++)
            {
                buttons[i].interactable = false;
            }

            
            combatText.text = "Select a target!";
            
            mg.score = 0;
            for (int i = 0; i < enemyList.Length; i++)
            {

                if (i == playerSelect)
                {
                    if (enemyList[i] != null)
                    {
                        enemyList[i].glow();
                    }
                }
                else
                {
                    if (enemyList[i] != null)
                    {
                        enemyList[i].unglow();
                    }
                }
                
            }
            selectTarget();
        }

        //Player selects their attack
        else if (gameState == 1)
        {
            combatText.text = "Choose your spell!";
            //hideUI.SetActive(true);
            for (var i = 0; i < buttons.Length; i++)
            {
                buttons[i].interactable = true;
            }
            //Assigns a button to the event system, if it doesn't have one
            if (es.currentSelectedGameObject != buttons[0].gameObject && !selectingAttack)
            {
                es.SetSelectedGameObject(buttons[0].gameObject);
                selectingAttack = true;
            }
            //print(es.currentSelectedGameObject);
            if (es.currentSelectedGameObject == buttons[0].gameObject)
            {
                descriptorText.text = "Tenderize raw meat before throwing it at your foe." + "\n" + "Mash the shoulder buttons to increase the strength of this attack";
            }
            if (es.currentSelectedGameObject == buttons[1].gameObject)
            {
                descriptorText.text = "Douse your enemy in your family's secret pasta sauce recipe, healing yourself in the process." + "\n" + "Spin the analog stick clockwise to increase sauce potency.";
            }
            if (es.currentSelectedGameObject == buttons[2].gameObject)
            {
                descriptorText.text = "Paralyze an enemy with the power of seasoning, halving their attack." + "\n" + "Press X to the beat to apply more oregano."; 
            }

            //If a button is clicked 
            if (Input.GetButtonDown("Submit"))
            {
                print("select)");
                
                //hideUI.SetActive(false);
                for (var i = 0; i < buttons.Length; i++)
                {
                    buttons[i].interactable = false;
                }
                //gameState = 3;
                descriptorText.text = "";
                
            }
        }

        else if (gameState == 6)
        {
            delayText.text = pTimer.ToString("f0");
            pTimer -= Time.deltaTime;
            if (pTimer <= 0)
            {
                pTimer = turnDelay;
                gameState = 2;
                scoreText.GetComponent<RectTransform>().DOAnchorPos(new Vector2(-355, 162), .5f);
                allButtons.GetComponent<RectTransform>().DOAnchorPos(new Vector2(0, 29), .5f);
                timerText.GetComponent<RectTransform>().DOAnchorPos(new Vector2(374, 197), .5f);
            }
        }

        //Activates the minigame
        else if (gameState == 2)
        {
            
            selectingAttack = false;
            //If a minigame is being played....
            if (gameActive)
            {
                scoreText.text = "Damage: " + mg.score;
                //hideUI.SetActive(false);
                for (var i = 0; i < buttons.Length; i++)
                {
                    buttons[i].interactable = false;
                }
                timer -= Time.deltaTime;

                //...Display the timer
                if (timer > 0)
                {
                    timerText.text = timer.ToString("f0");
                }

                //If the timer goes below zero, won't display negatives
                else
                {
                    timerText.text = "0.0";
                }

                //Minigames are played here, as long as the timer is going
                if (timer > 0)
                {
                    //Tenderizer minigam
                    if (whichGame == 1)
                    {
                        mg.Tenderizer();
                        comboAttack[0] = true;
                        combatText.text = "Mash the shoulder buttons!";
                    }

                    //Sause Toss minigame
                    else if (whichGame == 2)
                    {
                        mg.SauceToss();
                        comboAttack[1] = true;
                        combatText.text = "Spin the analog stick!";
                    }

                    //Orgeno Stun minigame
                    else if (whichGame == 3)
                    {
                        mg.OregenoStun();
                        comboAttack[2] = true;
                        combatText.text = "Press X on time!";
                    }

                    //Emergency overflow catch
                    else
                    {
                        Debug.Log("Minigame Selection Error");
                    }
                }
                //When the timer runs out...
                else
                {
                    comboStateReached = true;

                    for (int i = 0; i < comboAttack.Length; i++)
                    {
                        if (comboAttack[i] == false)
                        {
                            comboStateReached = false;
                            break;
                        }
                    }

                    //If the ulimate attack is successful, executes it here:
                    if (comboStateReached)
                    {
                        combatText.text = "ULTIMATE MOVE! MEATBALLISTIC MISSILE!";
                        for (int j = 0; j < enemyList.Length; j++)
                        {
                            enemyList[j].takeDamage(30);
                        }
                        comboAttack[0] = false;
                        comboAttack[1] = false;
                        comboAttack[2] = false;
                    }
                    else
                    {
                        //Damages the enemy
                        if (whichGame != 2) { 
                        enemyList[playerSelect].takeDamage(mg.score, whichGame);
                    }
                        enemyList[playerSelect].setStatusEffect(whichGame);
                        if (whichGame == 2)
                        {
                            p.health += (mg.score / 2);
                        }
                       
                    }

                    //Disables the minigame
                    mg.oreganoMinigame.gameObject.SetActive(false);
                    gameActive = false;
                    timer = 3;
                    gameState = 5;
                }
            }
        }
        else if(gameState == 5)
        {
            pTimer -= Time.deltaTime;
            if (pTimer <= 0)
            {
                pTimer = turnDelay;
                gameState = 3;
                combatText.transform.DOPunchScale(new Vector3(1, 1, 0), .5f, 1, 0);
                scoreText.GetComponent<RectTransform>().DOAnchorPos(new Vector2(-600, 162), .5f);
                timerText.text = "";
                timerText.GetComponent<RectTransform>().DOAnchorPos(new Vector2(513, 197), .5f);
            }
        }
        //Enemy Attacks the player
        else if (gameState == 3)
        {
            combatText.text = "Enemy's Turn!";
            if (enemyList[index]!=null)
            {
                enemyList[index].attack();
            }
            //enemyList[index].callOut();
            gameState = 4;
            index++;

            //ensures that we'll never go over the enemy list
            if (index > enemyList.Length - 1)
            {
                enemyTurn = false;
            }
            else
            {
                enemyTurn = true;
                pTimer = enemyDelay;
            }

            print("IM ATTACKING");

            /*for (int i = 0; i < enemyList.Length; i++)
            {
                
                enemyList[i].attack();
                
            }*/


            //gameState = 4;

        }
        else if (gameState == 4)
        {
            pTimer -= Time.deltaTime;
            if (pTimer <= 0)
            {
                pTimer = turnDelay;
                if (!enemyTurn)
                {
                    gameState = 0;
                    index = 0;


                }
                else
                {
                    gameState = 3;
                }
            }
        }


    }

    //Recieves an integer from the button, then, activates that minigame
    public void activiateMinigame(int x)
    {
        gameSoundsManager.Play(select);
        whichGame = x;
        gameState = 6;
        gameActive = true;


        

    }


    //This function will generate a series of ranom enemies (according to params)
    //and set them to the EnemyList
    public void generateEnemies(int min, int max)
    {
        //Determines the Length of the List
        int num = (int)Random.Range(min, max);
        enemyList = new Enemy[num];

        //Spawns list.length number of enemies
        for (int i = 0; i < num; i++)
        {

            //Grabs relevant component of Chef
            Vector3 temp = new Vector3(i * 4f, 0f, 0f) + gameObject.transform.position;
            enemyList[i] = Instantiate(enemyPrefab, temp - new Vector3(3, 0, 0), Quaternion.identity).GetComponent<Enemy>();


            //Sets enemy instance to be child of this object
            enemyList[i].gameObject.transform.SetParent(gameObject.transform, true);
            
        }
    }

    //Selects a target using the control stick; if the player goes over the
    //number of enemies, wraps aound to the other side
    
    public void selectTarget()
    {
        
        if (playerSelect == 0)
        {
            if (enemyList[playerSelect] != null)
            {
                descriptorText.text = "Remy" + "\n" + "Level 7 Pizza Rat" + "\n" + "Health:" + enemyList[playerSelect].health.ToString();
            }
            
        }
        if (playerSelect == 1)
        {
            if (enemyList[playerSelect] != null)
            {
                descriptorText.text = "Reginald" + "\n" + "Level 6 Pizza Rat" + "\n" + "Health:" + enemyList[playerSelect].health.ToString();
            }
        }
        if (playerSelect == 2)
        {
            if (enemyList[playerSelect] != null)
            {
                descriptorText.text = "Raymond" + "\n" + "Level 5 Pizza Rat" + "\n" + "Health:" + enemyList[playerSelect].health.ToString();
            }
        }
        //hideUI.SetActive(false);
        if (enemyList[playerSelect].dead == true)
        {

            playerSelect++;
        }
        for (var i = 0; i < buttons.Length; i++)
        {
            buttons[i].interactable = false;
        }
        if (Input.GetAxisRaw("Horizontal") > 0 && !movedStick)
        {
            playerSelect += 1;
            movedStick = true;
        }
        else if (Input.GetAxisRaw("Horizontal") < 0 && !movedStick)
        {
            playerSelect -= 1;
            movedStick = true;
        }
        else if ((Input.GetAxisRaw("Horizontal") == 0))
        {
            movedStick = false;
        }

        if (playerSelect >= enemyList.Length)
        {
            playerSelect = 0;
        }
        else if (playerSelect < 0)
        {
            playerSelect = enemyList.Length - 1;
        }

        //Goes from target selection to the minigame
        if (Input.GetButtonDown("Submit"))
        {
            gameState = 1;
            combatText.transform.DOPunchScale(new Vector3(1, 1, 0), .5f, 1, 0);
            print("hey");
            allButtons.GetComponent<RectTransform>().DOAnchorPos(new Vector2(0, 227), .5f);
        }
    }
}