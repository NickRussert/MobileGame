using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    public GameObject bulletPrefab; // Assign the Bullet prefab in Inspector
    public Transform firePoint; // Assign an empty GameObject where bullets spawn
    public float bulletSpeed = 10f;
    public float fireRate = 0.3f; // Time between shots

    private float nextFireTime = 0f;
    private PlayerController playerController; // Reference to check if game started

    void Start()
    {
        playerController = FindObjectOfType<PlayerController>(); // Find the player
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
                rb.velocity = new Vector2(0, bulletSpeed); // Move straight up
            }
        }
    }
}

