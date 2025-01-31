using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    public GameObject bulletPrefab; // Assign the Bullet prefab in Inspector
    public Transform firePoint; // Assign an empty GameObject where bullets spawn
    public float bulletSpeed = 10f;
    public float fireRate = 0.3f; // Time between shots

    private float nextFireTime = 0f;

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && Time.time >= nextFireTime) // Detect tap and fire rate cooldown
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

