using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class grossIngredient : MonoBehaviour {
    GameObject bag;
    Rigidbody2D rb;
    public float spd;
    public float distance;
    bool isDragging;
    Vector2 curPos;

    public ingredientClass thisIngredient;

    // Use this for initialization
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        //cam = GameObject.Find("Main Camera").GetComponent<CamControl>();
        bag = GameObject.FindWithTag("bag");
     
    }

    // Update is called once per frame

    void FixedUpdate()
    {
        if (bag != null)
        {
            //COMMENTED THIS OUT TO FOCUS ON GETTING SOCK THROWING
            
            //GameObject bagObj;
            //Vector3 bagPos;
            //bagObj = GameObject.FindWithTag("bag");
            //bagPos = bagObj.transform.position;
            //Vector3 vel;
            //if (isDragging == false)
            //{
            //    vel = (bagPos - transform.position).normalized * spd / 2;
            //    rb.MovePosition(transform.position + vel);
            //}
            
            float throwTimer = .1f;//timer that updates every tenth of a second to see how far you are from curPos 
            if (isDragging == true)//called when mouse is up
            {
                throwTimer -= Time.deltaTime;
                if (throwTimer == 0)
                {
                    curPos = transform.position;//set curPos to current location
                }
            }
            else
            {
                Vector2 throwSpeed = ((Vector2)transform.position - curPos).normalized * .10f;//compare curPos from before to transform position to set velocity
                rb.MovePosition((Vector2)transform.position + throwSpeed);
            }
        }
    }
    private void OnMouseDrag()
    {
        isDragging = true;
        //script below allows object to be picked up and moved
        Vector3 mousePos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, distance);
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

            HarvestManager.hm.bag.Add(HarvestManager.hm.sock);
            HarvestManager.hm.bag.Add(HarvestManager.hm.sock);
            HarvestManager.hm.sockCount += 2;
                Destroy(gameObject);
            
        }
    }
}
