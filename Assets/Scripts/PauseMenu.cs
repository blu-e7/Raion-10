using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI; // WAJIB ADA: Agar bisa akses Slider

public class PauseMenu : MonoBehaviour
{
    public static bool GameIsPaused = false;
    public GameObject pauseMenuUI;
    
    // Referensi ke Slider agar posisinya sinkron saat game mulai
    public Slider volumeSlider; 

    void Start()
    {
        // Saat game mulai, pastikan posisi slider sesuai dengan volume saat ini
        if (volumeSlider != null)
        {
            volumeSlider.value = AudioListener.volume;
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GameIsPaused) Resume();
            else Pause();
        }
    }

    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
    }

    void Pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
        
        // Update posisi slider lagi (jaga-jaga volume berubah dari tempat lain)
        if (volumeSlider != null)
        {
            volumeSlider.value = AudioListener.volume;
        }
    }

    // --- [FUNGSI BARU] PENGATUR VOLUME ---
    public void SetVolume(float volume)
    {
        // AudioListener mengontrol volume GLOBAL (BGM + SFX)
        AudioListener.volume = volume;
    }
    // -------------------------------------

    public void LoadMenu()
    {
        Time.timeScale = 1f;
        GameIsPaused = false;
        SceneManager.LoadScene("MainMenu");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}