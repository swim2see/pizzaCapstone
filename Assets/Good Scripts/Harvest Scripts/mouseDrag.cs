using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mouseDrag : MonoBehaviour {
    public float distance;
    bool isDragging;

    public float RotateSpeed;
    public float Radius;

    private Vector2 _centre;
    private float _angle;
    // Use this for initialization
    void Start () {
        _centre = transform.position;
        RotateSpeed = Random.Range(1.5f, 2.5f);
    }
	
	// Update is called once per frame
	void Update () {
        if (!isDragging)
        {
            _angle += RotateSpeed * Time.deltaTime;

            var offset = new Vector2(Mathf.Sin(_angle), Mathf.Cos(_angle)) * Radius;
            transform.position = _centre + offset;
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
        
    }
}
