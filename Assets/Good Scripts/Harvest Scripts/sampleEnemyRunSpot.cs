using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;


public class sampleEnemyRunSpot : MonoBehaviour {
    public Transform[] positions;

    private Vector3 screenPoint;
    private Vector3 offset;
    public float distance;
    public bool isDragging;
    Rigidbody2D rb;
    public float speed;
    Vector2 newTarget;
    Vector2 vel;
    Vector2 prevPos;

    public float targetDistance;
    // Use this for initialization
    void Start () {
        DOTween.SetTweensCapacity(2000, 100);
        rb = GetComponent<Rigidbody2D>();
        GetNewTargetAndMove();
        newTarget = new Vector2(0, 0);
        vel = (newTarget - (Vector2)transform.position).normalized * speed / 2;
        prevPos = (Vector2)transform.position;
    }

    // Update is called once per frame
    void Update () {
        // Grab a free Sequence to use
        if (Mathf.Abs((Vector2.Distance((Vector2)transform.position,newTarget)))<targetDistance)
        {
            prevPos = newTarget;
            newTarget = new Vector2(Random.Range(-4, 4), Random.Range(-4, 4)); 
            print(newTarget);
        }
        print(Mathf.Abs((Vector2.Distance(prevPos, newTarget))));

        vel = (newTarget - prevPos).normalized * speed / 2;
        rb.MovePosition((Vector2)transform.position + vel);

    }

    public void GetNewTargetAndMove()
    {
        
            
           
        
        
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
        //GetNewTargetAndMove();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "bag")
        {
            if (isDragging)
            {
                HarvestManager.hm.ingredientCountB++;
                Destroy(gameObject);
            }
        }
    }
}
