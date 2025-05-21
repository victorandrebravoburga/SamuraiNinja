using UnityEngine;

public class MovimientoRB : MonoBehaviour
{
    public float speed = 5f;

    private Rigidbody2D rb;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");
        Vector2 direction = new Vector2(x, y);
        rb.AddForce(direction * speed);
    }
}
