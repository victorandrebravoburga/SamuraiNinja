using UnityEngine;

public class MovimientoLerp : MonoBehaviour
{

public Transform objetive;
public float speed = 3f;

void Update ()

{
    transform.position = Vector2.Lerp
    (transform.position, objetive.position, speed * Time.deltaTime);
}
}
