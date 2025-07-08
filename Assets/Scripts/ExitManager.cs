using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ExitManager : MonoBehaviour
{
    public GameObject exitPanel; // Drag ExitConfirmationPanel ke sini
    public Button keluarButton;  // Drag tombol keluar ke sini

    void Start()
    {
        keluarButton.onClick.AddListener(ShowExitConfirmation);
        exitPanel.SetActive(false);
    }

    public void ShowExitConfirmation()
    {
        exitPanel.SetActive(true);
    }

    public void OnYesClicked()
    {
        Application.Quit();
        Debug.Log("Game keluar"); // Tidak akan terlihat di build, hanya di Editor
    }

    public void OnNoClicked()
    {
        exitPanel.SetActive(false);
    }
}
