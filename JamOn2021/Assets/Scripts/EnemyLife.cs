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
        print("auch");
        life -= l;
        if (life <= 0)
        {
            if (GetComponent<SpecialEnemyBehaviour>()) GameObject.FindGameObjectWithTag("InvokeBoss").GetComponent<InvokeBoss>().lightTorch();
            Destroy(gameObject);
            SoundManager.instance.enemyDeath();
        }
    }

    public float getLife()
    {
        return life;
    }
}
