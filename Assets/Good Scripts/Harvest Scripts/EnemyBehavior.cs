﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class EnemyBehavior : MonoBehaviour {

    GameObject player;
    Rigidbody2D rb;
    public float spd;
    public float distance;
    bool isDragging;

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

    

    public ingredientClass thisIngredient;

    // Use this for initialization
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        //cam = GameObject.Find("Main Camera").GetComponent<CamControl>();
        player = GameObject.FindWithTag("Player");
        transform.position = new Vector2(Random.Range(-6, 6), Random.Range(-3, 3));
        //print(transform.position);
        
        _centre = transform.position;
        RotateSpeed = Random.Range(1.5f, 2.5f);

        DOTween.SetTweensCapacity(2000, 100);
        rb = GetComponent<Rigidbody2D>();
        newTarget = new Vector2(0, 0);
        vel = (newTarget - (Vector2)transform.position).normalized * speed / 2;
        prevPos = (Vector2)transform.position;
    }

    // Update is called once per frame

    void FixedUpdate()
    {
        if (player != null)
        {
            GameObject playerObj;
            Vector3 playerPos;
            playerObj = GameObject.FindWithTag("Player");
            playerPos = playerObj.transform.position;
            Vector3 velo;
            if (Vector2.Distance(transform.position, playerPos) > 5f)
            {
                velo = (transform.position-playerPos).normalized * spd / 2;
            }
            else
            {
                velo = (transform.position - playerPos).normalized * spd * 1.5f;
            }

            rb.MovePosition(transform.position + velo);
        }
        
        
        
        
        //Sauce
        if (!isDragging)
        {
            if (Mathf.Abs((Vector2.Distance((Vector2)transform.position, newTarget))) < targetDistance)
            {
                prevPos = newTarget;
                newTarget = new Vector2(Random.Range(-11, 5), Random.Range(-3, 6));

            }
        }
        //print(Mathf.Abs((Vector2.Distance(prevPos, newTarget))));

        vel = (newTarget - prevPos).normalized * speed / 2;
        rb.MovePosition((Vector2)transform.position + vel);
        //End sauce
    }
    
    
    private void OnMouseDrag()
    {
        
        isDragging = true;
        Vector3 mousePos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, distance);
        Vector3 objectPos = Camera.main.ScreenToWorldPoint(mousePos);

        transform.position = objectPos;

        

    }

    /*private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "bag")
        {
            
            if (isDragging)
            {
                
                Destroy(gameObject);
            }
        }
    }*/
    
    
    

	
    // Update is called once per frame
    void Update () {
        if (!isDragging)
        {
            _angle += RotateSpeed * Time.deltaTime;

            var offset = new Vector2(Mathf.Sin(_angle), Mathf.Cos(_angle)) * Radius;
            //transform.position = _centre + offset;
            rb.MovePosition(_centre+offset);
        }
    }
    


    private void OnMouseUp()
    {
        isDragging = false;
        _centre = transform.position;
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

                HarvestManager.hm.bag.Add(HarvestManager.hm.sock);
                HarvestManager.hm.sockCount++;
               

                HarvestManager.hm.bag.Add(HarvestManager.hm.sauce);
                HarvestManager.hm.sauceCount++;

                HarvestManager.hm.bag.Add(HarvestManager.hm.meat);
                HarvestManager.hm.meatCount++;
                
                HarvestManager.hm.bag.Add(HarvestManager.hm.bread);
                HarvestManager.hm.breadCount++;
                HarvestManager.hm.bag.Add(HarvestManager.hm.bread);
                HarvestManager.hm.breadCount++;
                HarvestManager.hm.bag.Add(HarvestManager.hm.cheese);
                HarvestManager.hm.cheeseCount++;
                HarvestManager.hm.bag.Add(HarvestManager.hm.cheese);
                HarvestManager.hm.cheeseCount++;
                HarvestManager.hm.BagAddition();
                
                Destroy(gameObject);
                
                
            }
        }
    }
    
    

    
    
    
}
