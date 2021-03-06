﻿using System.Collections;
using System.Collections.Generic;
using System.Net.Mime;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class EnemyBehavior : MonoBehaviour {

    GameObject player;
    Rigidbody2D rb;
    public float spd;
    public float distance;
    public bool isDragging;
    private bool isScared;
    private bool isPicked;

    public float RotateSpeed;
    public float Radius;



    private Vector2 _centre;
    private float _angle;

    
    public Transform[] positions;

    private Vector3 screenPoint;
    private Vector3 offset;
    public float speed;
    Vector2 newTarget;
    Vector2 vel;
    Vector2 prevPos;
    
    public float targetDistance;

    private Vector2 centerPosition;
    
    private float distanceCounter;

    private Animator sauceAnimator;
    private Animator cheeseAnimator;
    private Animator breadAnimator;
    private Animator meatAnimator;
    
    public ingredientClass thisIngredient;

    [Header("Sock Stuff")]

    GameObject bag;
    //Rigidbody2D rb;
    //public float spd;
    //public float distance;
    //bool isDragging;
    public Vector2 curPos;
    public float throwTimer;
    Vector3 mousePos;
    Vector2 initialPos;
    bool resetPosition;
    //public ingredientClass thisIngredient;

    private Animator sockAnimator;
    // Use this for initialization
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        //cam = GameObject.Find("Main Camera").GetComponent<CamControl>();
        player = GameObject.FindWithTag("Player");
        transform.position = new Vector2(Random.Range(-6, 6), Random.Range(-3, 6));
        //print(transform.position);
        
        _centre = transform.position;
        RotateSpeed = Random.Range(1.5f, 2.5f);

        DOTween.SetTweensCapacity(2000, 100);
        //rb = GetComponent<Rigidbody2D>();
        newTarget = new Vector2(0, 0);
        vel = (newTarget - (Vector2)transform.position).normalized * speed / 2;
        prevPos = (Vector2)transform.position;
        
        centerPosition = transform.position;

        sauceAnimator = GetComponent<Animator>();
        cheeseAnimator = GetComponent<Animator>();
        breadAnimator = GetComponent<Animator>();
        meatAnimator = GetComponent<Animator>();
        sockAnimator = GetComponent<Animator>();

        //rb = GetComponent<Rigidbody2D>();
        //cam = GameObject.Find("Main Camera").GetComponent<CamControl>();
        bag = GameObject.FindWithTag("bag");
        throwTimer = 0;
        initialPos = transform.position;
    }

    // Update is called once per frame
    
    private void OnMouseDrag()
    {        
       
        isDragging = true;
        Vector3 mousePos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, distance);
        Vector3 objectPos = Camera.main.ScreenToWorldPoint(mousePos);
        
        centerPosition.x = objectPos.x;
        centerPosition.y = objectPos.y;

        transform.position = objectPos;      
        /*if(this.gameObject.tag == "Sauce")
        {         
                sauceAnimator.Play("Sauce grabbed");       
        }*/
  
    }


    void FixedUpdate()
    {

        
        vel = (newTarget - prevPos).normalized * speed / 2;
        rb.MovePosition((Vector2)transform.position + vel);
        
        Vector3 playerPos = player.transform.position;
        //End sauce
        
        //Determines if the cursor is close enough to play ingredient scared animations
        if (Vector2.Distance(transform.position, playerPos) < 3f)
        {
            isScared = true;
        }
        else
        {
            isScared = false;
        }
        
        if (!isDragging)
        {
            //foreach (GameObject sauce in HarvestManager.hm.sauceEnemyCount)
            //sauce movement
            if (this.gameObject.tag == "Sock")
            {
                if (bag != null)
                {

                    if (throwTimer > 0)
                    {
                        throwTimer -= Time.fixedDeltaTime;
                        if (throwTimer <= 0)
                        {
                            throwTimer = 0;
                        }
                    }
                    //COMMENTED THIS OUT TO FOCUS ON GETTING SOCK THROWING

                    GameObject bagObj;
                    Vector3 bagPos;
                    bagObj = GameObject.FindWithTag("bag");
                    bagPos = bagObj.transform.position;
                    Vector3 vel;
                    if (throwTimer <= 0)
                    {
                        vel = (bagPos - transform.position).normalized * spd / 2;
                        rb.MovePosition(transform.position + vel);
                    }

                    //timer that updates every tenth of a second to see how far you are from curPos 
                    if (Input.GetMouseButtonUp(0))//called when mouse is down
                    {

                        Vector2 throwSpeed = ((Vector2)mousePos - curPos);
                        print(throwSpeed);//compare curPos from before to transform position to set velocity
                        if (throwSpeed.magnitude > .01f)
                        {
                            //rb.MovePosition((Vector2)transform.position + throwSpeed);
                            rb.velocity = throwSpeed;
                            throwTimer = 1f;

                        }
                        else
                        {
                            //  transform.position = new Vector3(0, 0, 0);
                        }

                    }
                    curPos = mousePos;
                }

                if (isDragging)
                {
                    sockAnimator.Play("Sock grabbed");
                }
                else
                {
                    sockAnimator.Play("Sock hop");
                }

            }

            if(this.gameObject.tag == "Meat")
            {
                if (Mathf.Abs((Vector2.Distance((Vector2) transform.position, newTarget))) < targetDistance)
                {
                    prevPos = newTarget;
                    newTarget = new Vector2(Random.Range(-10, 10), Random.Range(-3, 5));
                    
                    //sauceAnimator.Play("Sauce idle animation");
                    
                }
                meatAnimator.Play("Meat walk");
            }
            
            //cheese movement
            if (this.gameObject.tag == "Cheese")
            {
                _angle += RotateSpeed * Time.deltaTime;

                var offset = new Vector2(Mathf.Sin(_angle), Mathf.Cos(_angle)) * Radius;
                //transform.position = _centre + offset;
                rb.MovePosition(_centre+offset);

                if (isScared == true)
                {
                    cheeseAnimator.Play("Mozzarella scared");
                    for(int i=0; i< this.gameObject.transform.childCount; i++)
                    {
                        GameObject child = this.gameObject.transform.GetChild(i).gameObject;
                        if(child != null)
                            child.SetActive(true);
                    }
                }
                else
                {
                    cheeseAnimator.Play("Cheese Walk");
                    for(int i=0; i< this.gameObject.transform.childCount; i++)
                    {
                        GameObject child = this.gameObject.transform.GetChild(i).gameObject;
                        if(child != null)
                            child.SetActive(false);
                    }
                }

            }
            
            //bread movement
            if(this.gameObject.tag == "Bread")
            {
                if (player != null)
                {
                    GameObject playerObj;
                    Vector3 velo;
                    if (Vector2.Distance(transform.position, playerPos) > 5f)
                    {
                        velo = (transform.position - playerPos).normalized * spd / 2;
                    }
                    else
                    {
                        velo = (transform.position - playerPos).normalized * spd * 1.5f;
                    }

                    rb.MovePosition(transform.position + velo);
                    breadAnimator.Play("Dough walk");
                }
            }
            
            if (this.gameObject.tag == "Sauce")
            {
                transform.position = new Vector2(Mathf.Sin(distanceCounter) * 2 + centerPosition.x, centerPosition.y);
                //+ centerPosition.x, centerPosition.y
                distanceCounter += spd;
                //transform.GetChild()
                if (distanceCounter > 2f * Mathf.PI)
                {
                    distanceCounter = 0;
                }
            }
            
        }
        else
        {        
               transform.position = centerPosition;

            sauceAnimator.Play("Sauce grabbed");
            cheeseAnimator.Play("Mozzarella grabbed");
            breadAnimator.Play("Dough grabbed");
            meatAnimator.Play("Meat grabbed");
        }
        //scaredAnimations();
    }

