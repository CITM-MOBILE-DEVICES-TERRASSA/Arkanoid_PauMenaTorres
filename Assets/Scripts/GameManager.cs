using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public int score = 0;
    public int lives = 3;
    public int maxScore = 0; // Puntaje máximo

    void Awake()
    {
        // Implementamos el patrón Singleton para que solo exista una instancia del GameManager
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // No destruir el GameManager al cambiar de escena
        }
        else
        {
            Destroy(gameObject); // Si ya hay un GameManager, destruimos el duplicado
        }
    }

    // Método para actualizar el puntaje máximo si es necesario
    public void UpdateMaxScore()
    {
        if (score > maxScore)
        {
            maxScore = score;
        }
    }

    public void RestartGame()
    {
        score = 0;
        lives = 3;
    }
}
