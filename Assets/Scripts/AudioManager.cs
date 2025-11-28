using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    [Header("Sumber Suara")]
    public AudioSource sfxSource; // Speaker khusus efek

    [Header("Klip Audio (Drag File Kesini)")]
    public AudioClip shootClip;
    public AudioClip explodeClip;
    public AudioClip hitClip;
    public AudioClip winClip;
    public AudioClip loseClip;
    public AudioClip enemyshootClip;
    public AudioClip tripleshootClip;

    void Awake()
    {
        // Singleton (Agar bisa dipanggil dari mana saja)
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Fungsi sakti untuk memutar suara apapun
    public void PlaySFX(AudioClip clip)
    {
        if (GameManager.instance != null && GameManager.instance.isGameOver)
        {
            // Pengecualian: Kalau klip yang mau diputar adalah suara kalah, biarkan lewat
            if (clip != loseClip) 
            {
                return; 
            }
        }
        if (clip == loseClip)
            {
                // Jika ini suara kalah, putar 3x lebih keras (Ubah angka 3f jika kurang keras)
                // Parameter kedua adalah "Volume Scale"
                sfxSource.PlayOneShot(clip, 3f); 
            }
        if (clip != null)
        {
            // PlayOneShot: Memutar suara sampai habis tanpa memotong suara lain
            // (Cocok untuk tembakan beruntun)
            sfxSource.PlayOneShot(clip);
        }
    }
}