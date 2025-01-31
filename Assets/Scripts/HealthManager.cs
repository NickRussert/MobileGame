using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class HealthManager : MonoBehaviour
{
    public int maxLives = 3;
    private int currentLives;
    public Image[] heartIcons;
    public GameObject gameOverPanel;
    public GameObject explosionPrefab; // Assign explosion effect in Inspector

    private PlayerController playerController;
    private PlayerShooting playerShooting;
    private AsteroidSpawner asteroidSpawner;
    private BackgroundScroll backgroundScroll; // Reference to BackgroundScroll

    void Start()
    {
        currentLives = maxLives;
        playerController = FindObjectOfType<PlayerController>();
        playerShooting = FindObjectOfType<PlayerShooting>();
        asteroidSpawner = FindObjectOfType<AsteroidSpawner>();
        backgroundScroll = FindObjectOfType<BackgroundScroll>(); // Get reference to BackgroundScroll

        UpdateHearts();

        if (gameOverPanel != null)
            gameOverPanel.SetActive(false);
    }

    public void LoseLife()
    {
        if (currentLives <= 0) return; // Prevent taking damage after Game Over

        currentLives--;

        if (playerController != null)
        {
            playerController.TakeDamage(); // Make the player blink
        }

        UpdateHearts();

        if (currentLives <= 0)
        {
            GameOver();
        }
    }

    void UpdateHearts()
    {
        for (int i = 0; i < heartIcons.Length; i++)
        {
            heartIcons[i].enabled = (i < currentLives);
        }
    }

    void GameOver()
    {
        Debug.Log("Game Over!");

        if (gameOverPanel != null)
            gameOverPanel.SetActive(true);

        if (playerController != null)
        {
            // Spawn explosion at player position
            Instantiate(explosionPrefab, playerController.transform.position, Quaternion.identity);

            // Remove the player after a slight delay
            Destroy(playerController.gameObject, 0.1f);
        }

        if (playerShooting != null)
            playerShooting.enabled = false;

        if (asteroidSpawner != null)
            asteroidSpawner.StopSpawning();

        if (backgroundScroll != null)
            backgroundScroll.StopScrolling();

        // Destroy all remaining asteroids
        GameObject[] asteroids = GameObject.FindGameObjectsWithTag("Asteroid");
        foreach (GameObject asteroid in asteroids)
        {
            Destroy(asteroid);
        }
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}

