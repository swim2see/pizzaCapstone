using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class grossIngredient : MonoBehaviour {
    GameObject bag;
    Rigidbody2D rb;
    public float spd;
    public float distance;
    bool isDragging;

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
            GameObject bagObj;
            Vector3 bagPos;
            bagObj = GameObject.FindWithTag("bag");
            bagPos = bagObj.transform.position;
            Vector3 vel;
           
                vel = (bagPos-transform.position).normalized * spd / 2;
                rb.MovePosition(transform.position + vel);
            
            
           

            
        }
    }
    private void OnMouseDrag()
    {
        isDragging = true;
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
