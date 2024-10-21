using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{   
    public static MenuManager instance;
    private static int previousSceneIndex = -1;
    private static int currentSceneIndex;

    // Método que se llama al inicio para almacenar la escena actual
    private void Start()
    {
        // Guarda el índice de la escena actual al iniciar el juego
        currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        Debug.Log("Escena actual al inicio: " + currentSceneIndex);
    }

    // Función para cargar una nueva escena y almacenar la actual
    public void GoToScene(int sceneIndex)
    {
        // Almacena la escena actual como la anterior antes de cambiar de escena
        previousSceneIndex = currentSceneIndex;
        currentSceneIndex = sceneIndex;

        Debug.Log("Cambiando a la escena: " + sceneIndex + ". Escena anterior: " + previousSceneIndex);

        // Asegúrate de que el tiempo esté en escala normal
        Time.timeScale = 1;
        
        // Carga la nueva escena
        SceneManager.LoadScene(sceneIndex);
    }

    // Función hardcodeada para cargar la última escena jugada
    public void GoLastScene()
    {
        // Si previousSceneIndex es -1, significa que no hay una escena anterior guardada.
        if (previousSceneIndex == -1)
        {
            Debug.LogWarning("No hay escena anterior almacenada para cargar.");
            return;
        }

        // Dependiendo del valor de previousSceneIndex, carga la escena anterior
        if (previousSceneIndex == 0)
        {
            SceneManager.LoadScene(0);  // Cargar escena 0
        }
        else if (previousSceneIndex == 1)
        {
            SceneManager.LoadScene(1);  // Cargar escena 1
        }
        else if (previousSceneIndex == 2)
        {
            SceneManager.LoadScene(2);  // Cargar escena 2
        }

        // Actualiza los índices después de volver a la escena anterior
        int tempSceneIndex = currentSceneIndex;
        currentSceneIndex = previousSceneIndex;
        previousSceneIndex = tempSceneIndex;

        Debug.Log("Volviendo a la escena anterior: " + currentSceneIndex + ". Escena actual: " + currentSceneIndex + ", Escena anterior ahora: " + previousSceneIndex);
    }

    public void RestartGame()
    {
       GoToScene(SceneManager.GetActiveScene().buildIndex + 1);
       GameManager.instance.RestartGame();
    }

    public void SaveGame()
    {
        GoToScene(0);
        GameManager.instance.SaveGame();
    }

    public void LoadGame()
    {
        GoLastScene();
        GameManager.instance.LoadGame();
    }
}
