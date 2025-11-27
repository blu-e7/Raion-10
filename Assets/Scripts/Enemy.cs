using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("Score")]
    public int scoreValue = 10; // Default 10 poin
    [Header("Stats")]
    public float moveSpeed = 3f;
    public float fireRate = 2f; 
    public int crashDamage = 1; // [BARU] Kerusakan jika menabrak badan pemain
    
    private float fireCountdown = 0f;

    [Header("Tipe Serangan")]
    public bool useTripleShot = false; 
    public float spreadAngle = 15f; 

    [Header("Components")]
    public GameObject enemyBulletPrefab;
    private Vector3 moveDirection = Vector3.left; 

    void Start()
    {
        fireCountdown = 1f / fireRate;
    }

    void Update()
    {
        transform.Translate(moveDirection * moveSpeed * Time.deltaTime);

        fireCountdown -= Time.deltaTime;
        if (fireCountdown <= 0f)
        {
            Shoot();
            fireCountdown = 1f / fireRate; 
        }
    }

    void Shoot()
    {
        if (enemyBulletPrefab != null)
        {
            Vector3 spawnPosition = transform.position;
            spawnPosition.y -= 0.3f;
            float baseAngle = 90f; 

            if (useTripleShot)
            {
                CreateBullet(spawnPosition, baseAngle);
                CreateBullet(spawnPosition, baseAngle - spreadAngle);
                CreateBullet(spawnPosition, baseAngle + spreadAngle);
            }
            else
            {
                CreateBullet(spawnPosition, baseAngle);
            }
        }
    }

    void CreateBullet(Vector3 pos, float angle)
    {
        Quaternion rotation = Quaternion.Euler(0, 0, angle);
        Instantiate(enemyBulletPrefab, pos, rotation);
    }

    // --- BAGIAN BARU: DETEKSI TABRAKAN BADAN ---
    private void OnTriggerEnter2D(Collider2D other)
    {
        // Jika menabrak Player
        if (other.CompareTag("Player"))
        {
            // 1. Ambil script nyawa player
            PlayerHealth playerHp = other.GetComponent<PlayerHealth>();
            
            // 2. Kurangi nyawa
            if (playerHp != null)
            {
                playerHp.TakeDamage(crashDamage); 
            }

            // 3. Hancurkan musuh ini (karena sudah nabrak, dia meledak/mati)
            Destroy(gameObject);
        }
    }

    void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}