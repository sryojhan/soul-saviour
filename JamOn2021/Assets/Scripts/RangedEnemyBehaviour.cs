using System.Security.Cryptography;
using UnityEditor;
using UnityEditor.Rendering;
using UnityEngine;

public class RangedEnemyBehaviour : MonoBehaviour
{
    [SerializeField] GameObject player;
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] float distanceToPlayer;
    [SerializeField] float distanceToBeDeactivated;
    [SerializeField] float backSpeed;
    [SerializeField] float forwardSpeed;
    [SerializeField] float deactivateSpeed;
    [SerializeField] float cadence;
    [SerializeField] float minTimeValue;
    [SerializeField] float maxTimeValue;
    [SerializeField] float interpolation;

    [SerializeField] float bulletSpeed;

    Vector3 initialPosition;
    Color originalColor;

    SpriteRenderer sp;
    Rigidbody2D rb;

    private Rigidbody2D playerRb;
    private bool active;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        rb = GetComponent<Rigidbody2D>();
        sp = GetComponent<SpriteRenderer>();
        active = false;
        initialPosition = transform.position;
        originalColor = sp.color;
        playerRb = player.GetComponent<Rigidbody2D>();
    }

     public bool isActive()
    {
        return active;
    }
    public void setActive(bool act)
    {
        active = act;
    }

    float time;
    void Update()
    {
        if (active)
        {
            time += Time.deltaTime;

            if (time > cadence)
            {
                if (GetComponent<Renderer>().isVisible) SoundManager.instance.enemyShoot();

                Vector2 playerPos = new Vector2(player.transform.position.x, player.transform.position.y);

                float playerAngle = Vector2.Angle(new Vector2(transform.position.x, transform.position.y) - playerPos, playerRb.velocity);

                float auxAngle1 = Mathf.Asin(Mathf.Sin(playerAngle * Mathf.Deg2Rad) * (playerRb.velocity.magnitude / bulletSpeed));

                float auxAngle2 = Vector2.Angle(Vector2.right, playerPos - new Vector2(transform.position.x, transform.position.y));

                float shootAngle = auxAngle2 - auxAngle1 * Mathf.Rad2Deg;

                if (player.transform.position.y < transform.position.y) shootAngle = -shootAngle;

                GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);

                //playerPos += Vector3.Lerp(Vector3.zero, playerRb.velocity, interpolation);
                bullet.GetComponent<InitialSpeed>().setDirection((Quaternion.Euler(0, 0, shootAngle) * Vector2.right).normalized);

                time = 0;
            }
        }
    }

    private float t;
    private bool wait;

    private void FixedUpdate()
    {
        Vector2 playerEnemyDirection = (player.transform.position - transform.position);
        if (!active)
        {
            if (playerEnemyDirection.magnitude < distanceToPlayer)
            {
                initialPosition = transform.position;
                active = true;
                sp.color = Color.white;
            }
            else
            {
                t += Time.deltaTime;
                if (t > Random.Range(minTimeValue, maxTimeValue))
                {
                    if (!wait)
                    {
                        float x = Random.Range(-1.0f, 1.0f);
                        float y = Random.Range(-1.0f, 1.0f);
                        Vector2 dir = new Vector2(x, y);
                        rb.velocity = dir.normalized * backSpeed;
                    }
                    else rb.velocity = Vector2.zero;

                    wait = !wait;
                    t = 0;
                }

            }
        }
        else
        {
            if (playerEnemyDirection.magnitude < distanceToPlayer)
            {
                rb.velocity = -playerEnemyDirection.normalized * backSpeed;
            }
            else if (playerEnemyDirection.magnitude > distanceToPlayer + 1)
            {
                if (playerEnemyDirection.magnitude > distanceToBeDeactivated)
                {
                    rb.velocity = -(transform.position - initialPosition).normalized * deactivateSpeed;
                    if ((transform.position - initialPosition).magnitude <= 0.5f)
                    {
                        rb.velocity = Vector2.zero;
                        active = false;
                        sp.color = originalColor;
                    }
                }
                else rb.velocity = playerEnemyDirection.normalized * forwardSpeed;
            }
            else rb.velocity = Vector2.zero;
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!active) rb.velocity = -rb.velocity;
    }
}
