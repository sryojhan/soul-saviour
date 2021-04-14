using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerImpaleAttack : MonoBehaviour
{
    [SerializeField] float length;
    [SerializeField] float width;
    [SerializeField] float attackStartPointOffset;
    [SerializeField] Transform playerPos;
    [SerializeField] PlayerSweepAttack sweepComponent;
    [SerializeField] LayerMask whatIsEnemies;
    [SerializeField] float damage;
    RaycastHit2D enemyHit;
    Vector2 inipos;
    Vector2 dir;

    void OnDrawGizmos()
    {
        // Draw a yellow sphere at the transform's position
        Gizmos.color = Color.yellow;
        Gizmos.DrawRay(inipos, dir * length);
    }


    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonUp(0) && !sweepComponent.isHeldEnough())
        {
            Vector2 mouseWorldPoint = new Vector2(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y);
            Vector2 playerPos2D = new Vector2(playerPos.position.x, playerPos.position.y);

            dir = (mouseWorldPoint - playerPos2D).normalized;

            float angle = Vector2.Angle(new Vector2(1, 0), dir);

            inipos = playerPos2D + (dir * attackStartPointOffset);
            enemyHit = Physics2D.BoxCast(playerPos2D + (dir * attackStartPointOffset), new Vector2(0.1f, width), angle, dir, length, whatIsEnemies);

            if (enemyHit.collider != null)
            {
                //dañar
            }

        }
    }
}
