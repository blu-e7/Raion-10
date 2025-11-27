using UnityEngine;

public class BorderDestroyer : MonoBehaviour
{
    [Tooltip("Centang ini HANYA untuk dinding KANAN agar musuh bisa masuk")]
    public bool isRightBorder = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        // 1. Jangan hapus Player
        if (other.CompareTag("Player")) return;

        // 2. LOGIKA BARU: Pengecualian untuk Dinding Kanan
        // Jika ini adalah dinding kanan DAN yang lewat adalah Musuh...
        if (isRightBorder && other.CompareTag("Enemy"))
        {
            return; // Biarkan lewat (JANGAN Destroy)
        }

        // 3. Hapus sisanya (Peluru, Asteroid, dll)
        Destroy(other.gameObject);
    }
}