using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class ScreenWrapper : MonoBehaviour
{
    private Rigidbody2D rigidbody;
    private Collider2D collider;

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        collider = GetComponent<Collider2D>();
    }

    void FixedUpdate()
    {
        var topLeft = Camera.main.ScreenToWorldPoint(Vector3.zero);
        var bottomRight = Camera.main.ScreenToWorldPoint(new Vector3(Camera.main.scaledPixelWidth, Camera.main.scaledPixelHeight, 0f));

        var position = transform.position;

        if (position.x - collider.bounds.extents.x > bottomRight.x && rigidbody.velocity.x > 0)
        {
            transform.position = new Vector2(topLeft.x, position.y);
        }

        if (position.x + collider.bounds.extents.x < topLeft.x && rigidbody.velocity.x < 0)
        {
            transform.position = new Vector2(bottomRight.x, position.y);
        }

        // Y is flipped in screen-space coordinates

        if (position.y - collider.bounds.extents.y > bottomRight.y && rigidbody.velocity.y > 0)
        {
            transform.position = new Vector2(position.x, topLeft.y);
        }

        if (position.y + collider.bounds.extents.y < topLeft.y && rigidbody.velocity.y < 0)
        {
            transform.position = new Vector2(position.x, bottomRight.y);
        }
    }
}
