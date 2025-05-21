using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    public float speed = 2f;
    public Transform[] patrolPoints;
    private int currentPointIndex = 0;

    private Animator animator;
    private Transform player;

    public float chaseRange = 5f;
    public float attackRange = 1.5f;
    private bool isChasing = false;

    public int health = 3;
    private bool isDead = false;

    void Start()
    {
        animator = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player")?.transform;
        animator.SetTrigger("Idle");
        Debug.Log("Enemy started with health: " + health);
    }

    void Update()
    {
        if (isDead) return;

        Debug.Log("Enemy health: " + health);

        if (player == null)
        {
            Debug.LogWarning("Player not found.");
            Patrol();
            return;
        }

        if (PlayerInRange() && !PlayerIsDead())
        {
            ChasePlayer();
        }
        else
        {
            Patrol();
        }
    }

    void Patrol()
    {
        if (patrolPoints.Length == 0) return;

        if (!isChasing)
        {
            Transform targetPoint = patrolPoints[currentPointIndex];
            transform.position = Vector2.MoveTowards(transform.position, targetPoint.position, speed * Time.deltaTime);

            if (Vector2.Distance(transform.position, targetPoint.position) < 0.1f)
            {
                currentPointIndex = (currentPointIndex + 1) % patrolPoints.Length;
            }

            FlipTowards(targetPoint.position.x);
            animator.SetTrigger("Flying");
        }
    }

    void ChasePlayer()
    {
        isChasing = true;
        float distanceToPlayer = Vector2.Distance(transform.position, player.position);

        if (distanceToPlayer > chaseRange)
        {
            isChasing = false;
            animator.SetTrigger("Idle");
            return;
        }

        if (distanceToPlayer > attackRange)
        {
            transform.position = Vector2.MoveTowards(transform.position, player.position, speed * 1.5f * Time.deltaTime);
            animator.SetTrigger("Flying");
        }
        else
        {
            animator.SetTrigger("Attack");
            PlayerState playerScript = player.GetComponent<PlayerState>();
            if (playerScript != null && !playerScript.isDead)
            {
                playerScript.TakeDamage(1);
                Debug.Log("Enemy attacking player.");
            }
        }

        FlipTowards(player.position.x);
    }

    public void TakeDamage(int damage)
    {
        if (isDead) return;

        health -= damage;
        animator.SetTrigger("Hurt");
        Debug.Log("Enemy took damage: " + damage);

        if (health <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        isDead = true;
        animator.SetTrigger("Death");
        Debug.Log("Enemy has died.");
        Destroy(gameObject, 1f);
    }

    bool PlayerInRange()
    {
        return Vector2.Distance(transform.position, player.position) <= chaseRange;
    }

    bool PlayerIsDead()
    {
        if (player == null) return true;
        PlayerState playerScript = player.GetComponent<PlayerState>();
        return playerScript == null || playerScript.health <= 0;
    }

    void FlipTowards(float targetX)
    {
        Vector3 scale = transform.localScale;
        scale.x = (targetX > transform.position.x) ? Mathf.Abs(scale.x) : -Mathf.Abs(scale.x);
        transform.localScale = scale;
    }
}
