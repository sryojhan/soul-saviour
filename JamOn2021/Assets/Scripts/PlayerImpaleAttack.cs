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
    [SerializeField] float cadence;

    [SerializeField] AudioClip sound;

    RaycastHit2D enemyHit;
    Vector2 inipos;
    Vector2 dir;

    float lastAttack = 0;

    //void OnDrawGizmos()
    //{
    //    // Draw a yellow sphere at the transform's position
    //    Gizmos.color = Color.yellow;
    //    Gizmos.DrawRay(inipos, dir * length);
    //}

    private void Start()
    {
        playerPos = this.transform;
    }

    // Update is called once per frame
    void Update()
    {
        lastAttack += Time.deltaTime;
        if (Input.GetMouseButtonUp(0) && !sweepComponent.isHeldEnough() && lastAttack >= cadence)
        {
            lastAttack = 0;
            Vector2 mouseWorldPoint = new Vector2(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y);
            Vector2 playerPos2D = new Vector2(playerPos.position.x, playerPos.position.y);

            dir = (mouseWorldPoint - playerPos2D).normalized;

            float angle = Vector2.Angle(new Vector2(1, 0), dir);

            inipos = playerPos2D + (dir * attackStartPointOffset);
            enemyHit = Physics2D.BoxCast(playerPos2D + (dir * attackStartPointOffset), new Vector2(0.1f, width), angle, dir, length, whatIsEnemies);

            GetComponent<AudioSource>().PlayOneShot(sound);

            if (enemyHit.collider != null)
            {
                enemyHit.collider.GetComponent<EnemyLife>().attack(damage);
                if (enemyHit.collider.gameObject.GetComponent<BossBattle>())
                {
                    enemyHit.collider.gameObject.GetComponent<BossBattle>().Hurt((int)damage);
                }
                else if (enemyHit.collider.gameObject.GetComponent<ShieldBehaviour>())
                {
                    enemyHit.collider.gameObject.GetComponent<ShieldBehaviour>().Hurt(damage);
                }
                else
                {
                    enemyHit.collider.gameObject.GetComponent<EnemyLife>().attack(damage);
                }
            }

        }
    }
}
