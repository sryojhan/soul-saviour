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
    [SerializeField] AudioClip sound;

    private Vector2 attackPos;


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
                Vector2 mouseWorldPoint = new Vector2(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y);
                Vector2 playerPos2D = new Vector2(playerPos.position.x, playerPos.position.y);
                Vector2 circlePosDir = ((mouseWorldPoint - playerPos2D).normalized) * (attackDistanceFromPlayer.position - playerPos.position).magnitude;

                attackPos = (playerPos2D + circlePosDir);
                Collider2D[] enemiesToDamage = Physics2D.OverlapCircleAll(attackPos, attackRange, whatIsEnemies);
                for (int i = 0; i < enemiesToDamage.Length; i++)
                {
                    enemiesToDamage[i].GetComponent<EnemyLife>().attack(damage);
                }

                GetComponent<AudioSource>().PlayOneShot(sound);

            }

            timeHeld = 0;
        }
    }
}
