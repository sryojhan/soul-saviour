using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossSprintAttack : MonoBehaviour
{
    [SerializeField] Transform playerPosition;
    [SerializeField] BossBattle battle;

    [SerializeField] float speed;
    [SerializeField] float distance;

    [SerializeField] Rigidbody2D rb;

    private Vector2 playerDirection;
    private Vector3 startPos;
    bool isSprinting = false;


    public void Attack()
    {
        playerDirection = (playerPosition.position - transform.position).normalized;
        startPos = transform.position;
        rb.AddForce(playerDirection * speed, ForceMode2D.Impulse);

        isSprinting = true;
    }

    private void Update()
    {
        if (isSprinting)
        {
            if ((transform.position - startPos).magnitude >= distance)
            {
                rb.velocity = Vector2.zero;
                isSprinting = false;
                battle.StopAttack();
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (isSprinting)
        {
            if (collision.gameObject.GetComponent<PlayerSweepAttack>())
            {
                rb.velocity = Vector2.zero;
                isSprinting = false;
                battle.StopAttack();
            }
            else if (collision.gameObject.GetComponent<RangedEnemyBehaviour>())
            {
                Destroy(collision.gameObject);
            }
        }
    }
}
