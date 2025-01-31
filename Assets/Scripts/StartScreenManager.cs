using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class StartScreenManager : MonoBehaviour
{
    public GameObject startScreenCanvas; // Assign in Inspector
    private bool hasStarted = false;
    private Vector2 swipeStartPosition;

    void Update()
    {
        if (!hasStarted)
        {
            DetectSwipeToStart();
        }
    }

    void DetectSwipeToStart()
    {
        if (Input.GetMouseButtonDown(0))
        {
            swipeStartPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        }

        if (Input.GetMouseButtonUp(0))
        {
            Vector2 swipeEndPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 swipeDirection = swipeEndPosition - swipeStartPosition;
            float swipeDistance = swipeDirection.magnitude;

            if (swipeDistance > 0.2f) // Ensure a valid swipe
            {
                StartGame();
            }
        }
    }

    void StartGame()
    {
        hasStarted = true;
        startScreenCanvas.SetActive(false); // Hide start screen
    }
}
