using UnityEngine;

public class MiniMapToggle : MonoBehaviour
{
    public GameObject miniMapUI; // drag MiniMapView ke sini

    private bool isVisible = true;

    public void ToggleMiniMap()
    {
        isVisible = !isVisible;
        if (miniMapUI != null)
            miniMapUI.SetActive(isVisible);
    }
}