/*
    public IEnumerator IngCollected(float x)
    {

        var startTime = Time.realtimeSinceStartup;
        while (Time.realtimeSinceStartup < startTime + 2f)
        {
            var randomPoint = new Vector3(Random.Range(initialPos.x - 1, initialPos.x + 1), Random.Range(initialPos.y - 1, initialPos.y + 1), initialPos.z);
            target.localPosition = randomPoint;
            yield return null;
        }

        pendingShakeDuration = 0f;
        target.localPosition = initialPos; 
        isShaking = false;
    }
*/

    private void OnMouseUp()
    {
        isDragging = false;
        _centre = transform.position;
        sauceAnimator.Play("Sauce idle animation");
        //cheeseAnimator.Play("Cheese Walk");
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "bag")
        {
            if (isDragging)
            {
                HarvestManager.hm.source.PlayOneShot(HarvestManager.hm.ingAddedSound);
                //HarvestManager.hm.ingredientCountC++;
                
                
                HarvestManager.hm.BagAddition();
                if (gameObject.tag == "sauce")
                {
                    HarvestManager.hm.bag.Add(HarvestManager.hm.sauce);
                    HarvestManager.hm.sauceCount++;
                }

                if (gameObject.tag == "Sock")
                {
                    HarvestManager.hm.bag.Add(HarvestManager.hm.sock);
                    HarvestManager.hm.sockCount++;
                }

                if (gameObject.tag == "Meat")
                {
                    HarvestManager.hm.bag.Add(HarvestManager.hm.meat);
                    HarvestManager.hm.meatCount++;
                }

                if (gameObject.tag == "Bread")
                {
                    HarvestManager.hm.bag.Add(HarvestManager.hm.bread);
                    HarvestManager.hm.breadCount++;
                }

                if (gameObject.tag == "Cheese")
                {
                    HarvestManager.hm.bag.Add(HarvestManager.hm.cheese);
                    HarvestManager.hm.cheeseCount++;
                }

                //HarvestManager.hm.BagAddition();
                
                Destroy(gameObject);
                
                
            }
        }
    }

    private void scaredAnimations()
    {
        //Vector3 playerPos = player.transform.position;




        if (!isDragging)
        {
           
               /* if (this.gameObject.tag == "Meat")
                {
                    if (Mathf.Abs((Vector2.Distance((Vector2) transform.position, newTarget))) < targetDistance)
                    {
                        prevPos = newTarget;
                        newTarget = new Vector2(Random.Range(-11, 5), Random.Range(-3, 6));

                        //sauceAnimator.Play("Sauce idle animation");
                    }
                }*/

                //cheese movement
                if (this.gameObject.tag == "Cheese")
                {
                    

                    //bread movement
                /*if (this.gameObject.tag == "Bread")
                {
                    if (player != null)
                    {
                        GameObject playerObj;
                        Vector3 velo;
                        if (Vector2.Distance(transform.position, playerPos) > 5f)
                        {
                            velo = (transform.position - playerPos).normalized * spd / 2;
                        }
                        else
                        {
                            velo = (transform.position - playerPos).normalized * spd * 1.5f;
                        }

                        rb.MovePosition(transform.position + velo);
                    }
                }

                if (this.gameObject.tag == "Sauce")
                {
                    transform.position =
                        new Vector2(Mathf.Sin(distanceCounter) * 2 + centerPosition.x, centerPosition.y);
                    //+ centerPosition.x, centerPosition.y
                    distanceCounter += spd;
                    if (distanceCounter > 2f * Mathf.PI)
                    {
                        distanceCounter = 0;
                    }
                }*/

            }
            else
            {
                transform.position = centerPosition;

                //sauceAnimator.Play("Sauce grabbed");
                cheeseAnimator.Play("Mozzarella grabbed");
            }

        }
    }

}
