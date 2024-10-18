using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ReturnMenu()
    {
        // Cargar la escena del menú
        SceneManager.LoadScene(0);
    }

    public void PlayLevel1()
    {
        SceneManager.LoadScene(1);
    }

    public void PlayLevel2()
    {
        SceneManager.LoadScene(2);
    }

    public void GoLastLevel()
    {
        int lastLevel = SceneManager.sceneCountInBuildSettings - 1;
        SceneManager.LoadScene(lastLevel);
    }
    
    public void RestartGame()
    {
        GameManager.instance.RestartGame();
    }
}
