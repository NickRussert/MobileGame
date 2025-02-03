using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    public GameObject bulletPrefab; // Assign the Bullet prefab in Inspector
    public Transform firePoint; // Assign an empty GameObject where bullets spawn
    public float bulletSpeed = 10f;
    public float fireRate = 0.3f; // Time between shots

    private float nextFireTime = 0f;
    private PlayerController playerController; // Reference to check if game started

    [Header("Audio")]
    public AudioClip shootingSound; // Assign in Inspector
    private AudioSource audioSource;

    void Start()
    {
        playerController = FindObjectOfType<PlayerController>(); // Find the player
        audioSource = GetComponent<AudioSource>(); // Get AudioSource
    }

    void Update()
    {
        if (playerController != null && !playerController.IsLaunched()) return; // Prevent shooting before launch

        if (Input.GetMouseButtonDown(0) && Time.time >= nextFireTime)
        {
            Shoot();
            nextFireTime = Time.time + fireRate;
        }
    }

    void Shoot()
    {
        if (bulletPrefab != null && firePoint != null)
        {
            GameObject bullet = Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);
            Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();

            if (rb != null)
            {
                rb.velocity = new Vector2(0, bulletSpeed); // Move bullet straight up
            }

            // Play shooting sound
            if (audioSource != null && shootingSound != null)
            {
                audioSource.volume = 0.5f; //  Adjust this value (0.0 = silent, 1.0 = full volume)

                audioSource.PlayOneShot(shootingSound);
            }
        }
    }
}
