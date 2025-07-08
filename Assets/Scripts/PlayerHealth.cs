using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    [Header("Health Settings")]
    public int maxHealth = 100;
    private int currentHealth;

    // Flag supaya mati hanya sekali
    private bool isDead = false;

    private void Start()
    {
        currentHealth = maxHealth;
    }

    // Dipanggil saat menerima damage
    public void TakeDamage(int damage)
    {
        if (isDead) return;          // sudah mati? abaikan

        currentHealth -= damage;
        Debug.Log($"Player terkena damage! Sisa HP: {currentHealth}");

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    // Logika kematian
    private void Die()
    {
        isDead = true;
        Debug.Log("Player mati! Menghilang dan restart level...");

        // 1️⃣ Hilangkan visual
        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        if (sr != null) sr.enabled = false;

        // 2️⃣ Matikan collider (opsional tapi disarankan)
        Collider2D col = GetComponent<Collider2D>();
        if (col != null) col.enabled = false;

        // 3️⃣ Nonaktifkan animator (opsional)
        Animator anim = GetComponent<Animator>();
        if (anim != null) anim.enabled = false;

        // 4️⃣ Restart level setelah 3 detik
        StartCoroutine(RestartLevelAfterDelay());
    }

    private System.Collections.IEnumerator RestartLevelAfterDelay()
    {
        yield return new WaitForSeconds(3f);     // ⏳ tunda 3 detik
        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.name);
    }

    // Menyembuhkan player
    public void Heal(int amount)
    {
        if (isDead) return;

        currentHealth += amount;
        if (currentHealth > maxHealth) currentHealth = maxHealth;

        Debug.Log($"Player sembuh! HP sekarang: {currentHealth}");
    }

    // Akses eksternal (UI, dll.)
    public int CurrentHealth => currentHealth;
}
