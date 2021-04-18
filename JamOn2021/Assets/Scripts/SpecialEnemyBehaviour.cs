using UnityEngine;

public class SpecialEnemyBehaviour : MonoBehaviour
{

    [SerializeField] GameObject player;
    [SerializeField] float distanceToPlayer;
    [SerializeField] float velocity;
    [SerializeField] float distanceToAttack;
    [SerializeField] float deactivateSpeed;
    [SerializeField] float cadence;
    [SerializeField] float minTimeValue;
    [SerializeField] float maxTimeValue;

    private bool active;
    private bool attacking;
    Vector2 playerEnemyDirection;
    Vector3 initialPosition;
    Rigidbody2D rb;
    GameObject circle;
    SpriteRenderer spriteRenderer;
    Transform circleTr;
    PlayerHealth playerHealth;


    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        rb = GetComponent<Rigidbody2D>();
        playerHealth = player.GetComponent<PlayerHealth>();
        active = false;
        circle = transform.GetChild(0).gameObject;
        spriteRenderer = circle.GetComponent<SpriteRenderer>();
        createCircle();
    }

    void setCircleAlpha(float alpha)
    {
        Color col = spriteRenderer.color;
        col.a = (alpha / 255);
        spriteRenderer.color = col;
    }
    void createCircle()
    {
        setCircleAlpha(40);
        circleTr = circle.GetComponent<Transform>();
        Vector3 a = circleTr.transform.localScale;
        a.x = distanceToAttack * 2; a.y = distanceToAttack * 2;
        circleTr.transform.localScale = a;
    }

    private float time;
    void Update()
    {
        playerEnemyDirection = (player.transform.position - transform.position);
        time += Time.deltaTime;

        if (time > cadence)
        {
            attack();
            time = 0;
        }
    }
    void attack()
    {
        if(GetComponent<Renderer>().isVisible) SoundManager.instance.specialEnemyAttack();
        setCircleAlpha(255);
        Invoke("resetAlpha", 0.3f);

        if (attacking)
        {
            playerHealth.looseLife();
        }
    }
    void resetAlpha()
    {
        setCircleAlpha(40);
    }

    private float t;
    private bool wait;

    private void FixedUpdate()
    {
        float mng = playerEnemyDirection.magnitude;
        if (!active)
        {
            if (mng < distanceToPlayer)
            {
                initialPosition = transform.position;
                active = true;
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
                        rb.velocity = dir.normalized * velocity;
                    }
                    else rb.velocity = Vector2.zero;

                    wait = !wait;
                    t = 0;
                }

            }
        }
        else
        {
            if (mng < distanceToPlayer)
            {
                if (mng < distanceToAttack)
                {
                    if (!attacking)
                    {
                        rb.velocity = Vector2.zero;
                        attacking = true;
                    }
                }
                else if (mng > distanceToAttack && attacking)
                {
                    attacking = false;
                    resetAlpha();
                }
                else rb.velocity = playerEnemyDirection.normalized * velocity;
            }
            else if (mng > distanceToPlayer + 1)
            {
                if ((transform.position - initialPosition).magnitude <= 0.5f)
                {
                    rb.velocity = Vector2.zero;
                    active = false;
                }
                else rb.velocity = -(transform.position - initialPosition).normalized * deactivateSpeed;
            }
            else rb.velocity = Vector2.zero;
        }
    }
}