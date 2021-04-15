using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldBehaviour : MonoBehaviour
{

    BossShield bossShield;
    GameObject ray;


    private int health = 100;

    private void Start()
    {
        bossShield = GameObject.FindObjectOfType<BossShield>();
    }

    public void setRay(GameObject r)
    {
        ray = r;
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<InitialSpeed>())
        {
            health -= 20;

            if (health <= 0)
            {
                bossShield.DestroyShield();
                Destroy(ray);
                Destroy(this.gameObject);
            }
        }
    }
}
