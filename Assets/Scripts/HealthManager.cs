using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class HealthManager : MonoBehaviour
{
    public int maxLives = 3;
    private int currentLives;

    public Image[] heartIcons;
    private PlayerController playerController;

    void Start()
    {
        currentLives = maxLives;
        playerController = FindObjectOfType<PlayerController>(); // Get player reference
        UpdateHearts();
    }

    public void LoseLife()
    {
        currentLives--;

        if (playerController != null)
        {
            playerController.TakeDamage(); // Trigger blinking effect
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
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); // Restart game
    }
}

