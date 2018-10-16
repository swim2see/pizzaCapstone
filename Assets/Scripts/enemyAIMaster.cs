using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class enemyAIMaster : MonoBehaviour
{

    int whoseTurn;//determines who is performing actions

    public enemy[] enemyList;//generates an array based on the enemy superclass
    public int playerSelect;//an int attached to the number of enemies that there are
    bool canISelectThings;
    public GameObject enemyPrefab;//what we are instantiating

    public LemonSqueezeMinigame tenderizerScript;
    public potSpinning stirSauce;
    public OreganoCircle oCircles;
    public OreganoStun oBar;
    public GameObject oreganoHolder;

    public GameObject hideUI;

    public Image enemyHealthBar;

    public bool minigameActive;
    public bool movedStick;

    public enum CombatState
    {
        enemySelect,
        spellSelect,
        minigame,
        enemyTurn
    }

    public CombatState combatState;
    // Use this for initialization
    void Start()
    {

        generateEnemies(1, 3);
        whoseTurn = 0;
        //enemyList[playerSelect].health -= 40; <this is how we affect an enemies health>

    }

    // Update is called once per frame
    void Update()
    {
        for(int i = 0; i < enemyList.Length; i++)
        {
            if(i == playerSelect)
            {
                enemyList[i].glow();
            }
            else
            {
                enemyList[i].unglow();
            }
        }
        switch (combatState)
        {
            case CombatState.enemySelect:
                selectTarget();
                print("enemySelect");
                break;
            case CombatState.spellSelect:
                SpellSelection();
                print("spellSelect");
                break;
            case CombatState.minigame:
                Minigames();
                print("minigame");
                Debug.Log(tenderizerScript.gameState);

                if (tenderizerScript.gameState == 0 && gameManager.gm.tenderizerGame == true)
                {
                   
                    if (tenderizerScript.squeezeCount > 20 && tenderizerScript.squeezeCount < 30)
                    {
                        enemyList[playerSelect].takeDamage(20);

                    }
                    else if (tenderizerScript.squeezeCount > 30)
                    {
                        enemyList[playerSelect].takeDamage(40);
                    }
                    else if (tenderizerScript.squeezeCount < 25 && tenderizerScript.squeezeCount > 0)
                    {
                        enemyList[playerSelect].takeDamage(10);
                    }
                    tenderizerScript.squeezeCount = 0;
                    gameManager.gm.tenderizerGame = false;
                    combatState = CombatState.enemyTurn;
                    minigameActive = false;
                }

                else if (stirSauce.gameState == 0 && gameManager.gm.stirringGame == true)
                {

                    if (stirSauce.fullRotation > 10 && stirSauce.fullRotation < 15)
                    {
                        enemyList[playerSelect].takeDamage(20);
                    }
                    else if (stirSauce.fullRotation > 15)
                    {
                        enemyList[playerSelect].takeDamage(20);
                    }
                    else if (stirSauce.fullRotation < 10 && stirSauce.fullRotation > 0)
                    {
                        enemyList[playerSelect].takeDamage(10);
                    }
                    stirSauce.fullRotation = 0;
                    gameManager.gm.stirringGame = false;
                    combatState = CombatState.enemyTurn;
                    minigameActive = false;
                }

                break;
            case CombatState.enemyTurn:
                print("enemyTurn");
                EnemyAttackPattern();
                combatState = CombatState.enemySelect;
                break;
        }



    }

    public void selectTarget()
    {
        hideUI.SetActive(false);
        if (Input.GetAxisRaw("Horizontal") > 0 && !movedStick)
        {
            playerSelect += 1;
            movedStick = true;
        }
        else if (Input.GetAxisRaw("Horizontal") < 0 && !movedStick)
        {
            playerSelect -= 1;
            movedStick = true;
        }else if ((Input.GetAxisRaw("Horizontal") == 0)){
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

        if (Input.GetButtonDown("Submit"))
        {
            combatState = CombatState.spellSelect;
            print(combatState);
        }
    }
    public void SpellSelection()
    {

    }

    public void Minigames()
    {
        hideUI.SetActive(false);

        if (!minigameActive)
        {
            if (gameManager.gm.tenderizerGame == true)
            {
                tenderizerScript.gameState = 1;
                minigameActive = true;
            }
            if (gameManager.gm.stirringGame == true)
            {
                stirSauce.gameState = 1;
                minigameActive = true;
            }       
        }
    }


    public void EnemyAttackPattern()
    {
        print("IM ATTACKING");
        for (int i = 0; i < enemyList.Length; i++)
        {
            if (Random.Range(0f, 2f) > 0.01f)
            {
                print("HERE COMES THE PAIN");
                enemyList[i].attack();
            }
            else
            {
                enemyList[i].defend();
            }

        }
    }


    //This function will generate a series of random enemies (according to params)
    //and set them to the EnemyList
    public void generateEnemies(int min, int max)
    {
        //Determines the Length of the List
        int num = (int)Random.Range(min, max);
        enemyList = new enemy[num];

        //Spawns list.length number of enemies
        for (int i = 0; i < num; i++)
        {
            //Grabs relevant component of Chef
            Vector3 temp = new Vector3(i * 10f, 0f, 0f) + gameObject.transform.position;
            enemyList[i] = Instantiate(enemyPrefab, temp, Quaternion.identity).GetComponent<enemyChef>();



            //Sets enemy instance to be child of this object
            enemyList[i].gameObject.transform.SetParent(gameObject.transform, true);
        }
    }
}
