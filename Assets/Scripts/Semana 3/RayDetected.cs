using UnityEngine;

public class RayDetected : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

void Update()
{
    RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.right, 5f);
    if (hit.collider != null)
    {
        Debug.DrawLine(transform.position, hit.point, Color.red);
        Debug.Log("Colisión con: " + hit.collider.name);
    }
}

}
