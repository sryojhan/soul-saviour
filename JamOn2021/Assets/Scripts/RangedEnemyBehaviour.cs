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

    Vector3 initialPosition;
    Color originalColor;

    SpriteRenderer sp;
    Rigidbody2D rb;

    private Rigidbody2D playerRb;
    private bool active;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sp = GetComponent<SpriteRenderer>();
        active = false;
        initialPosition = transform.position;
        originalColor = sp.color;
        playerRb = player.GetComponent<Rigidbody2D>();
    }

    float time;
    void Update()
    {
        if (active)
        {
            time += Time.deltaTime;

            if (time > cadence)
            {
                GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
                Vector3 playerPos = player.transform.position;



                //playerPos += Vector3.Lerp(Vector3.zero, playerRb.velocity, interpolation);
                bullet.GetComponent<InitialSpeed>().setDirection((playerPos - transform.position).normalized);
                time = 0;
            }
        }
    }

    private float t;
    private bool wait;
    
    private void FixedUpdate()
    {
        Vector2  playerEnemyDirection = (player.transform.position - transform.position);
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
                if(playerEnemyDirection.magnitude > distanceToBeDeactivated)
                {
                    rb.velocity = -(transform.position-initialPosition).normalized * deactivateSpeed;
                    if((transform.position-initialPosition).magnitude <= 0.5f)
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
