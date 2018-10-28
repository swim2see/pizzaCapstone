using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
  
public class barScriptTest : MonoBehaviour
{
    public float[] maxFill;
    public float[] currentFill;
    public Image[] theBar;
    public bool[] isComplete;

    public bool[] buttonClicked;

    public Button sendButton;
    public Text spelltext;
    // Use this for initialization
    void Start()
    {
        sendButton.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < theBar.Length; i++)
        {
            theBar[i].fillAmount = currentFill[i] / maxFill[i];
            currentFill[i] += Time.deltaTime;

            if (currentFill[i] / maxFill[i] >= 1)
            {
                currentFill[i] = maxFill[i];
                isComplete[i] = true;
                print("fill complete");
            }
        }
        if (isComplete[0] && isComplete[1] && isComplete[2])
        {
            sendButton.gameObject.SetActive(true);
        }


        
    }

    public void MeatballButton()
    {
        spelltext.text="Meatball Slam!";
        
    }
    public void SpaghettiButton()
    {
        buttonClicked[1] = true;
    }
    public void SauceButton()
    {
        buttonClicked[2] = true;
    }
}
