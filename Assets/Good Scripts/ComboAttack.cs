using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComboAttack : MonoBehaviour

{
    public List<Minigames.microgames> minigameList;

    public int score = 0;

    public Minigames mg;

    private int curGame = 0;
    //every time this is called, it will advance to the next game, and then wait until the timer is up
    public void playSpell()
    {
        score += mg.score;
        //stops the function if the minigame list is satisfied 
        if (curGame >= minigameList.Count)
        {
            minigameList.Clear();
            curGame = 0;
            return;
        }

        Minigames.microgames game = minigameList[curGame];
   
            //Tenderizer minigame
            if (game == Minigames.microgames.Tenderizer)
            {
                mg.Tenderizer(this);
               // combatText.text = "Mash the shoulder buttons!";
            }

            //Sause Toss minigame
            else if (game == Minigames.microgames.SauceToss)
            {
                mg.SauceToss();
               // combatText.text = "Spin the analog stick!";
            }

            //Orgeno Stun minigame
            else if (game == Minigames.microgames.OreganoStun)
            {
                mg.OregenoStun();
                //combatText.text = "Press X on time!";
            }

            //Emergency overflow catch
            else
            {
                Debug.Log("Minigame Selection Error");
            }
    
        curGame++;
    }

    public void meatBallMissile()
    {
       minigameList.Add(Minigames.microgames.Tenderizer);
       //minigameList.Add(Minigames.microgames.OreganoStun);
       //minigameList.Add(Minigames.microgames.SauceToss);
        minigameList.Add(Minigames.microgames.Tenderizer);
       playSpell();
    }
}