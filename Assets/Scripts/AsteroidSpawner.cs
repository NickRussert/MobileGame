using UnityEngine;

public class AsteroidSpawner : MonoBehaviour
{
    public GameObject asteroidPrefab; // Assign in Inspector
    public float spawnRate = 1.5f; // Time between spawns
    public float minX = -2.5f, maxX = 2.5f; // X-range for spawning
    public float spawnY = 5f; // Spawn height
    public float asteroidSpeed = 3f; // How fast asteroids move down

    private bool hasStartedSpawning = false;
    public PlayerController player; // Reference to check if player has launched

    void Update()
    {
        if (!hasStartedSpawning && player != null && player.IsLaunched())
        {
            StartSpawning();
            hasStartedSpawning = true;
        }
    }

    void StartSpawning()
    {
        InvokeRepeating(nameof(SpawnAsteroid), 0f, spawnRate);
    }

    void SpawnAsteroid()
    {
        Vector2 spawnPosition = new Vector2(Random.Range(minX, maxX), spawnY);
        GameObject asteroid = Instantiate(asteroidPrefab, spawnPosition, Quaternion.identity);

        // Add velocity to move down
        Rigidbody2D rb = asteroid.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.velocity = new Vector2(0, -asteroidSpeed);
        }
    }

    public void StopSpawning()
    {
        CancelInvoke("SpawnAsteroid"); // Stop spawning new asteroids
    }

}

