using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BackgroundMove : MonoBehaviour
{
    public RawImage img;
    public float _x;
    public float _y;
    public bool isPaused = false;

    void Update()
    {
        if (isPaused)
        {
            img.uvRect = new Rect(img.uvRect.position + new Vector2(_x, _y) * Time.unscaledDeltaTime, img.uvRect.size);
        }

        img.uvRect = new Rect(img.uvRect.position + new Vector2(_x, _y) * Time.deltaTime, img.uvRect.size);
    }
}
