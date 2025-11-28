using UnityEngine;
using UnityEngine.UI; // PENTING: Wajib ada agar bisa akses UI Text

public class GameManager : MonoBehaviour
{
    public static GameManager instance; // Singleton agar mudah dipanggil dari mana saja

    [Header("UI References")]
    public Text scoreText;
    public Text hpText;
    public bool isGameOver = false;

    private int currentScore = 0;

    void Awake()
    {
        // Setup Singleton
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Fungsi menambah skor
    public void AddScore(int amount)
    {
        currentScore += amount;
        scoreText.text = "Score: " + currentScore; // Update tulisan di layar
    }

    // Fungsi update tampilan HP
    public void UpdateHP(int currentHealth)
    {
        hpText.text = "HP: " + currentHealth;
    }
}