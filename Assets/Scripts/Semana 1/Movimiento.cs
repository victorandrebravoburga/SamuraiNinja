using UnityEngine;

public class Movimiento : MonoBehaviour
{

    public Transform robot;
    public float speed = 1f;

    void Update()
    {
        transform.position = Vector2.MoveTowards
        (transform.position, robot.position, speed * Time.deltaTime);
    }
}
