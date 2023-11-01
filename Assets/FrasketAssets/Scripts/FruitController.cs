using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FruitController : MonoBehaviour
{
    private SpriteRenderer spriteRenderer = null;
    private bool isGrabbed = false;
    private bool hasTouchedGround = false;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            gameObject.transform.GetChild(0).gameObject.SetActive(true);

            if (spriteRenderer != null)
            {
                spriteRenderer.enabled = true;
                hasTouchedGround = false;
            }

            isGrabbed = true;
        }
        else if (collision.CompareTag("Ground"))
        {
            Debug.Log("OnTriggerEnter2D called");
            if (isGrabbed)
            {
                hasTouchedGround = false;
            }
            else
            {
                hasTouchedGround = true;
                StartCoroutine(BlinkFruit());
            }
        }
    }

    private IEnumerator BlinkFruit()
    {
        float timeToBlink = 1.2f;
        float blinkInterval = 0.2f;
        float timer = Time.time + timeToBlink;

        while (Time.time < timer)
        {
            spriteRenderer.enabled = !spriteRenderer.enabled;
            yield return new WaitForSeconds(blinkInterval);
        }

        if (!isGrabbed && hasTouchedGround)
        {
            GetComponent<SpriteRenderer>().enabled = false;
            gameObject.transform.GetChild(0).gameObject.SetActive(true);
            Destroy(gameObject, 0.4f);
        }
    }
}