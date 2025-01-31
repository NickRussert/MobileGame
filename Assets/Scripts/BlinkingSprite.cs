using UnityEngine;
using System.Collections;

public class BlinkingSprite : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    public float blinkSpeed = 0.5f; // Speed of blinking

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();

        if (spriteRenderer == null) // Check if the component exists
        {
            Debug.LogError("BlinkingSprite script is attached to an object without a SpriteRenderer!");
            return;
        }

        StartCoroutine(BlinkEffect());
    }

    IEnumerator BlinkEffect()
    {
        while (true)
        {
            if (spriteRenderer != null) // Check before toggling
            {
                spriteRenderer.enabled = !spriteRenderer.enabled; // Toggle visibility
            }
            yield return new WaitForSeconds(blinkSpeed);
        }
    }
}

