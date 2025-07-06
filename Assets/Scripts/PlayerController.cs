using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [Header("Health System")]
    public int maxHealth = 100;
    private int currentHealth;
    public TextMeshProUGUI healthText;

    [Header("Attack Settings")]
    public int attackDamage = 10;
    public float attackRange = 2.5f;

    [Header("Knockback Settings")]
    [SerializeField] private float knockBackTime = 0.2f;
    [SerializeField] private float knockBackThrust = 10f;

    private bool isKnockedBack = false;
    private bool facingLeft = false;

    [SerializeField] private float moveSpeed = 1f;
    private PlayerControl playerControl;
    private Vector2 movement;
    private Rigidbody2D rb;
    private Animator anim;
    public SpriteRenderer sprite;

    private void Awake()
    {
        playerControl = new PlayerControl();
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();

        currentHealth = maxHealth;
        UpdateHealthUI();
    }

    private void OnEnable()
    {
        playerControl.Enable();
    }

    private void Update()
    {
        PlayerInput();

        // Serang via keyboard (Space)
        if (Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            TriggerAttack();
        }
    }

    private void FixedUpdate()
    {
        if (isKnockedBack) return;
        AdjustPlayerFacingDirection();
        Move();
    }

    private void PlayerInput()
    {
        movement = playerControl.Movement.Move.ReadValue<Vector2>();
        anim.SetFloat("moveX", movement.x);
        anim.SetFloat("moveY", movement.y);
    }

    private void Move()
    {
        rb.MovePosition(rb.position + movement * (moveSpeed * Time.fixedDeltaTime));
    }

    private void AdjustPlayerFacingDirection()
    {
        if (movement.x != 0f)
        {
            sprite.flipX = movement.x < 0f;
            facingLeft = movement.x < 0f;
        }
    }

    public void TakeDamage(int damage, Vector2 direction)
    {
        if (isKnockedBack) return;

        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            currentHealth = 0;
            Debug.Log("Player Mati");
        }

        StartCoroutine(HandleKnockback(direction.normalized));
        UpdateHealthUI();
    }

    private void UpdateHealthUI()
    {
        if (healthText != null)
            healthText.text = "Health: " + currentHealth;
    }

    private IEnumerator HandleKnockback(Vector2 direction)
    {
        isKnockedBack = true;
        rb.velocity = Vector2.zero;

        Vector2 force = direction * knockBackThrust * rb.mass;
        rb.AddForce(force, ForceMode2D.Impulse);

        yield return new WaitForSeconds(knockBackTime);
        rb.velocity = Vector2.zero;
        isKnockedBack = false;
    }

    // Fungsi untuk serang dari tombol dan keyboard
    public void OnAttackButtonClicked()
    {
        TriggerAttack();
    }

    private void TriggerAttack()
    {
        anim.SetTrigger("attack");

        // Cek semua enemy dalam jangkauan
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (GameObject enemy in enemies)
        {
            float distance = Vector2.Distance(transform.position, enemy.transform.position);
            if (distance <= attackRange)
            {
                EnemyHealth enemyHealth = enemy.GetComponent<EnemyHealth>();
                if (enemyHealth != null)
                {
                    enemyHealth.TakeDamage(attackDamage);
                    Debug.Log(enemy.name + " terkena serangan dari player!");
                }
            }
        }
    }
}
