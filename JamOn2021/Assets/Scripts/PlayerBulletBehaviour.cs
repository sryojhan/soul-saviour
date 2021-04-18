using UnityEngine;

public class PlayerBulletBehaviour : MonoBehaviour
{
    [SerializeField] float bulletDamage;

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
        else if (collision.gameObject.GetComponent<SpecialEnemyBehaviour>())
        {
            collision.gameObject.GetComponent<EnemyLife>().attack(bulletDamage);
        }
        else if (collision.gameObject.GetComponent<BossBattle>())
        {
            collision.gameObject.GetComponent<BossBattle>().Hurt((int)bulletDamage);
        }
        else if (collision.gameObject.GetComponent<ShieldBehaviour>())
        {
            collision.gameObject.GetComponent<ShieldBehaviour>().Hurt(bulletDamage);
        }
        if (GetComponent<Renderer>().isVisible) SoundManager.instance.playerBulletSound();
        Destroy(gameObject);
    }

}
