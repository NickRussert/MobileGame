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
    private BackgroundScroll backgroundScroll;

    [Header("Audio")]
    public AudioClip damageSound; // Assign in Inspector
    public AudioClip gameOverSound; // Assign in Inspector
    private AudioSource audioSource;

    void Start()
    {
        currentLives = maxLives;
        playerController = FindObjectOfType<PlayerController>();
        playerShooting = FindObjectOfType<PlayerShooting>();
        asteroidSpawner = FindObjectOfType<AsteroidSpawner>();
        backgroundScroll = FindObjectOfType<BackgroundScroll>();

        audioSource = GetComponent<AudioSource>(); // Get AudioSource

        UpdateHearts(); // ✅ Ensure hearts update at start

        if (gameOverPanel != null)
            gameOverPanel.SetActive(false);
    }

    public void LoseLife()
    {
        if (currentLives <= 0) return; // Prevent losing extra lives after Game Over

        currentLives--;

        if (playerController != null)
        {
            playerController.TakeDamage();
        }

        PlayDamageSound(); // 🔊 Play damage sound (before Game Over check)

        UpdateHearts(); // ✅ Ensure UI updates

        if (currentLives <= 0)
        {
            GameOver();
        }
    }

    void PlayDamageSound()
    {
        if (audioSource != null && damageSound != null && currentLives > 0)
        {
            audioSource.PlayOneShot(damageSound); // 🔊 Play damage sound
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
            // Stop background sound before playing Game Over sound
            playerController.StopBackgroundSound();

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

        // Play Game Over sound
        if (audioSource != null && gameOverSound != null)
        {
            audioSource.PlayOneShot(gameOverSound);
            Debug.Log("Game Over sound played!"); // Debug to confirm sound is triggered
        }

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

