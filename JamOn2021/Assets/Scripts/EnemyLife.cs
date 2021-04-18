using System.Collections;
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

        if (life <= 0)
        {
            if (GetComponent<SpecialEnemyBehaviour>()) GameObject.FindGameObjectWithTag("InvokeBoss").GetComponent<InvokeBoss>().lightTorch();
            Destroy(gameObject);
            SoundManager.instance.enemyDeath();
        }
       StartCoroutine(tick());
    }

    IEnumerator tick()
    {
        for (int i = 0; i < 6; ++i)
        {
            GetComponent<SpriteRenderer>().enabled = !GetComponent<SpriteRenderer>().enabled;
            yield return new WaitForSeconds(0.065f);
        }
        StopCoroutine(tick());
    }

    public float getLife()
    {
        return life;
    }
}
