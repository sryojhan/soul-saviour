using UnityEngine;

public class PlayerBulletBehaviour : MonoBehaviour
{
    [SerializeField] float bulletDamage;
    void Start()
    {

    }

    void Update()
    {

    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        GameObject enemy = collision.gameObject;
        if (enemy.GetComponent<RangedEnemyBehaviour>() != null || collision.gameObject.GetComponent<MeleeEnemy>())
        {
            enemy.GetComponent<EnemyLife>().attack(bulletDamage);
        }
        if (collision.gameObject.GetComponent<BossBattle>())
        {
            collision.gameObject.GetComponent<BossBattle>().Hurt((int)bulletDamage);
        }
        if (collision.gameObject.GetComponent<ShieldBehaviour>())
        {
            collision.gameObject.GetComponent<ShieldBehaviour>().Hurt(bulletDamage);
        }
        Destroy(gameObject);
    }

}
