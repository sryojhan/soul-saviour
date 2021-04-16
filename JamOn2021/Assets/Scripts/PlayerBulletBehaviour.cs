using UnityEngine;

public class PlayerBulletBehaviour : MonoBehaviour
{
    [SerializeField] float bulletDamage;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        GameObject enemy = collision.gameObject;
        if (enemy.GetComponent<RangedEnemyBehaviour>() != null)
        {
            collision.gameObject.GetComponent<EnemyLife>().attack(bulletDamage);
            Destroy(gameObject);
        }
        else if (enemy.GetComponent<SpecialEnemyBehaviour>() != null)
        {
            Destroy(gameObject);
        }
        else Destroy(gameObject);
    }

}
