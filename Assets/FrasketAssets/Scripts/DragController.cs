using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragController : MonoBehaviour
{
    public LineRenderer line;
    public float dragLimit = 1f;
    private bool isDraggin;
    public Vector3 dragStartPos;
    public Vector3 dragEndPos;
    public float throwForce;

    Vector3 MousePosition
    {
        get
        {
            Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            pos.z = 0f;
            return pos;
        }
    }

    private void Start()
    {
        line.positionCount = 2;
        line.SetPosition(0, Vector2.zero);
        line.SetPosition(1, Vector2.zero);
        line.enabled = false;
    }

    public void Update()
    {
        if (Input.GetMouseButtonDown(0) && !isDraggin)
        {
            DragStart();
        }

        if (isDraggin)
        {
            Drag();
        }

        if (Input.GetMouseButtonUp(0) && isDraggin)
        {
            DragEnd();
        }
    }

    public Vector3 GetDragStartPos()
    {
        return dragStartPos;
    }

    void Drag()
    {
        Vector3 startPos = line.GetPosition(0);
        Vector3 currentPos = MousePosition;

        Vector3 distance = currentPos - startPos;

        if (distance.magnitude <= dragLimit)
        {
            line.SetPosition(1, currentPos);
        }
        else
        {
            Vector3 limitVector = startPos + (distance.normalized * dragLimit);
            line.SetPosition(1, limitVector);
        }
    }

    void DragStart()
    {
        line.enabled = true;
        isDraggin = true;
        line.SetPosition(0, MousePosition);
    }

    public void StartDragging()
    {
        dragStartPos = MousePosition;
        line.SetPosition(0, dragStartPos);
        line.enabled = true;
        isDraggin = true;
    }

    private void DragEnd()
    {
        isDraggin = false;
        line.enabled = false;

        Vector3 distance = Vector3.zero;
        for (int i = 1; i < line.positionCount; i++)
        {
            distance += line.GetPosition(i) - line.GetPosition(i - 1);
        }

        if (distance.magnitude > dragLimit)
        {
            distance = distance.normalized * dragLimit;
        }

        throwForce = distance.magnitude;
    }
}