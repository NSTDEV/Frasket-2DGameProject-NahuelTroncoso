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
    public AudioSource gol;

    public Animator camAnimator;

    void Awake()
    {
        rawIMGBG.gameObject.SetActive(true);
        if (rawIMGBG == null)
        {
            Debug.LogError("Background not found.");
        }
    }

    void Update()
    {
        float deltaTime = isPaused ? Time.unscaledDeltaTime : Time.deltaTime;
        rawIMGBG.uvRect = new Rect(rawIMGBG.uvRect.position + new Vector2(_x, _y) * deltaTime, rawIMGBG.uvRect.size);

    }

    public void CamShakes()
    {
        gol.Play();
        camAnimator.SetTrigger("Shake");
    }
}
