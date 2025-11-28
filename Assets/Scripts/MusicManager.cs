using UnityEngine;
using System.Collections;

public class MusicManager : MonoBehaviour
{
    public static MusicManager instance;
    private AudioSource audioSource;
    private float originalVolume; // Untuk mengingat volume asli

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            
            // Ambil Audio Source dan simpan volume aslinya
            audioSource = GetComponent<AudioSource>();
            if (audioSource != null)
            {
                originalVolume = audioSource.volume;
            }
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Fungsi untuk dipanggil saat Game Over
    public void LowerVolume()
    {
        if (audioSource != null)
        {
            // Turunkan volume jadi 20% dari aslinya
            // Kita pakai Coroutine biar turunnya halus (Fade Out)
            StartCoroutine(FadeOut(0.2f)); 
        }
    }

    // Fungsi untuk dipanggil saat Restart
    public void ResetVolume()
    {
        if (audioSource != null)
        {
            audioSource.volume = originalVolume; // Balikin ke normal
        }
    }

    IEnumerator FadeOut(float targetVolume)
    {
        float startVolume = audioSource.volume;
        float duration = 1.0f; // Durasi mengecil dalam 1 detik
        float timer = 0f;

        while (timer < duration)
        {
            timer += Time.unscaledDeltaTime; // Pakai unscaled biar tetap jalan kalau game di-pause
            audioSource.volume = Mathf.Lerp(startVolume, targetVolume * originalVolume, timer / duration);
            yield return null;
        }
    }
}