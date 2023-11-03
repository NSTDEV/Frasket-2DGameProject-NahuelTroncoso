using UnityEngine;

public class Grabber : MonoBehaviour
{
    private GameObject heldFruit;
    private Vector3 initialHeldPosition;
    private bool isHolding = false;
    private Vector3 dragStartPos;

    public DragController dragController;

    private void Update()
    {
        if (!isHolding)
        {
            Collider2D hitCollider = GetTouchingFruit();
            if (hitCollider != null)
            {
                TryGrabFruit(hitCollider.gameObject);
            }
        }
        else
        {
            MoveHeldFruit();
            if (Input.GetMouseButtonUp(0))
            {
                ThrowFruit();
            }
        }
    }

    private Collider2D GetTouchingFruit()
    {
        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(transform.position, 0.1f);

        foreach (Collider2D collider in hitColliders)
        {
            if (collider.CompareTag("Fruit"))
            {
                return collider;
            }
        }

        return null;
    }

    private void TryGrabFruit(GameObject fruit)
    {
        heldFruit = fruit;
        Rigidbody2D fruitRigidbody = heldFruit.GetComponent<Rigidbody2D>();
        fruitRigidbody.simulated = false;

        initialHeldPosition = new Vector3(0f, 0.18f, 0f);
        isHolding = true;
        dragStartPos = transform.position;
    }

    private void MoveHeldFruit()
    {
        if (heldFruit != null)
        {
            heldFruit.transform.position = transform.position + initialHeldPosition;
        }
    }

    private void ThrowFruit()
    {
        if (heldFruit != null)
        {
            Rigidbody2D fruitRigidbody = heldFruit.GetComponent<Rigidbody2D>();
            fruitRigidbody.simulated = true;

            Vector3 throwDirection = -(dragController.dragEndPos - dragController.dragStartPos).normalized;
            float throwForce = (dragController.dragEndPos - dragController.dragStartPos).magnitude * 3.0f;

            fruitRigidbody.velocity = throwDirection * throwForce;

            heldFruit = null;
            isHolding = false;
        }
    }
}
