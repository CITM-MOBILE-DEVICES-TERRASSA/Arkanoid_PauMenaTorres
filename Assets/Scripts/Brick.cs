using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brick : MonoBehaviour
{
    public int resistance = 1; // La resistencia del ladrillo
    private SpriteRenderer spriteRenderer; // Referencia al SpriteRenderer

    // En el método Awake, obtenemos la referencia al componente SpriteRenderer
    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>(); // Asigna el SpriteRenderer automáticamente
    }

    public void TakeDamage()
    {
        resistance--; // Reducir la resistencia
        if (resistance <= 0)
        {
            Destroy(gameObject); // Destruir el ladrillo si la resistencia es 0
        }
        else
        {
            // Cambiar el color para reflejar la pérdida de resistencia (puedes modificar la lógica)
            Color newColor = spriteRenderer.color;
            newColor.a = Mathf.Clamp01((float)resistance / 3); // Ajusta la transparencia en función de la resistencia
            spriteRenderer.color = newColor;
        }
    }
}
