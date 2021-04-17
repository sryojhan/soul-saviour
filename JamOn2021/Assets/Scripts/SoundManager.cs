using UnityEngine;

public class SoundManager : MonoBehaviour
{
    static public SoundManager instance;
    enum clipsNames { PlayerBullet, Dash, Impale, Sweep, PlayerShot};
    [SerializeField] AudioClip[] clips;
    private AudioSource audioSource;

    void Awake()
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
    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }
    public void playerBulletSound()
    {
        audioSource.PlayOneShot(clips[(int)clipsNames.PlayerBullet]);
    }
    public void dashSound()
    {
        audioSource.PlayOneShot(clips[(int)clipsNames.Dash]);
    }
    public void impaleSound()
    {
        audioSource.PlayOneShot(clips[(int)clipsNames.Impale]);
    }
    public void sweepSound()
    {
        audioSource.PlayOneShot(clips[(int)clipsNames.Sweep]);
    }
    public void playerShot()
    {
        audioSource.PlayOneShot(clips[(int)clipsNames.PlayerShot]);
    }
}
