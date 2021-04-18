using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum Escenas
{
    Inicial = 0,
    Menu,
    Juego
}

public class GameManager : MonoBehaviour
{
    [SerializeField] GameObject canvas;
    static public GameManager instance;

    private void Awake()
    {
        if (instance == null) //si no hay instancia
        {
            instance = this; //la creamos
            DontDestroyOnLoad(gameObject); //evitamos que se destruya entre escenas
        }
        else //en caso contrario
        {
            Destroy(gameObject); //destruimos la instancia
        }
    }
    public void changeScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);

        Time.timeScale = 1;
    }
    public void exit()
    {
        Application.Quit();
    }
    public void backToMenu()
    {
        SceneManager.LoadScene(1);
    }


}
