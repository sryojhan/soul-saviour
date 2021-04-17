using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        PlayerHealth p = collision.gameObject.GetComponent<PlayerHealth>();
        if (p != null)
        {
            p.looseLife();
        }

        Destroy(this.gameObject);
    }
}
