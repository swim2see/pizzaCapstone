using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class grossIngredient : MonoBehaviour {
    GameObject bag;
    Rigidbody2D rb;
    public float spd;
    public float distance;
    bool isDragging;
    public Vector2 curPos;
    public float throwTimer;
    Vector3 mousePos;
    Vector2 initialPos;
    bool resetPosition;
    public ingredientClass thisIngredient;
    
    private Animator sockAnimator;

    // Use this for initialization
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        //cam = GameObject.Find("Main Camera").GetComponent<CamControl>();
        bag = GameObject.FindWithTag("bag");
        throwTimer = 0;
        initialPos = transform.position;
        
        sockAnimator = GetComponent<Animator>();
    }

    // Update is called once per frame

    void FixedUpdate()
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
            if (throwTimer<=0)
            {
                vel = (bagPos - transform.position).normalized * spd / 2;
                rb.MovePosition(transform.position + vel);
            }

            //timer that updates every tenth of a second to see how far you are from curPos 
            if (Input.GetMouseButtonUp(0))//called when mouse is down
            {
  
                Vector2 throwSpeed = ((Vector2)mousePos-curPos);
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
    private void OnMouseDrag()
    {
        if (throwTimer > 0) return;
        isDragging = true;
        //script below allows object to be picked up and moved
        mousePos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, distance);
        Vector3 objectPos = Camera.main.ScreenToWorldPoint(mousePos);

        transform.position = objectPos;
        

    }
    private void OnMouseUp()
    {
        isDragging = false;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "bag")
        {
            HarvestManager.hm.source.PlayOneShot(HarvestManager.hm.ingAddedSound);
            HarvestManager.hm.bag.Add(HarvestManager.hm.sock);
            HarvestManager.hm.bag.Add(HarvestManager.hm.sock);
            HarvestManager.hm.BagAddition();
            HarvestManager.hm.sockCount += 2;
                Destroy(gameObject);
            
        }
        if (collision.gameObject.tag == "The Boss")
        {
            HarvestManager.hm.bossTurnActions.health -= 10;
            Destroy(gameObject);
        }
        
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
       /* if (col.collider.tag == "Wall")
        {
            //cancel this collision
            if (throwTimer >= 0)
                Physics2D.IgnoreCollision(col.collider, col.otherCollider, true);
            else
                Physics2D.IgnoreCollision(col.collider, col.otherCollider, false);
        }
        if(col.collider.tag=="The Boss")
        {
            HarvestManager.hm.bossTurnActions.health -= 10f;
        }*/

    }
}
