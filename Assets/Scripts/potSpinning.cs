using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class potSpinning : MonoBehaviour
{
    Vector2 lastDirection;
    // ...
    public float spinsDetected;
    public int quadrant;
    bool[] currentQuandrant = new bool[4];

    public bool clockwise;
    int rotationI;
   public int fullRotation;

    public int gameState;
    public float timer;

   
    // Use this for initialization
    void Start()
    {
        timer = 3;
        gameState = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
        if (gameState == 1)
        {
            timer -= Time.deltaTime;
        }
        else if (gameState == 0)
        {
            timer = 3f;
            //squeezeCount = 0;

        }

        
        if (timer <= 0f)
        {
            timer = 0f;
            gameState = 0;
            
        }
        //gameText.text = (gameState.ToString()); 


       
        SpinSticks();

    }
    void SpinSticks()
    {
        if (gameState == 1)
        {
            if (rotationI >= 4)
            {
                rotationI = 0;
                fullRotation += 1;
            }
            //check to see if the magnitude of the vector is greater than .2 (if the stick returns to neutral, you fail)

            Vector2 inputDirection = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")); // GetAxisRaw for more precise reading of analog stick direction
            if (inputDirection.x > 0.2 && inputDirection.y > 0.2)
            {
                quadrant = 0;
                if (currentQuandrant[3] || quadrant == 4)
                {
                    rotationI += 1;
                }
            }
            else if (inputDirection.x > 0.2 && inputDirection.y < 0.2)
            {
                quadrant = 1;
                if (currentQuandrant[0] || quadrant == 4)
                {
                    rotationI += 1;
                }
            }
            else if (inputDirection.x < 0.2 && inputDirection.y < 0.2)
            {
                quadrant = 2;
                if (currentQuandrant[1] || quadrant == 4)
                {
                    rotationI += 1;
                }
            }
            else if (inputDirection.x < 0.2 && inputDirection.y > 0.2)
            {
                quadrant = 3;
                if (currentQuandrant[2] || quadrant == 4)
                {
                    rotationI += 1;
                }
            }

            for (int i = 0; i < currentQuandrant.Length; i++)
            {
                currentQuandrant[i] = false;
            }
            currentQuandrant[quadrant] = true;


            print(inputDirection);                                                                                      // Normalize input only if it exceeds the maximum for either axis alone.
            if (inputDirection.magnitude >= .02)
            {
                quadrant = 4;
            }
        }
    }
}
