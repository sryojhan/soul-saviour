using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.UI;

public class BossBattle : MonoBehaviour
{
    [SerializeField] BossSpawnEnemies spawnEnemies;
    [SerializeField] BossCircleAttack circleAttack;
    [SerializeField] BossSprintAttack sprintAttack;
    [SerializeField] BossShield shield;

    [SerializeField] float basicAttackCadence;
    [SerializeField] float bulletSpeed;
    [SerializeField] GameObject bulletPrefab;

    private HealthBarBehaviour healthBar;
    float lastBasicAttack = 0;

    [SerializeField] int restoreLifePerSecondWithShield = 5;

    GameObject player;

    enum Phase { PHASE1, PHASE2, PHASE3 };

    bool attacking = false;
    bool isShielded = false;
    Phase phase;

    float shieldRecover = 0;

    int health = 170;
    public void StartBattle()
    {
        healthBar = GetComponent<HealthBarBehaviour>();
        player = GameObject.FindWithTag("Player");
        phase = Phase.PHASE1;
        Battle();

    }

    public void StopAttack() { attacking = false; Battle(); }
    public void StopShield() { isShielded = false; shieldRecover = 0; }

    public float getHealth()
    {
        return health;
    }
    public void Hurt(int hp)
    {
        if (!isShielded)
        {
            health -= hp;
            healthBar.setSliderValue(health);

            if (health <= 50) phase = Phase.PHASE2;
            else if (health <= 20) phase = Phase.PHASE3;

            if (health <= 0)
            {
                TimerBehaviour.instance.Finnish();
                Time.timeScale = 0;
                Destroy(gameObject);
            }
        }
    }

    void Battle()
    {
        int random = Random.Range(0, 100);

        if (random <= 30)
        {
            int numEnemies = Random.Range((int)phase + 1, 4);
            spawnEnemies.Spawn(numEnemies);
        }
        else if (random <= 60)
        {
            sprintAttack.Attack();
        }
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

        Vector3 dir = (Quaternion.Euler(0, 0, shootAngle) * Vector2.right).normalized;

        GameObject bullet = Instantiate(bulletPrefab, transform.position + dir * 1.5f, Quaternion.identity);

        // playerPos += Vector3.Lerp(Vector3.zero, playerRb.velocity, interpolation);
        bullet.GetComponent<InitialSpeed>().setDirection(dir);
    }

    private void Update()
    {
        if (attacking)
        {
            if (isShielded)
            {
                shieldRecover += Time.deltaTime;
                if (shieldRecover >= 1)
                {
                    health += restoreLifePerSecondWithShield;
                    healthBar.setSliderValue(health);
                    shieldRecover = 0;
                }
            }

        }


        lastBasicAttack += Time.deltaTime;

        if (lastBasicAttack >= basicAttackCadence)
        {
            BasicAttack();
            lastBasicAttack = 0;
        }

    }
}
