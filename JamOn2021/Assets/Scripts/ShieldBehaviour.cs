using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldBehaviour : MonoBehaviour
{

    BossShield bossShield;
    GameObject ray;


    private float health = 5;


    public void setRay(GameObject r)
    {
        ray = r;
    }

    public void setBossShield(BossShield bS)
    {
        bossShield = bS;
    }


    public void Hurt(float dmg )
    {
        health -= dmg;

        if (health <= 0)
        {
            Destroy(ray.gameObject);
            Destroy(gameObject);
            bossShield.DestroyShield();
        }

    }
}
