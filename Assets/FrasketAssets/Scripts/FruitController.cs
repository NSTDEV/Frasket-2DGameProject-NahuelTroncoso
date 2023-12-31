using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class FruitController : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    public FruitData fruitData;
    public AudioSource touchGround, destroy;
    private bool hasBeenProcessed = false;

    private BackgroundMove bgm;

    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        bgm = FindObjectOfType<BackgroundMove>();
    }

    private void Start()
    {
        Invoke("StartBlink", 4f);
    }

    private void StartBlink()
    {
        StartCoroutine(BlinkFruit());
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            touchGround.Play();
        }
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("BasquetButton") && !hasBeenProcessed)
        {
            GameManager.score += fruitData.fruitScore;
            bgm.CamShakes();
            StartCoroutine(GolAndDestroy());
        }
    }

    private IEnumerator GolAndDestroy()
    {
        hasBeenProcessed = true;

        yield return new WaitForSeconds(0.5f);
        Destroy(gameObject);
    }

    private IEnumerator BlinkFruit()
    {
        float timeToBlink = 1f;
        float blinkInterval = 0.2f;
        float timer = Time.time + timeToBlink;

        while (Time.time < timer)
        {
            spriteRenderer.enabled = !spriteRenderer.enabled;
            yield return new WaitForSeconds(blinkInterval);
        }

        spriteRenderer.enabled = false;
        gameObject.transform.GetChild(0).gameObject.SetActive(true);
        destroy.Play();

        yield return new WaitForSeconds(0.5f);
        Destroy(gameObject);
    }
}