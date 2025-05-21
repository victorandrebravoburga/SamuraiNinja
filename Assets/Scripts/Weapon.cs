using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    public string weaponName;
    public abstract void Use(); // Definir el uso del arma (abstracto para personalizar en cada arma)
}