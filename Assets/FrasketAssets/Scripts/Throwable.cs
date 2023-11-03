using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Throwable : MonoBehaviour
{
    Vector3 throwVector;
    Rigidbody2D rb2D;
    LineRenderer lr;

    bool isDragging = false; // Indica si se está arrastrando la fruta.

    void Awake()
    {
        rb2D = GetComponent<Rigidbody2D>();
        lr = GetComponent<LineRenderer>();
    }

    void Update()
    {
        if (isDragging)
        {
            CalculateThrowVector();
            SetArrow();
        }

        // Detecta un clic en cualquier lugar de la pantalla.
        if (Input.GetMouseButtonDown(0))
        {
            isDragging = true;
            CalculateThrowVector();
            SetArrow();
        }

        // Detecta la liberación del botón izquierdo del mouse.
        if (Input.GetMouseButtonUp(0) && isDragging)
        {
            RemoveArrow();
            Throw(throwVector);
            isDragging = false;
        }
    }

    void CalculateThrowVector()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 distance = mousePos - transform.position;
        throwVector = -distance.normalized * 100;
    }

    void SetArrow()
    {
        lr.positionCount = 2;
        lr.SetPosition(0, Vector3.zero);
        lr.SetPosition(1, throwVector.normalized / 2);
        lr.enabled = true;
    }

    void RemoveArrow()
    {
        lr.enabled = false;
    }

    public void Throw(Vector2 force)
    {
        rb2D.AddForce(force);
    }
}
