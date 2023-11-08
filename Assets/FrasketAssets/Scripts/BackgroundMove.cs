using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BackgroundMove : MonoBehaviour
{
    public RawImage rawIMG;
    public float _x;
    public float _y;
    public bool isPaused = false;

    void Update()
    {
        if (isPaused)
        {
            rawIMG.uvRect = new Rect(rawIMG.uvRect.position + new Vector2(_x, _y) * Time.unscaledDeltaTime, rawIMG.uvRect.size);
        }

        rawIMG.uvRect = new Rect(rawIMG.uvRect.position + new Vector2(_x, _y) * Time.deltaTime, rawIMG.uvRect.size);
    }
}
