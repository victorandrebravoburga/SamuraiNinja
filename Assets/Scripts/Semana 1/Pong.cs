using UnityEngine;

public class Pong : MonoBehaviour
{
    public float speed = 3f;
    private Vector2 direction = Vector2.right;

    void Update()
    {
        transform.Translate(direction * speed * Time.deltaTime);
    }

    void OionEnter2D(Collision2D collision)
    {
        direction = Vector2.Reflect(direction, collision.contacts[0].normal);
    }
}