using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float destroyTime = 3f; // Auto destroy if no hit

    void Start()
    {
        Destroy(gameObject, destroyTime);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Asteroid")) // Check if it hits an asteroid
        {
            Destroy(other.gameObject); // Destroy asteroid
            Destroy(gameObject); // Destroy bullet
        }
    }
}


