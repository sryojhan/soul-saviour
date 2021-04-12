using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSweepAttack : MonoBehaviour
{

    private float timeHeld = 0;
    [SerializeField] float holdMouseTime;
    [SerializeField] Transform attackDistanceFromPlayer;
    [SerializeField] Transform playerPos;
    [SerializeField] float attackRange;
    [SerializeField] LayerMask whatIsEnemies;
    [SerializeField] float damage;

    public float lookRadius = 10f;
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, lookRadius);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            timeHeld += Time.deltaTime;
        }

        if (Input.GetMouseButtonUp(0))
        {
            if (timeHeld >= holdMouseTime)
            {
                Vector3 mouseWorldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                Vector3 circlePosDir = (mouseWorldPoint - playerPos.position).normalized * (attackDistanceFromPlayer.position - playerPos.position).magnitude;
                Vector2 attackPos = playerPos.position + circlePosDir;
                Collider2D[] enemiesToDamage = Physics2D.OverlapCircleAll(attackPos, attackRange, whatIsEnemies);
 
                for (int i = 0; i < enemiesToDamage.Length; i++)
                {
                    
                }
            }

            timeHeld = 0;
        }

    }
}
