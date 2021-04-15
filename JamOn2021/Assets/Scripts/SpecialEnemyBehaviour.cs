using UnityEngine;

public class SpecialEnemyBehaviour : MonoBehaviour
{

    [SerializeField] GameObject player;
    [SerializeField] float distanceToPlayer;
    [SerializeField] float velocity;
    [SerializeField] float distanceToAttack;
    [SerializeField] float deactivateSpeed;
    [SerializeField] float cadence;

    LineRenderer line;
    private bool active;
    private bool attacking;
    Vector2 playerEnemyDirection;
    Vector3 initialPosition;
    Rigidbody2D rb;
    GameObject circle;
    SpriteRenderer sp;
    Transform circleTr;
    

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        line = GetComponent<LineRenderer>();
        active = false;
        createPoints();
        createCircle();
    }
    
    void createCircle()
    {
        circle = transform.GetChild(0).gameObject;
        sp = circle.GetComponent<SpriteRenderer>();
        Color aux = sp.color;
        aux.a = 100;
        sp.color = aux;
        circleTr = circle.GetComponent<Transform>();
        Vector3 a = circleTr.transform.localScale;
        a.x = distanceToAttack; a.y = distanceToAttack;
        circleTr.transform.localScale = a;
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
            x = Mathf.Sin(Mathf.Deg2Rad * angle) * distanceToAttack;
            y = Mathf.Cos(Mathf.Deg2Rad * angle) * distanceToAttack;

            line.SetPosition(i, new Vector3(x, y, z));

            angle += (360f / 360);
        }
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
                else if (mng > distanceToAttack && attacking)
                {
                    attacking = false;
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
