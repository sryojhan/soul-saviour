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
        Destroy(gameObject);
    }

}
