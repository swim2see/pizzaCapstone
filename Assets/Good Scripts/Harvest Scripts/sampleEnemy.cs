using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sampleEnemy : MonoBehaviour
{
    GameObject player;
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
        player = GameObject.FindWithTag("Player");
        transform.position = new Vector2(Random.Range(-6, 6), Random.Range(-3, 3));
        //print(transform.position);

       
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
            Vector3 vel;
            if (Vector2.Distance(transform.position, playerPos) > 5f)
            {
                vel = (transform.position-playerPos).normalized * spd / 2;
            }
            else
            {
                vel = (transform.position - playerPos).normalized * spd * 1.5f;
            }

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
            if (isDragging)
            {
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
