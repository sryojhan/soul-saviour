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
        if (enemy.GetComponent<RangedEnemyBehaviour>() != null)
        {
            enemy.GetComponent<EnemyLife>().attack(bulletDamage);

            if (enemy.GetComponent<EnemyLife>().alive() && !enemy.GetComponent<RangedEnemyBehaviour>().isActive())
            {
                enemy.GetComponent<RangedEnemyBehaviour>().setActive(true);
            }
        }
        else if(collision.gameObject.GetComponent<MeleeEnemy>() != null)
        {
            enemy.GetComponent<EnemyLife>().attack(bulletDamage);

            if (enemy.GetComponent<EnemyLife>().alive() && !enemy.GetComponent<MeleeEnemy>().isActive())
            {
                enemy.GetComponent<MeleeEnemy>().setActive(true);
            }
        }
        Destroy(gameObject);
    }

}
