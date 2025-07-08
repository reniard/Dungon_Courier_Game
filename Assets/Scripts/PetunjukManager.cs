using UnityEngine;

public class PetunjukManager : MonoBehaviour
{
    public GameObject petunjukPanel;

    public void ShowPetunjuk()
    {
        petunjukPanel.SetActive(true);
    }

    public void ClosePetunjuk()
    {
        petunjukPanel.SetActive(false);
    }
}
