using UnityEngine;

public class Asteroid : MonoBehaviour
{
    [SerializeField]
    private GameObject asteroidPrefab;

    [SerializeField]
    private float splitSpeed = 4f;

    private int splitDepth = 0;

    public void Split()
    {
        var newAsteroidOne = Instantiate(asteroidPrefab);
        var newAsteroidTwo = Instantiate(asteroidPrefab);

        newAsteroidOne.transform.position = transform.position;
        newAsteroidTwo.transform.position = transform.position;

        newAsteroidOne.GetComponent<Asteroid>().SetSplitDepth(splitDepth + 1);
        newAsteroidTwo.GetComponent<Asteroid>().SetSplitDepth(splitDepth + 1);

        newAsteroidOne.GetComponent<Rigidbody2D>().velocity = Random.insideUnitCircle * splitSpeed;
        newAsteroidTwo.GetComponent<Rigidbody2D>().velocity = Random.insideUnitCircle * splitSpeed;

        Destroy(gameObject);
    }

    public void SetSplitDepth(int splitDepth)
    {
        this.splitDepth = splitDepth;

        if (splitDepth == 0)
        {
            // big size
            transform.localScale = Vector3.one;
        }
        else if (splitDepth == 1)
        {
            // medium
            transform.localScale = Vector3.one * 0.66f;
        }
        else
        {
            // small
            transform.localScale = Vector3.one * 0.33f;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Bullets"))
        {
            Debug.Log("Hit a bullet");
            Destroy(collision.gameObject);

            if (splitDepth < 2)
            {
                Split();
            }
            else
            {
                Destroy(gameObject);
            }
        }
    }
}
