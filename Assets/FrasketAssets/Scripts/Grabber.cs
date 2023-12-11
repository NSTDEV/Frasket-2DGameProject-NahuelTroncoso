using UnityEngine;

public class Grabber : MonoBehaviour
{
    private GameObject heldFruit;
    private Vector3 initialHeldPosition;
    private bool isHolding = false;
    public float multipliyer = 2f;

    private DragController dragController;

    private void Start()
    {
        dragController = GetComponent<DragController>();
        initialHeldPosition = new Vector3(0f, 0.28f, 0f);
    }

    private void Update()
    {
        MoveHeldFruit();

        if (isHolding && Input.GetMouseButtonUp(0))
        {
            ThrowFruit();
            isHolding = false;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        TryGrabFruit(collision.gameObject);
    }

    private void TryGrabFruit(GameObject fruit)
    {
        if (!isHolding && (fruit.CompareTag("Fruit") || fruit.CompareTag("PineaPowa")))
        {
            heldFruit = fruit;
            Rigidbody2D fruitRigidbody = heldFruit.GetComponent<Rigidbody2D>();
            fruitRigidbody.simulated = false;
            isHolding = true;
        }
    }

    private void MoveHeldFruit()
    {
        if (isHolding && heldFruit != null)
        {
            heldFruit.transform.position = transform.position + initialHeldPosition;
        }
        else
        {
            heldFruit = null;
            isHolding = false;
        }
    }

    private void ThrowFruit()
    {
        if (heldFruit != null)
        {
            heldFruit.SetActive(true);

            Rigidbody2D fruitRigidbody = heldFruit.GetComponent<Rigidbody2D>();
            fruitRigidbody.simulated = true;

            Vector3 throwDirection = -(dragController.dragEndPos - dragController.dragStartPos).normalized;
            float throwForce = dragController.throwForce * multipliyer;

            fruitRigidbody.velocity = throwDirection * throwForce;
        }
    }
}