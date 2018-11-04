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
    // Use this for initialization
    void Start () {
        DOTween.SetTweensCapacity(2000, 100);
        GetNewTargetAndMove();
    }

    // Update is called once per frame
    void Update () {
        // Grab a free Sequence to use
        

    }

    public void GetNewTargetAndMove()
    {
        if (!isDragging)
        {
            Vector2 newTarget = new Vector2(Random.Range(-4, 4), Random.Range(-4, 4));
            transform.DOMove(newTarget, 1.0f).OnComplete(GetNewTargetAndMove);
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
        GetNewTargetAndMove();
    }

    private void OnCollisionEnter(Collision collision)
    {
        
    }
}
