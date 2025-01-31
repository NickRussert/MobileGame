using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rb;
    private bool isLaunched = false;
    private SpriteRenderer spriteRenderer;
    private Collider2D playerCollider; // Reference to player's collider

    [Header("Launch Settings")]
    public float minSwipeDistance = 0.2f; // Minimum swipe distance required to launch
    public float launchMultiplier = 5f;   // Adjust for desired speed

    [Header("Gyro Movement")]
    public float tiltSpeed = 3f; // Adjust sensitivity

    [Header("Blink Effect")]
    public float blinkDuration = 1f; // Total duration of blinking
    public float blinkInterval = 0.1f; // Speed of blinking

    private bool isInvincible = false; // Track invincibility status
    private Vector2 swipeStartPosition;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 0; // Ensure no gravity (top-down view)
        spriteRenderer = GetComponent<SpriteRenderer>(); // Get the sprite renderer
        playerCollider = GetComponent<Collider2D>(); // Get the player's collider

        // Enable Gyroscope
        if (SystemInfo.supportsGyroscope)
            Input.gyro.enabled = true;
    }

    void Update()
    {
        if (!isLaunched)
        {
            HandleSwipeLaunch();
        }
        else
        {
            HandleGyroMovement();
            ClampToScreen();
        }
    }

    void HandleSwipeLaunch()
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

            if (swipeDistance >= minSwipeDistance && swipeDirection.y > Mathf.Abs(swipeDirection.x))
            {
                Vector2 launchForce = swipeDirection.normalized * launchMultiplier;
                rb.velocity = launchForce;
                isLaunched = true; // Plane is now in motion
            }
        }
    }

    void HandleGyroMovement()
    {
        if (SystemInfo.supportsGyroscope)
        {
            Vector3 tilt = Input.gyro.rotationRateUnbiased;
            float moveX = tilt.y * tiltSpeed; // Tilting LEFT/RIGHT moves LEFT/RIGHT
            float moveY = -tilt.x * tiltSpeed; // Tilting UP/DOWN moves UP/DOWN

            rb.velocity = new Vector2(moveX, moveY);
        }
    }

    void ClampToScreen()
    {
        Vector3 minScreenBounds = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, 0));
        Vector3 maxScreenBounds = Camera.main.ViewportToWorldPoint(new Vector3(1, 1, 0));

        Vector3 clampedPosition = transform.position;
        clampedPosition.x = Mathf.Clamp(clampedPosition.x, minScreenBounds.x + 0.5f, maxScreenBounds.x - 0.5f);
        clampedPosition.y = Mathf.Clamp(clampedPosition.y, minScreenBounds.y + 0.5f, maxScreenBounds.y - 0.5f);

        transform.position = clampedPosition;
    }

    public bool IsLaunched()
    {
        return isLaunched;
    }

    // Call this when the player loses a life
    public void TakeDamage()
    {
        if (!isInvincible) // Only take damage if not invincible
        {
            StartCoroutine(BlinkAndInvincible());
        }
    }

    IEnumerator BlinkAndInvincible()
    {
        isInvincible = true; // Enable invincibility
        playerCollider.enabled = false; // Disable collider for invincibility

        float elapsedTime = 0f;
        while (elapsedTime < blinkDuration)
        {
            spriteRenderer.enabled = !spriteRenderer.enabled; // Toggle visibility
            yield return new WaitForSeconds(blinkInterval);
            elapsedTime += blinkInterval;
        }

        spriteRenderer.enabled = true; // Ensure visibility
        playerCollider.enabled = true; // Re-enable collider
        isInvincible = false; // Disable invincibility
    }
}

