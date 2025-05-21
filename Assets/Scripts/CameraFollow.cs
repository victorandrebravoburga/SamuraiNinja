using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player; // Jugador a seguir
    public Transform canvas; // Canvas al que la cámara enfocará
    public float smoothSpeed = 0.125f;
    public Vector3 offset;

    private bool focusOnCanvas = false; // Controlar si la cámara enfoca el Canvas

    private void LateUpdate()
    {
        if (focusOnCanvas && canvas != null)
        {
            // Enfocar el Canvas
            Vector3 desiredPosition = canvas.position + offset;
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
            transform.position = smoothedPosition;
        }
        else if (player != null)
        {
            // Seguir al jugador
            Vector3 desiredPosition = player.position + offset;
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
            transform.position = smoothedPosition;
        }
    }

    // Método público para alternar entre seguir al jugador o al Canvas
    public void FocusOnCanvas(bool focus)
    {
        focusOnCanvas = focus;
    }
}