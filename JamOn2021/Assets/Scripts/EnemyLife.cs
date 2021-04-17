using UnityEngine;

public class EnemyLife : MonoBehaviour
{
    [SerializeField] float life;
    void Start()
    {
        
    }

    void Update()
    {
        
    }
    public bool alive()
    {
        return life > 0;
    }
    public void attack(float l)
    {
        life -= l;
        if (life <= 0) Destroy(gameObject);
    }
}
