using UnityEngine;

public class DontDestroyMusic : MonoBehaviour
{
    public AudioSource musicSource;    // Untuk musik latar
    public AudioClip backgroundMusic;

    public AudioSource sfxSource;      // Untuk efek suara (klik, dll)
    public AudioClip clickSound;

    private static DontDestroyMusic instance;

    void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    void Start()
    {
        if (musicSource != null && backgroundMusic != null)
        {
            musicSource.clip = backgroundMusic;
            musicSource.loop = true;
            musicSource.Play();
        }
    }

    // Fungsi untuk memainkan klik tombol
    public void PlayClickSound()
    {
        if (sfxSource != null && clickSound != null)
        {
            sfxSource.PlayOneShot(clickSound);
        }
    }
}
