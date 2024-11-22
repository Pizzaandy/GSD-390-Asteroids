using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMover : MonoBehaviour
{
    [SerializeField]
    private float thrust = 25;

    [SerializeField]
    private float turnSpeed = 10;

    [SerializeField]
    private float bulletSpeed = 10;

    [SerializeField]
    private GameObject bulletPrefab;

    private Rigidbody2D rigidbody;

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            var bulletInstance = Instantiate(bulletPrefab);
            bulletInstance.transform.position = transform.position;

            var bulletRigidbody = bulletInstance.GetComponent<Rigidbody2D>();
            bulletRigidbody.velocity = transform.up * bulletSpeed * Time.fixedDeltaTime;
        }
    }

    void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.W))
        {
            rigidbody.AddForce(transform.up * thrust);
        }

        if (Input.GetKey(KeyCode.A))
        {
            // turn counter clockwise
            rigidbody.freezeRotation = false;
            rigidbody.SetRotation(rigidbody.rotation + turnSpeed * Time.fixedDeltaTime);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            // turn clockwise
            rigidbody.freezeRotation = false;
            rigidbody.SetRotation(rigidbody.rotation - turnSpeed * Time.fixedDeltaTime);
        }
        else
        {
            rigidbody.freezeRotation = true;
        }


    }
}
