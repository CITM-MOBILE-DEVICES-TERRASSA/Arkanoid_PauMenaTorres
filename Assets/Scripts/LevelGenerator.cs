using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;




public class LevelGenerator : MonoBehaviour
{
    public static LevelGenerator instance;
    public Vector2Int size = new Vector2Int(10, 5);
    public Vector2 offset = new Vector2(1.1f, 0.6f);
    public GameObject brickPrefab;

    public Gradient gradient = new Gradient
    {
        colorKeys = new GradientColorKey[]
        {
            new GradientColorKey(new Color(1f, 0f, 1f), 1f), // Magenta al final
            new GradientColorKey(new Color(0.5f, 0f, 0.5f), 0f) // Púrpura al inicio
        },
        alphaKeys = new GradientAlphaKey[]
        {
            new GradientAlphaKey(1f, 1f), // Totalmente opaco al final
            new GradientAlphaKey(1f, 0f) // Totalmente opaco al inicio
        }
    };

    private void Awake()
    {
        for (int i = 0; i < size.x; i++)
        {
            for (int j = 0; j < size.y; j++) 
            {
                GameObject newBrick = Instantiate(brickPrefab, transform);
                newBrick.transform.position = transform.position + new Vector3((float)((size.x - 1) * 0.5f - i) * offset.x, j * offset.y, 0); 
                newBrick.GetComponent<SpriteRenderer>().color = gradient.Evaluate((float)j / (size.y - 1));

                // Asignar resistencia adicional a los ladrillos de la tercera fila en adelante
                Brick brickScript = newBrick.GetComponent<Brick>();
                if (j >= 2) // Fila 3 y superiores
                {
                    brickScript.resistance = 2; // Por ejemplo, aumentar a 2 de resistencia
                }
            }
        }
    }

    void Start()
    {
        
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
        {
            Debug.Log("W or Up Arrow key was pressed.");
            PowerUpDestroyRandomColumn();
        }
    }

 public void PowerUpDestroyRandomColumn()
    {
        int randomColumn = Random.Range(0, size.x); // Selecciona columna aleatoria
        float targetXPosition = transform.position.x + (randomColumn - (size.x - 1) * 0.5f) * offset.x; // Calcula la posición en X de la columna seleccionada

        int bricksDestroyed = 0; // Contador de ladrillos destruidos por el power-up

        // Recorre todos los ladrillos hijos
        foreach (Transform child in transform)
        {
            Brick brick = child.GetComponent<Brick>();
            if (brick != null && child.gameObject.activeInHierarchy)
            {
                // Compara la posición X del ladrillo con la posición X de la columna seleccionada usando un margen de tolerancia
                if (Mathf.Abs(child.position.x - targetXPosition) < 0.01f)
                {
                    Destroy(child.gameObject); // Destruye el ladrillo
                    bricksDestroyed++; // Aumenta el contador de ladrillos destruidos
                }
            }
        }

        // Actualiza el brickCount en el BouncyBall
        BouncyBall bouncyBall = FindObjectOfType<BouncyBall>();
        if (bouncyBall != null)
        {
            bouncyBall.brickCount -= bricksDestroyed; // Restamos los ladrillos destruidos del contador
            if (bouncyBall.brickCount <= 0)
            {
                // Si no quedan ladrillos, activamos el panel de victoria
                bouncyBall.youWinPanel.SetActive(true);
                Time.timeScale = 0; // Detenemos el tiempo cuando ganamos
            }
        }
    }
}



