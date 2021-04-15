using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossCircleAttack : MonoBehaviour
{
    [SerializeField] GameObject bulletPrefab;
    bool isAttacking = false;


    IEnumerator Attack(int numProjectiles, int lados)
    {
        isAttacking = true;
        double ang = 0;
        int count = 0;
        for (int i = 0; i < lados * numProjectiles; ++i)
        {
            ang += 360.0 / lados;
            if (ang == 360.0) ang = 0;
            Vector3 dir = (Vector2)(Quaternion.Euler(0, 0, (float)ang) * Vector2.right);
            GameObject bullet = Instantiate(bulletPrefab, transform.position + dir * 1.5f, Quaternion.identity);
            bullet.GetComponent<InitialSpeed>().setDirection(dir.normalized);

            count++;

            if (count == lados)
            {
                count = 0;
                yield return new WaitForSeconds(0.5f);
            }
        }
        isAttacking = false;
    }

    public void AttackPhase1()
    {
        StartCoroutine(Attack(2, 6));
    }

    public void AttackPhase2()
    {
        StartCoroutine(Attack(4, 9));
    }

    public void AttackPhase3()
    {
        StartCoroutine(Attack(6, 12));
    }

    public bool isActive()
    {
        return isAttacking;
    }
}
