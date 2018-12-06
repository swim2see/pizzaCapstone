using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class EnemyBehavior : MonoBehaviour {

    GameObject player;
    Rigidbody2D rb;
    public float spd;
    public float distance;
    bool isDragging;
    private bool isScared;

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
        //rb = GetComponent<Rigidbody2D>();
        newTarget = new Vector2(0, 0);
        vel = (newTarget - (Vector2)transform.position).normalized * speed / 2;
        prevPos = (Vector2)transform.position;
        
        centerPosition = transform.position;

        sauceAnimator = GetComponent<Animator>();
        cheeseAnimator = GetComponent<Animator>();
        breadAnimator = GetComponent<Animator>();

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
            

            if(this.gameObject.tag == "Meat")
            {
                if (Mathf.Abs((Vector2.Distance((Vector2) transform.position, newTarget))) < targetDistance)
                {
                    prevPos = newTarget;
                    newTarget = new Vector2(Random.Range(-11, 5), Random.Range(-3, 6));
                    
                    //sauceAnimator.Play("Sauce idle animation");
                }
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
                }
                else
                {
                    cheeseAnimator.Play("Cheese Walk");
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
        }
        //scaredAnimations();
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

                HarvestManager.hm.BagAddition();
                
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
