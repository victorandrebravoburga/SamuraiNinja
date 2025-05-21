using UnityEngine;

public class Bullet : MonoBehaviour
{
    public int damage = 1;
    public float lifeTime = 2f;

    void Start()
    {
        Destroy(gameObject, lifeTime); // Destruir la bala después de 2 segundos
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        EnemyScript enemy = collision.gameObject.GetComponent<EnemyScript>();
        if (enemy != null)
        {
            enemy.TakeDamage(damage);
            Debug.Log("Bala impactó al enemigo.");
        }

        Destroy(gameObject); // Destruir la bala al impactar algo
    }
}