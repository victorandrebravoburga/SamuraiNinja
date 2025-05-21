using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int health = 50;

    public void TakeDamage(int damage)
    {
        health -= damage;
        Debug.Log($"{name} recibió {damage} de daño. Vida restante: {health}");

        if (health <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        Debug.Log($"{name} fue derrotado.");
        Destroy(gameObject); // Elimina al enemigo
    }
}