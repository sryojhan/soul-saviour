using UnityEngine;
using UnityEngine.UI;

public class TimerBehaviour : MonoBehaviour
{
    static public TimerBehaviour instance;
    private float startTime;
    [SerializeField] Text timerText;
    [SerializeField] Text finnishText;
    [SerializeField] GameObject panel;
    private bool finnish;
    

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
    void Start()
    {
        panel.SetActive(false);
        startTime = Time.time;
        finnish = false;
    }
    
    void Update()
    {
        if (finnish) return;

        float t = Time.time - startTime;
        string minutes = ((int) t/60).ToString();
        string seconds = (t % 60).ToString("f0");

        timerText.text = minutes + ":" + seconds;
    }
    public void Finnish()
    {
        finnish = true;
        finnishText.text = timerText.text;
        panel.SetActive(true);
        GameObject [] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        for(int i = 0; i< enemies.Length; i++)
        {
            Destroy(enemies[i]);
        }
    }
}
