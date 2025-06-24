using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private bool isPaused = false;

    [Header("UI Panel")]
    public GameObject pausePanel;

    public void TogglePause()
    {
        isPaused = !isPaused;

        if (isPaused)
        {
            Time.timeScale = 0f;
            if (pausePanel != null)
                pausePanel.SetActive(true);
            Debug.Log("Game Paused");
        }
        else
        {
            ResumeGame();
        }
    }

    // Fungsi untuk tombol Resume
    public void ResumeGame()
    {
        isPaused = false;
        Time.timeScale = 1f;
        if (pausePanel != null)
            pausePanel.SetActive(false);
        Debug.Log("Game Resumed");
    }

    // Fungsi untuk tombol Quit
    public void QuitGame()
    {
        Debug.Log("Kembali ke Main Menu");
        Time.timeScale = 1f;
        SceneManager.LoadScene("mainmenu");
    }
}
