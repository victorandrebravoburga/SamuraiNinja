using UnityEngine;

public class Sword : Weapon
{
    public float attackRange = 1.5f; // Rango del ataque cercano
    public LayerMask enemyLayer; // Capas que serán atacadas (enemigos)
    public int damage = 10; // Daño infligido por la espada

    public override void Use()
    {
        AttackClose();
    }

    private void AttackClose()
    {
        // Detectar enemigos en el rango de ataque
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(transform.position, attackRange, enemyLayer);

        // Infligir daño a todos los enemigos detectados
        foreach (Collider2D enemy in hitEnemies)
        {
            // Usar la lógica de daño del enemigo (deberás crear un script Enemy, por ejemplo)
            enemy.GetComponent<Enemy>()?.TakeDamage(damage);
            Debug.Log($"Golpeaste a {enemy.name} con {weaponName} causando {damage} de daño.");
        }

        // Animación o efecto visual del ataque
        Debug.Log($"{weaponName} realizó un ataque cercano.");
    }

    // Gizmo para visualizar el rango de ataque en el editor
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}