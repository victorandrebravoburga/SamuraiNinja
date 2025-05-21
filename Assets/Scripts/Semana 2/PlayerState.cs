using UnityEngine;
using System.Collections.Generic;

public class PlayerState : MonoBehaviour
{
    public float speed = 3f;
    public float jumpForce = 6f;
    public int health = 5;

    private Rigidbody2D rb;
    private Animator animator;
    private SpriteRenderer spriteRenderer;
    private bool isGrounded = true;
    internal bool isDead = false;

    public Transform attackPoint;
    public float attackRange = 1f;
    public LayerMask enemyLayers;

    // Lista de armas y arma activa
    public List<Weapon> weapons; // Las armas se asignan desde el inspector
    private int currentWeaponIndex = 0;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        if (weapons.Count > 0)
            EquipWeapon(weapons[currentWeaponIndex]); // Equipar el arma inicial
    }

    void Update()
    {
        if (isDead) return;

        Movement();
        ManageWeapon();
    }

    void Movement()
    {
        float move = Input.GetAxisRaw("Horizontal");
        rb.linearVelocity = new Vector2(move * speed, rb.linearVelocity.y);
        animator.SetFloat("Speed", Mathf.Abs(move));

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
            isGrounded = false;
            animator.SetBool("isJumping", true);
        }
    }

    void ManageWeapon()
    {
        if (weapons.Count == 0) return;

        // Control del ataque (solo usa el arma actualmente equipada)
        if (Input.GetKeyDown(KeyCode.J))
        {
            Weapon activeWeapon = weapons[currentWeaponIndex];
            activeWeapon.Use(); // Usar el arma actual
            animator.SetTrigger("Attack");
        }

        // Cambiar de arma con "Q"
        if (Input.GetKeyDown(KeyCode.Q))
        {
            currentWeaponIndex = (currentWeaponIndex + 1) % weapons.Count; // Ciclar a la siguiente arma
            EquipWeapon(weapons[currentWeaponIndex]);
        }
    }

    void EquipWeapon(Weapon newWeapon)
    {
        foreach (Weapon weapon in weapons)
            weapon.gameObject.SetActive(false); // Desactivar todas las armas

        newWeapon.gameObject.SetActive(true); // Activar el arma seleccionada
        Debug.Log($"Equipada arma: {newWeapon.weaponName}");
    }

    public void TakeDamage(int damage)
    {
        if (isDead) return;

        health -= damage;
        Debug.Log($"Player took {damage} damage. Health remaining: {health}");

        if (health <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        isDead = true;
        animator.SetTrigger("Death");
        Debug.Log("Player has died");
        Destroy(gameObject, 1f);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
            animator.SetBool("isJumping", false);
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (attackPoint == null) return;

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}