using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBattle : MonoBehaviour
{
    [SerializeField] BossSpawnEnemies spawnEnemies;
    [SerializeField] BossCircleAttack circleAttack;
    [SerializeField] BossSprintAttack sprintAttack;
    [SerializeField] BossShield shield;

    [SerializeField] float basicAttackCadence;
    [SerializeField] float bulletSpeed;
    [SerializeField] GameObject bulletPrefab;

    float lastBasicAttack = 0;

    [SerializeField] int restoreLifePerSecondWithShield = 5;

    GameObject player;

    enum Phase { PHASE1, PHASE2, PHASE3 };

    bool attacking = false;
    bool isShielded = false;
    Phase phase;

    float shieldRecover = 0;

    int health = 400;

    void Start()
    {
        player = GameObject.FindWithTag("Player");
        phase = Phase.PHASE1;
        Battle();
    }

    public void StopAttack() { attacking = false; }
    public void StopShield() { isShielded = false; shieldRecover = 0; }

    public void Hurt(int hp)
    {
        health -= hp;

        if (health <= 50) phase = Phase.PHASE2;
        else if (health <= 20) phase = Phase.PHASE3;
    }

    void Battle()
    {
        int random = Random.Range(0, 100);

        if (random <= 30)
        {
            int numEnemies = Random.Range((int)phase + 1, 4);
            spawnEnemies.Spawn(numEnemies);
        }
        else if (random <= 60) sprintAttack.Attack();
        else if (random <= 65)
        {
            shield.Spawn((int)phase + 1);
            isShielded = true;
        }
        else
        {
            if (phase == Phase.PHASE1)
                circleAttack.AttackPhase1();
            if (phase == Phase.PHASE2)
                circleAttack.AttackPhase2();
            if (phase == Phase.PHASE3)
                circleAttack.AttackPhase3();
        }

        attacking = true;
    }

    void BasicAttack()
    {
        Vector2 playerPos = new Vector2(player.transform.position.x, player.transform.position.y);

        Rigidbody2D playerRb = player.GetComponent<Rigidbody2D>();

        float playerAngle = Vector2.Angle(new Vector2(transform.position.x, transform.position.y) - playerPos, playerRb.velocity);

        float auxAngle1 = Mathf.Asin(Mathf.Sin(playerAngle * Mathf.Deg2Rad) * (playerRb.velocity.magnitude / bulletSpeed));

        float auxAngle2 = Vector2.Angle(Vector2.right, playerPos - new Vector2(transform.position.x, transform.position.y));

        float shootAngle = auxAngle2 - auxAngle1 * Mathf.Rad2Deg;

        if (player.transform.position.y < transform.position.y) shootAngle = -shootAngle;

        GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);

        //playerPos += Vector3.Lerp(Vector3.zero, playerRb.velocity, interpolation);
        bullet.GetComponent<InitialSpeed>().setDirection((Quaternion.Euler(0, 0, shootAngle) * Vector2.right).normalized);
    }

    private void Update()
    {
        if (attacking)
        {
            lastBasicAttack += Time.deltaTime;

            if (lastBasicAttack >= basicAttackCadence)
            {
                BasicAttack();
                lastBasicAttack = 0;
            }

            if (isShielded)
            {
                shieldRecover += Time.deltaTime;
                if (shieldRecover >= 1)
                {
                    health += restoreLifePerSecondWithShield;
                    shieldRecover = 0;
                }
            }

        }
        else
        {
            Battle();
        }
    }
}
