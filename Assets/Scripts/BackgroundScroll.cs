using UnityEngine;

public class BackgroundScroll : MonoBehaviour
{
    public float scrollSpeed = 0.1f; // Positive moves right, negative moves left
    private Renderer bgRenderer;
    private Vector2 offset;
    private bool isGameOver = false;


    public PlayerController player; // Reference to detect launch

    void Start()
    {
        bgRenderer = GetComponent<Renderer>();
    }

    void Update()
    {
        if (isGameOver) return; // Stop scrolling on game over


        if (player != null && player.IsLaunched()) // Only start scrolling after launch
        {
            if (bgRenderer != null)
            {
                offset = bgRenderer.material.mainTextureOffset;

                offset.y = 0; // Lock vertical movement
                offset.x += scrollSpeed * Time.deltaTime; // Scroll horizontally

                bgRenderer.material.mainTextureOffset = offset;
            }
        }
    }

    public void StopScrolling()
    {
        isGameOver = true; // This stops the background from updating
    }
}

