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
        if (collision.gameObject.GetComponent<RangedEnemyBehaviour>() != null || collision.gameObject.GetComponent<MeleeEnemy>())
        {
            collision.gameObject.GetComponent<EnemyLife>().attack(bulletDamage);
            Destroy(gameObject);
        }
    }
}
