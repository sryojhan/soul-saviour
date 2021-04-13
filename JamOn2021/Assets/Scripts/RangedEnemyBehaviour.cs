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

    Vector3 initialPosition;
    Color originalColor;

    SpriteRenderer sp;
    Rigidbody2D rb;
    private bool active;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sp = GetComponent<SpriteRenderer>();
        active = false;
        initialPosition = transform.position;
        originalColor = sp.color;
    }

    float time;
    void Update()
    {
        if (active)
        {
            time += Time.deltaTime;

            if (time > cadence)
            {
                GameObject bullet = Instantiate(bulletPrefab, transform);
                bullet.GetComponent<InitialSpeed>().setDirection((player.transform.position - transform.position).normalized);
                time = 0;
            }
        }
    }


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
        }
        else
        {
            if (playerEnemyDirection.magnitude < distanceToPlayer)
            {
                rb.velocity = -playerEnemyDirection.normalized * backSpeed * Time.fixedDeltaTime;
            }
            else if (playerEnemyDirection.magnitude > distanceToPlayer + 1)
            {
                if(playerEnemyDirection.magnitude > distanceToBeDeactivated)
                {
                    rb.velocity = -(transform.position-initialPosition).normalized * deactivateSpeed * Time.fixedDeltaTime;
                    if((transform.position-initialPosition).magnitude <= 1)
                    {
                        rb.velocity = Vector2.zero;
                        active = false;
                        sp.color = originalColor;
                    }
                }
                else rb.velocity = playerEnemyDirection.normalized * forwardSpeed * Time.fixedDeltaTime;
            }
            else rb.velocity = Vector2.zero;
        }
    }
}
