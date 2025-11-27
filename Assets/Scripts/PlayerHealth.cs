using UnityEngine;
using System.Collections; // Wajib ada untuk IEnumerator

public class PlayerHealth : MonoBehaviour
{
    [Header("Pengaturan Nyawa")]
    public int maxHealth = 3; // Total nyawa awal
    private int currentHealth;

    [Header("Immunity (Kekebalan)")]
    [Tooltip("Berapa detik pemain kebal setelah kena hit")]
    public float immunityDuration = 2f; 
    
    [Tooltip("Kecepatan kedip saat kebal (makin kecil makin cepat)")]
    public float flashDuration = 0.1f;
    
    private bool isImmune = false;
    private SpriteRenderer spriteRenderer;

    [Header("UI & Game Over")]
    public GameObject gameOverCanvas; // Drag Canvas Game Over kesini

    void Start()
    {
        currentHealth = maxHealth;
        
        // Ambil komponen gambar untuk efek kedip
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
        
        // Loop selama durasi immunity belum habis
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

    // Fungsi pembantu untuk ubah transparansi gambar
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
        
        // Munculkan layar Game Over
        if (gameOverCanvas != null)
        {
            gameOverCanvas.SetActive(true);
        }

        // Hancurkan Player
        Destroy(gameObject);
    }
}