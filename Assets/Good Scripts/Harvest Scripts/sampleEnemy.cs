using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sampleEnemy : MonoBehaviour
{
    GameObject player;
    Rigidbody2D rb;
    public float spd;
    // Use this for initialization
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        //cam = GameObject.Find("Main Camera").GetComponent<CamControl>();
        player = GameObject.FindWithTag("Player");
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
}
