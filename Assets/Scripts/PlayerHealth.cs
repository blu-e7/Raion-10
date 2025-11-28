using UnityEngine;
using System.Collections; // Wajib ada untuk IEnumerator

public class PlayerHealth : MonoBehaviour
{
    [Header("Pengaturan Nyawa")]
    public int maxHealth = 3; 
    private int currentHealth;

    [Header("Immunity (Kekebalan)")]
    [Tooltip("Berapa detik pemain kebal setelah kena hit")]
    public float immunityDuration = 2f; 
    
    [Tooltip("Kecepatan kedip saat kebal (makin kecil makin cepat)")]
    public float flashDuration = 0.1f;
    
    private bool isImmune = false;
    private SpriteRenderer spriteRenderer;

    [Header("Effects & UI")]
    public GameObject gameOverCanvas; // Drag Canvas Game Over kesini
    public GameObject explosionEffect; // [BARU] Drag Prefab Explosion kesini

    void Start()
    {
        currentHealth = maxHealth;
        
        // Ambil komponen gambar untuk efek kedip
        // Jika gambarmu ada di anak object, gunakan GetComponentInChildren<SpriteRenderer>()
        spriteRenderer = GetComponent<SpriteRenderer>();

        // Pastikan layar Game Over mati saat mulai
        if(gameOverCanvas != null) 
            gameOverCanvas.SetActive(false);

        // Update UI HP ke angka penuh saat game baru mulai
        if (GameManager.instance != null)
        {
            GameManager.instance.UpdateHP(currentHealth);
        }
    }

    public void TakeDamage(int damageAmount)
    {
        // 1. CEK KEBAL: Jika sedang immune, tolak damage
        if (isImmune) return;

        // 2. Kurangi Nyawa
        currentHealth -= damageAmount;
        Debug.Log("Nyawa tersisa: " + currentHealth);

        // Update UI HP di layar
        if (GameManager.instance != null)
        {
            GameManager.instance.UpdateHP(currentHealth);
        }

        // MAINKAN SUARA KENA HIT
        if (AudioManager.instance != null)
        {
            AudioManager.instance.PlaySFX(AudioManager.instance.hitClip);
        }

        // 3. Cek Mati atau Kebal
        if (currentHealth <= 0)
        {
            Die();
        }
        else
        {
            // Mulai proses kekebalan sementara
            StartCoroutine(BecomeImmune());
        }
    }

    // Fungsi loop untuk efek kedip-kedip
    IEnumerator BecomeImmune()
    {
        isImmune = true;
        
        float timer = 0f;
        while (timer < immunityDuration)
        {
            // Jadi transparan (samar)
            SetPlayerTransparency(0.5f);
            yield return new WaitForSeconds(flashDuration);

            // Jadi terlihat jelas
            SetPlayerTransparency(1f);
            yield return new WaitForSeconds(flashDuration);

            timer += flashDuration * 2;
        }

        // Selesai: Pastikan terlihat jelas lagi & matikan status kebal
        SetPlayerTransparency(1f);
        isImmune = false;
    }

    void SetPlayerTransparency(float alpha)
    {
        if (spriteRenderer != null)
        {
            Color newColor = spriteRenderer.color;
            newColor.a = alpha; 
            spriteRenderer.color = newColor;
        }
    }

    void Die()
    {
        Debug.Log("Player Mati!");

        // 1. [BARU] MUNCULKAN VISUAL LEDAKAN
        if (explosionEffect != null)
        {
            Instantiate(explosionEffect, transform.position, transform.rotation);
        }

        // 2. [BARU] MAINKAN SUARA LEDAKAN & KALAH
        if (AudioManager.instance != null)
        {
            // Suara ledakan fisik
            AudioManager.instance.PlaySFX(AudioManager.instance.explodeClip);
            
            // Suara Game Over (Lose Clip)
            AudioManager.instance.PlaySFX(AudioManager.instance.loseClip);
        }

        // 3. SET STATUS GAME OVER (Agar musuh berhenti nembak)
        if (GameManager.instance != null)
        {
            GameManager.instance.isGameOver = true;
        }

        // 4. KECILKAN VOLUME MUSIK
        if (MusicManager.instance != null)
        {
            MusicManager.instance.LowerVolume();
        }
        
        // 5. MUNCULKAN LAYAR GAME OVER
        if (gameOverCanvas != null)
        {
            gameOverCanvas.SetActive(true);
        }

        // 6. HANCURKAN PLAYER
        Destroy(gameObject);
    }
}