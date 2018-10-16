using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class gameManager : MonoBehaviour {

    public static gameManager gm;
    public enemyAIMaster ai;

    [Header ("Text")]
    public Text combatText;

    [Header ("Minigames")]
    public LemonSqueezeMinigame squeezeScript;
    public potSpinning stirSauce;
    public OreganoCircle oCircles;
    public OreganoStun oBar;
    public GameObject oreganoHolder;


    public Text rotationCountText;
    public Text timerText;

    public Text squeezeCountText;
    public Text spinCountText;

    [Header("UI Buttons")]
    public Button button1;
    public Button button2;
    public Button button3;

    public bool tenderizerGame;
    public bool stirringGame;
    public bool oreganoGame;
   
  
    int ultimateInt;
    bool ultimateCheck;

    int meatInt;
    int sauceInt;

    [Header("Player Variables")]
   
    public AudioSource ultimateMoveSound;
    public Image playerHealthBar;
    public player p;
    
    // Use this for initialization
    void Start () {
        gm = this;
      
	}
	
	// Update is called once per frame
	void Update () {
	    
        playerHealthBar.fillAmount = p.health / p.maxHealth;//what affects the player health bar
        

        squeezeCountText.text = "Meat Spell Strength:" + squeezeScript.squeezeCount.ToString();
        spinCountText.text = "Sauce Spell Strength:" + stirSauce.fullRotation.ToString();

        if (squeezeScript.gameState == 1 && stirSauce.gameState==0)
        {
            timerText.text = squeezeScript.timer.ToString();
        }
        if (stirSauce.gameState == 1 && squeezeScript.gameState==0)
        {
            timerText.text = stirSauce.timer.ToString();
        }
        if (squeezeScript.timer <= 0)
        {
            timerText.text = "0.0";
        }
        if (stirSauce.timer <= 0)
        {
            timerText.text = "0.0";
        }
        
	    
	    if (oBar.Bar.fillAmount >= .99f)
	    {
	        oBar.Bar.fillAmount = 0;
	        //we must cancel the invoke, because it will continue to invoke even after the object is deactivated 
	        oBar.CancelInvoke();
	        
	        OreganoCircle[] circles = oreganoHolder.GetComponentsInChildren<OreganoCircle>();
	        Debug.Log(circles.Length);

	        foreach (OreganoCircle c in circles) 
	        {
	            c.Reset();
	            Debug.Log(c.butDead);
	        }
	        //oCircles.Reset();
	        oreganoHolder.gameObject.SetActive(false);
	    }
    }

    public void LemonSqueeze()
    {
        //ai.combatState = enemyAIMaster.CombatState.minigame;
        tenderizerGame = true;
        combatText.text = "MASH BUTTONS TO POUND MEAT!";
       // squeezeScript.gameState = 1;
        
        if (meatInt != 1)
        {
            ultimateInt += 1;
        }
        meatInt += 1;
        //timer counts down from 5 seconds, then player can select next spell
    }
    public void OreganoButtonPress()
    {
        ai.combatState = enemyAIMaster.CombatState.minigame;
        oreganoHolder.gameObject.SetActive(true);
    }
    
    public void PotSpin()
    {
        stirringGame = true;
        ai.combatState = enemyAIMaster.CombatState.minigame;
        combatText.text = "STIR THAT SAUCE";
        stirSauce.gameState = 1;
        
        if (sauceInt != 1)
        {
            ultimateInt += 1;
        }
        sauceInt += 1;
    }

    //This reatives the menu ui after a minigame
    
}

