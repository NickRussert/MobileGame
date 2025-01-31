using UnityEngine;

public class Asteroid : MonoBehaviour
{
    private HealthManager healthManager;

    void Start()
    {
        healthManager = FindObjectOfType<HealthManager>(); // Get reference to HealthManager
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Asteroid hit player!");
            healthManager.LoseLife(); // Lose a life
            Destroy(gameObject); // Destroy asteroid
        }
        else if (other.CompareTag("Bottom"))
        {
            Debug.Log("Asteroid reached bottom!");
            healthManager.LoseLife(); // Lose a life
            Destroy(gameObject); // Destroy asteroid
        }
    }
}

