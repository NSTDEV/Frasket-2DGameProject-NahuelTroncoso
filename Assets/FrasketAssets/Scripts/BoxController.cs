using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxController : MonoBehaviour
{
    public BoxData boxData;
    private int remainingLifes;
    private SpriteRenderer spriteRenderer;
    private int hitCount = 0;

    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Start()
    {
        remainingLifes = boxData.boxLifes;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("PineaPowa"))
        {
            if (remainingLifes > 0)
            {
                hitCount++;
                remainingLifes--;

                Animator childAnimator = gameObject.transform.GetChild(0).GetComponent<Animator>();
                if (childAnimator != null)
                {
                    spriteRenderer.enabled = false;
                    gameObject.transform.GetChild(0).gameObject.SetActive(true);
                    StartCoroutine(StopAnimation(childAnimator, 0.5f));

                    if (hitCount >= boxData.boxLifes)
                    {
                        StartCoroutine(DestroyBox());
                    }
                }
            }
        }
    }

    IEnumerator StopAnimation(Animator animator, float duration)
    {
        yield return new WaitForSeconds(duration);
        gameObject.transform.GetChild(0).gameObject.SetActive(false);
        spriteRenderer.enabled = true;

        if (hitCount >= boxData.boxLifes)
        {
            StartCoroutine(DestroyBox());
        }
    }

    IEnumerator DestroyBox()
    {
        float timeToDestroy = 0.4f;
        float timer = Time.time + timeToDestroy;

        while (Time.time < timer)
        {
            gameObject.transform.GetChild(1).gameObject.SetActive(true);
            spriteRenderer.enabled = false;
            yield return new WaitForSeconds(timeToDestroy);
        }

        yield return new WaitForSeconds(timeToDestroy);

        OpenBox();
        Destroy(gameObject);
    }

    public void OpenBox()
    {
        foreach (var fruitInfo in boxData.availableFruits)
        {
            GameObject fruit = Instantiate(fruitInfo.fruitData.fruitPrefab, transform.position, Quaternion.identity);
        }
    }
}