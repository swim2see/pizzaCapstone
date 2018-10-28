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
    // Use this for initialization
    void Start()
    {
        
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
    }
}
