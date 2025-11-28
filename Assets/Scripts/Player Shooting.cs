using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    [Header("Setup Peluru")]
    public GameObject bulletPrefab;
    public Transform firePoint; // Titik keluar peluru (moncong)

    void Update()
    {
        // Jika tombol Spasi ditekan (atau Klik Kiri)
        if (Input.GetButtonDown("Jump") || Input.GetButtonDown("Fire1"))
        {
            Shoot();
        }
    }

    void Shoot()
    {
        // Kita tambahkan rotasi -90 derajat agar peluru "tidur" (Horizontal)
        // Jika masih salah arah, coba ganti -90f menjadi 90f
        if (AudioManager.instance != null)
        {
            AudioManager.instance.PlaySFX(AudioManager.instance.shootClip);
        }
        Quaternion rotationCorrection = transform.rotation * Quaternion.Euler(0, 0, -90f);

        Instantiate(bulletPrefab, firePoint.position, rotationCorrection);
    }
}