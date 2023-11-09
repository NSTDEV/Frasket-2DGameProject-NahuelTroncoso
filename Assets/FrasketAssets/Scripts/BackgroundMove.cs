using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BackgroundMove : MonoBehaviour
{
    public RawImage rawIMGBG;
    public float _x;
    public float _y;
    public bool isPaused = false;

    void Awake()
    {
        rawIMGBG.enabled = true;
        if (rawIMGBG == null)
        {
            Debug.LogError("Background not found.");
        }
    }

    void Update()
    {
        if (isPaused)
        {
            rawIMGBG.uvRect = new Rect(rawIMGBG.uvRect.position + new Vector2(_x, _y) * Time.unscaledDeltaTime, rawIMGBG.uvRect.size);
        }
        rawIMGBG.uvRect = new Rect(rawIMGBG.uvRect.position + new Vector2(_x, _y) * Time.deltaTime, rawIMGBG.uvRect.size);
    }
}
