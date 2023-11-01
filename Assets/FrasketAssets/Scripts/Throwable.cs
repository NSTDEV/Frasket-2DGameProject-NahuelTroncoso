using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Throwable : MonoBehaviour
{
    Vector3 throwVector;
    Rigidbody2D rb;
    LineRenderer lr;

    void Awake()
    {
        rb = this.GetComponent<Rigidbody2D>();
        lr = this.GetComponent<LineRenderer>();
    }

    void OnMouseDown()
    {
        CalculateThrowVector();
        SetArrow();
    }

    void OnMouseDrag()
    {
        CalculateThrowVector();
        SetArrow();
    }

    void CalculateThrowVector()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 distance = mousePos - this.transform.position;
        throwVector = -distance.normalized * 100;
    }

    void SetArrow()
    {
        lr.positionCount = 2;
        lr.SetPosition(0, Vector3.zero);
        lr.SetPosition(1, throwVector.normalized / 2);
        lr.enabled = true;
    }

    void OnMouseUp()
    {
        RemoveArrow();
        Throw();
    }

    void RemoveArrow()
    {
        lr.enabled = false;
    }

    public void Throw()
    {
        rb.AddForce(throwVector);
    }
}
