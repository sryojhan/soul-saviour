using UnityEngine;

public class MeleeEnemy : MonoBehaviour
{
    [SerializeField] float length;
    [SerializeField] float width;
    [SerializeField] float attackStartPointOffset;
    [SerializeField] Transform playerPos;
    [SerializeField] LayerMask whatIsPlayer;
    [SerializeField] float damage;
    [SerializeField] float cadence;

    [SerializeField] float moveSpeed;
    [SerializeField] float viewDistance;
    [SerializeField] float pauseTime;
    [SerializeField] float pauseBeforeAttack;
    [SerializeField] float attackActiveTime;

    float attackDistance;

    RaycastHit2D hit;
    Vector2 inipos;
    Vector2 dir;

    private Rigidbody2D rb;
    float lastAttack = 0;
    bool pause = false;
    float lastPause = 0;

    float attackPause = 0;

    bool active = false;
    float activeCounter = 0;

    public void setActive(bool a) { active = a; }
    public bool isActive() { return active; }

    void OnDrawGizmos()
    {
        // Draw a yellow sphere at the transform's position
        Gizmos.color = Color.yellow;
        Gizmos.DrawRay(inipos, dir * length);
    }

    private void Start()
    {
        playerPos = GameObject.FindGameObjectWithTag("Player").transform;
        attackDistance = length * 0.75f;
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        lastAttack += Time.deltaTime;

        if ((transform.position - playerPos.position).magnitude <= length) attackPause += Time.deltaTime;

        if (active) activeCounter += Time.deltaTime;
        if (activeCounter >= attackActiveTime)
        {
            active = false;
            activeCounter = 0;
        }

        if ((transform.position - playerPos.position).magnitude <= attackDistance)
        {

            rb.velocity = Vector2.zero;
            if (lastAttack >= cadence && attackPause >= pauseBeforeAttack)
            {
                attackPause = 0;
                pause = true;
                lastAttack = 0;
                Vector2 mouseWorldPoint = new Vector2(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y);
                Vector2 pos2D = new Vector2(transform.position.x, transform.position.y);
                Vector2 playerPos2D = new Vector2(playerPos.position.x, playerPos.position.y);

                dir = (playerPos2D - pos2D).normalized;

                float angle = Vector2.Angle(new Vector2(1, 0), dir);

                inipos = pos2D + (dir * attackStartPointOffset);
                hit = Physics2D.BoxCast(pos2D + (dir * attackStartPointOffset), new Vector2(0.1f, width), angle, dir, length, whatIsPlayer);

                if (hit.collider != null)
                {
                    hit.collider.gameObject.GetComponent<PlayerHealth>().looseLife();
                }

            }
        }

        if (pause)
        {
            lastPause += Time.deltaTime;

            if (lastPause >= pauseTime)
            {
                lastPause = 0;
                pause = false;
            }
        }
    }


    private void FixedUpdate()
    {
        if (!pause)
        {
            if ((transform.position - playerPos.position).magnitude <= viewDistance || active)
            {
                if ((transform.position - playerPos.position).magnitude > attackDistance)
                {
                    rb.velocity = (playerPos.position - transform.position).normalized * moveSpeed;
                }

            }
            else rb.velocity = Vector2.zero;
        }
    }
}
