using UnityEngine;
using UnityEngine.UI;

public class ButtonSoundBinder : MonoBehaviour
{
    void Start()
    {
        // Ambil semua tombol di scene
        Button[] buttons = FindObjectsOfType<Button>();

        foreach (Button btn in buttons)
        {
            // Tambahkan listener ke setiap tombol
            btn.onClick.AddListener(() =>
            {
                // Cari dan mainkan suara klik
                DontDestroyMusic music = FindObjectOfType<DontDestroyMusic>();
                if (music != null)
                {
                    music.PlayClickSound();
                }
            });
        }
    }
}
