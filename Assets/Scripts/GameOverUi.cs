using UnityEngine;
using UnityEngine.SceneManagement; // WAJIB ADA: Agar bisa pindah scene

public class GameOverUI : MonoBehaviour
{
    // Fungsi untuk tombol Restart
    public void RestartGame()
    {
        // Memuat ulang scene yang sedang aktif saat ini
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        
        // Pastikan waktu berjalan lagi (siapa tahu tadi di-pause saat game over)
        Time.timeScale = 1f; 
    }

    // Fungsi untuk tombol Main Menu
    public void LoadMainMenu()
    {
        // Ganti "MainMenu" dengan nama file scene menu kamu nanti
        SceneManager.LoadScene("MainMenu");
        
        Time.timeScale = 1f;
    }

    // Fungsi untuk tombol Quit (Keluar Game)
    public void QuitGame()
    {
        Debug.Log("Keluar dari Game!");
        Application.Quit();
    }
}