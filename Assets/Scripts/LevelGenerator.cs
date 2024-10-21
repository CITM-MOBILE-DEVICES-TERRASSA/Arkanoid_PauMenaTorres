using System.Collections;
using System.Collections.Generic;
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
        if (instance == null)
        {
            if (SceneManager.GetActiveScene().name != "MainMenu")
            {
                instance = this;
                DontDestroyOnLoad(gameObject);
            }
        }
        else
        {
            Destroy(gameObject);
        }
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
        
    }

    public void RestartGame()
    {
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }
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

}



