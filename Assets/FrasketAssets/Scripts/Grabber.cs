using UnityEngine;

public class Grabber : MonoBehaviour
{
    private GameObject heldFruit;
    private DragController dragController;
    private float throwForce;

    private void Start()
    {
        dragController = GetComponent<DragController>();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            if (heldFruit == null)
            {
                TryGrabFruit();
            }
            else
            {
                ThrowFruit();
            }
        }
    }

    private void TryGrabFruit()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(mousePosition, 0.1f);

        foreach (var hitCollider in hitColliders)
        {
            if (hitCollider.CompareTag("Fruit"))
            {
                heldFruit = hitCollider.gameObject;
                heldFruit.transform.SetParent(transform);
                heldFruit.transform.localPosition = new Vector3(0f, 0.17f, -5f);
                heldFruit.GetComponent<Rigidbody2D>().simulated = false;
            }
        }
    }

    private void ThrowFruit()
    {
        heldFruit.transform.SetParent(null);
        Rigidbody2D fruitRigidbody = heldFruit.GetComponent<Rigidbody2D>();
        fruitRigidbody.simulated = true;

        Vector3 throwDirection = -(dragController.dragEndPos - dragController.dragStartPos).normalized;
        fruitRigidbody.velocity = throwDirection * dragController.throwForce;

        heldFruit = null;
    }
}