using UnityEngine;

public class AutoDestroy : MonoBehaviour
{
    [Tooltip("Berapa detik objek ini akan bertahan hidup sebelum hancur")]
    public float delay = 1f; // Default 1 detik

    void Start()
    {
        // Fungsi Destroy punya parameter kedua untuk waktu tunda (delay)
        Destroy(gameObject, delay);
    }
}