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
    [SerializeField] Sprite sweepSpriteDerecha;
    [SerializeField] Sprite sweepSpriteIzquierda;
    [SerializeField] Sprite sweepSpriteArriba;
    [SerializeField] Sprite sweepSpriteAbajo;


    SpriteRenderer sp;
    Sprite originalSprite;
    private Vector2 attackPos;

    private void Start()
    {
        sp = GetComponent<SpriteRenderer>();
        originalSprite = sp.sprite;
    }
    //void OnDrawGizmos()
    //{
    //    // Draw a yellow sphere at the transform's position
    //    Gizmos.color = Color.yellow;
    //    Gizmos.DrawSphere(attackPos, attackRange);
    //}


    public bool isHeldEnough()
    {
        return timeHeld >= holdMouseTime;
    }

    // Update is called once per frame
    void manageSprite(Vector2 mouseWorldPoint)
    {
        if (mouseWorldPoint.y < transform.position.y)
        {
            if (mouseWorldPoint.x > mouseWorldPoint.y)
            {
                if (mouseWorldPoint.x > transform.position.x) sp.sprite = sweepSpriteDerecha;
                else sp.sprite = sweepSpriteIzquierda;
            }
            else sp.sprite = sweepSpriteAbajo;
        }
        else
        {
            if (mouseWorldPoint.x > mouseWorldPoint.y)
            {
                if (mouseWorldPoint.x > transform.position.x) sp.sprite = sweepSpriteDerecha;
                else sp.sprite = sweepSpriteIzquierda;
            }
            else sp.sprite = sweepSpriteArriba;
        }
        sp.flipX = false;
        sp.flipY = false;
    }

    void resetSprite()
    {
        sp.sprite = originalSprite;
    }
    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            timeHeld += Time.deltaTime;
        }

        if (Input.GetMouseButtonUp(0))
        {
            if (isHeldEnough())
            {
                SoundManager.instance.sweepSound();
                Vector2 mouseWorldPoint = new Vector2(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y);
                Vector2 playerPos2D = new Vector2(playerPos.position.x, playerPos.position.y);
                Vector2 circlePosDir = ((mouseWorldPoint - playerPos2D).normalized) * (attackDistanceFromPlayer.position - playerPos.position).magnitude;

                attackPos = (playerPos2D + circlePosDir);
                Collider2D[] enemiesToDamage = Physics2D.OverlapCircleAll(attackPos, attackRange, whatIsEnemies);
                for (int i = 0; i < enemiesToDamage.Length; i++)
                {
                    if (enemiesToDamage[i].gameObject.GetComponent<BossBattle>())
                    {
                        enemiesToDamage[i].gameObject.GetComponent<BossBattle>().Hurt((int)damage);
                    }
                    else if (enemiesToDamage[i].gameObject.GetComponent<ShieldBehaviour>())
                    {
                        enemiesToDamage[i].gameObject.GetComponent<ShieldBehaviour>().Hurt(damage);
                    }
                    else
                    {
                        enemiesToDamage[i].gameObject.GetComponent<EnemyLife>().attack(damage);
                    }
                }
                manageSprite(mouseWorldPoint);
                Invoke("resetSprite", 0.3f);
            }

            timeHeld = 0;
        }
    }
}
