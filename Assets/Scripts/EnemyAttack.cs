using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    public float speed = 3f;
    public float attackRange = 5.2f;
    public float attackCooldown = 2f;
    public int damage = 10;

    private Transform player;
    private float lastAttackTime;
    private Animator animator;

    private bool isPlayerInRange = false;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player")?.transform;
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (!isPlayerInRange || player == null) return;

        float distance = Vector2.Distance(transform.position, player.position);

        if (distance > attackRange)
        {
            transform.position = Vector2.MoveTowards(transform.position, player.position, speed * Time.deltaTime);
        }
        else
        {
            if (Time.time - lastAttackTime >= attackCooldown)
            {
                if (animator != null)
                    animator.SetTrigger("attack");

                var health = player.GetComponent<PlayerHealth>();
                if (health != null)
                {
                    health.TakeDamage(damage);
                    Debug.Log(name + " menyerang player!");
                }

                lastAttackTime = Time.time;
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInRange = true;
            Debug.Log(name + " mendeteksi player.");
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInRange = false;
            Debug.Log(name + " kehilangan player.");
        }
    }
}
