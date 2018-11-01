using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

[RequireComponent(typeof(BoxCollider2D))]
public class sampleEnemyRunSpot : MonoBehaviour {
    public Transform[] positions;

    private Vector3 screenPoint;
    private Vector3 offset;
    public bool dragging;
    // Use this for initialization
    void Start () {
        DOTween.SetTweensCapacity(2000, 100);
        //GetNewTargetAndMove();
    }

    // Update is called once per frame
    void Update () {
        // Grab a free Sequence to use
        

    }

    //public void GetNewTargetAndMove()
    //{
    //    if (!dragging) { 
    //    Vector2 newTarget = new Vector2(Random.Range(-4, 4), Random.Range(-4, 4));
    //    transform.DOMove(newTarget, 1.0f).OnComplete(GetNewTargetAndMove);
    //}
    //}

    public void OnMouseDown()
    {
        screenPoint = Camera.main.WorldToScreenPoint(gameObject.transform.position);

        offset = gameObject.transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z));

        dragging = true;

    }
     public void OnMouseUp()
    {
        dragging = false;
    }
   public void OnMouseDrag()
    {
        Vector3 curScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z);

        Vector3 curPosition = Camera.main.ScreenToWorldPoint(curScreenPoint) + offset;

    }
}
