using UnityEngine;

public class EnemyLife : MonoBehaviour
{
    [SerializeField] float life;

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
