using UnityEngine;

public class SpecialEnemyBehaviour : MonoBehaviour
{

    [SerializeField] GameObject player;
    [SerializeField] float distanceToPlayer;
    [SerializeField] float velocity;
    [SerializeField] float distanceToAttack;
    [SerializeField] float deactivateSpeed;
    [SerializeField] float attackRadius;
    [SerializeField] float cadence;
    [SerializeField] LayerMask playerMask;

    LineRenderer line;
    private bool active;
    private bool attacking;
    Vector2 playerEnemyDirection;
    Vector3 initialPosition;
    Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        line = GetComponent<LineRenderer>();
        active = false;
        createPoints();
    }

    void createPoints()
    {
        line.positionCount = 361;
        line.useWorldSpace = false;
        float x;
        float y;
        float z = 0f;

        float angle = 20f;

        for (int i = 0; i < (360 + 1); i++)
        {
            x = Mathf.Sin(Mathf.Deg2Rad * angle) * attackRadius;
            y = Mathf.Cos(Mathf.Deg2Rad * angle) * attackRadius;

            line.SetPosition(i, new Vector3(x, y, z));

            angle += (360f / 360);
        }
    }
    public float getDistanceToAttack()
    {
        return attackRadius;
    }

    private float time;
    void Update()
    {
        playerEnemyDirection = (player.transform.position - transform.position);
        time += Time.deltaTime;

        if(time > cadence)
        {
            attack();
            time = 0;
        }
    }
    void attack()
    {
        if (attacking)
        {
            
        }
    }

    private void FixedUpdate()
    {
        float mng = playerEnemyDirection.magnitude;
        if (!active)
        {
            if(mng < distanceToPlayer)
            {
                initialPosition = transform.position;
                active = true;
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
                else if (mng > distanceToAttack + 1 && attacking) attacking = false;
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
