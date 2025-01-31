using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class HealthManager : MonoBehaviour
{
    public int maxLives = 3; // Maximum lives
    private int currentLives;

    public Image[] heartIcons; // UI heart images

    void Start()
    {
        currentLives = maxLives;
        UpdateHearts();
    }

    public void LoseLife()
    {
        currentLives--;

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
            if (i < currentLives)
                heartIcons[i].enabled = true; // Show heart
            else
                heartIcons[i].enabled = false; // Hide heart
        }
    }

    void GameOver()
    {
        Debug.Log("Game Over!");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); // Restart game
    }
}

