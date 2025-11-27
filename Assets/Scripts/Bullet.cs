using UnityEngine;

public class Bullet : MonoBehaviour
{
    [Header("Status Peluru")]
    public float speed = 10f;       // Kecepatan peluru
    public float lifetime = 3f;     // Waktu sebelum hancur otomatis
    public int damage = 1;          // Jumlah kerusakan

    [Header("Target Sasaran")]
    [Tooltip("Isi 'Enemy' jika ini peluru Pemain. Isi 'Player' jika ini peluru Musuh.")]
    public string targetTag = "Enemy"; 

    void Update()
    {
        // Bergerak ke arah 'Atas/Kepala' peluru sendiri
        // (Sesuai perbaikan rotasi agar peluru horizontal)
        transform.Translate(Vector2.up * speed * Time.deltaTime);

        // Hancur otomatis jika waktu habis (lifetime)
        lifetime -= Time.deltaTime;
        if (lifetime <= 0f)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D hitInfo)
    {
        // Cek apakah objek yang ditabrak memiliki Tag sesuai target
        if (hitInfo.CompareTag(targetTag))
        {
            // A. JIKA TARGETNYA PLAYER (Peluru Musuh kena Pemain)
            if (targetTag == "Player")
            {
                PlayerHealth playerHp = hitInfo.GetComponent<PlayerHealth>();
                if (playerHp != null)
                {
                    playerHp.TakeDamage(damage);
                }
            }
            // B. JIKA TARGETNYA MUSUH (Peluru Pemain kena Musuh)
            else if (targetTag == "Enemy")
            {
                // Ambil script Enemy untuk tahu berapa skor-nya
                Enemy enemyScript = hitInfo.GetComponent<Enemy>();
                
                // Panggil GameManager untuk tambah skor
                if (enemyScript != null && GameManager.instance != null)
                {
                    GameManager.instance.AddScore(enemyScript.scoreValue);
                }

                // Hancurkan Musuh
                Destroy(hitInfo.gameObject);
            }

            // Hancurkan peluru itu sendiri setelah menabrak target
            Destroy(gameObject);
        }
        
        // Kode cek "Ground" sudah dihapus agar tidak error.
        // Peluru yang meleset akan otomatis dihapus oleh "BorderDestroyer" di pinggir layar.
    }
}