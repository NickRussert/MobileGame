using UnityEngine;

public class BackgroundScroll : MonoBehaviour
{
    public float scrollSpeed = 0.1f; // Positive moves right, negative moves left
    private Renderer bgRenderer;
    private Vector2 offset;

    public PlayerController player; // Reference to detect launch

    void Start()
    {
        bgRenderer = GetComponent<Renderer>();
    }

    void Update()
    {
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
}

