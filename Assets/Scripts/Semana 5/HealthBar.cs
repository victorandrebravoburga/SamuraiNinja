using UnityEngine;
using UnityEngine.UI;
public class HealthBar : MonoBehaviour
{
    // Referencia al componente Image que representa la barra de vida
    // [SerializeField] permite modificar la variable desde el Inspector
    [SerializeField] private Image barImage;

    // Método público para actualizar la barra de vida
    // Recibe la vida máxima y la vida actual como parámetros
    public void UpdateHealthBar(float maxHealth, float health)
    {
        // Calcula y asigna el porcentaje de vida restante a la barra
        barImage.fillAmount = health / maxHealth;
    }
}