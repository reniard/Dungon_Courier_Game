using UnityEngine;
using TMPro;

public class HealthDisplay : MonoBehaviour
{
    public PlayerHealth playerHealth;
    public TextMeshProUGUI healthText;

    // Warna khusus
    private Color hijauTua = new Color32(0x00, 0x64, 0x00, 0xFF);     // #006400
    private Color kuningGelap = new Color32(0xB8, 0x86, 0x0B, 0xFF);  // #B8860B
    private Color merahTua = new Color32(0x4B, 0x00, 0x00, 0xFF);     // #4B0000

    void Update()
    {
        if (playerHealth != null && healthText != null)
        {
            int hp = playerHealth.CurrentHealth;

            if (hp > 0)
            {
                healthText.text = "Health: " + hp;

                if (hp > 50)
                    healthText.color = hijauTua;
                else if (hp > 30)
                    healthText.color = kuningGelap;
                else
                    healthText.color = Color.red;
            }
            else
            {
                healthText.text = "DEAD";
                healthText.color = merahTua;
            }
        }
    }
}
