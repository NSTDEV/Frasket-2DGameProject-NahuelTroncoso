using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FruitController : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    public FruitData fruitData;

    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        Invoke("StartBlink", 4f);
    }

    private void StartBlink()
    {
        StartCoroutine(BlinkFruit());
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("BasquetButton"))
        {
            GameManager.score += fruitData.fruitScore;
            Destroy(gameObject);
        }
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

        yield return new WaitForSeconds(0.5f);
        Destroy(gameObject);
    }
}