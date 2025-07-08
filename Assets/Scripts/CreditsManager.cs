using UnityEngine;

public class CreditsManager : MonoBehaviour
{
    public GameObject creditsPanel;

    public void ShowCredits()
    {
        creditsPanel.SetActive(true);
    }

    public void CloseCredits()
    {
        creditsPanel.SetActive(false);
    }
}
