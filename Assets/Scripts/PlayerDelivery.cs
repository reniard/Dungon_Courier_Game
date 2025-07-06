using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class PlayerDelivery : MonoBehaviour
{
    private enum DeliveryStatus
    {
        TidakDibawa,
        Dibawa,
        Terkirim
    }

    private DeliveryStatus currentStatus = DeliveryStatus.TidakDibawa;

    public TextMeshProUGUI statusText;
    public GameObject finishPanel;         // Panel ucapan selamat
    public GameObject backgroundOverlay;   // Overlay latar belakang gelap (jika ada)

    private void Start()
    {
        UpdateStatusText();

        // Sembunyikan panel dan overlay di awal
        if (finishPanel != null)
            finishPanel.SetActive(false);

        if (backgroundOverlay != null)
            backgroundOverlay.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Item") && currentStatus == DeliveryStatus.TidakDibawa)
        {
            Destroy(collision.gameObject);
            currentStatus = DeliveryStatus.Dibawa;
            Debug.Log("📦 Barang diambil!");
            UpdateStatusText();
        }
        else if (collision.CompareTag("Goal") && currentStatus == DeliveryStatus.Dibawa)
        {
            currentStatus = DeliveryStatus.Terkirim;
            Debug.Log("🎯 Barang dikirim!");
            UpdateStatusText();

            string currentScene = SceneManager.GetActiveScene().name;
            
            if (currentScene == "Level3")
            {
                // Jika di Level3, tampilkan panel selamat + overlay
                if (backgroundOverlay != null)
                    backgroundOverlay.SetActive(true);

                if (finishPanel != null)
                    finishPanel.SetActive(true);

                Time.timeScale = 0f; // ⏸ Game langsung di-pause
            }

            else
            {
                // Pindah ke scene berikutnya (bukan Level3)
                Invoke("LoadNextScene", 1.5f);
            }
        }
    }

    void UpdateStatusText()
    {
        if (statusText == null) return;

        switch (currentStatus)
        {
            case DeliveryStatus.TidakDibawa:
                statusText.text = "Status Barang : Tidak Dibawa. Cari Barang Sekarang!";
                break;
            case DeliveryStatus.Dibawa:
                statusText.text = "Status Barang : Dibawa. Antar Ke Tempat Tujuan!";
                break;
            case DeliveryStatus.Terkirim:
                statusText.text = "Status Barang : Terkirim! Bersiap ke level selanjutnya....";
                break;
        }
    }

    void LoadNextScene()
    {
        int currentIndex = SceneManager.GetActiveScene().buildIndex;
        int nextIndex = currentIndex + 1;

        if (nextIndex < SceneManager.sceneCountInBuildSettings)
        {
            Debug.Log("🎬 Pindah ke scene berikutnya (index " + nextIndex + ")");
            SceneManager.LoadScene(nextIndex);
        }
        else
        {
            Debug.Log("🏁 Tidak ada scene berikutnya. Mungkin game selesai.");
        }
    }

    // Fungsi tombol "Close" di FinishPanel
    public void OnCloseFinishPanel()
    {
        if (finishPanel != null)
            finishPanel.SetActive(false);

        if (backgroundOverlay != null)
            backgroundOverlay.SetActive(false);

        SceneManager.LoadScene("menulevel"); // Pindah ke scene menu level
    }

    public bool IsCarryingItem()
    {
        return currentStatus == DeliveryStatus.Dibawa;
    }
}
