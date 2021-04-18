using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] float dashCadence;
    [SerializeField] Sprite dcha;
    [SerializeField] Sprite izq;

    private Vector2 direction;
    PlayerDash dash;
    private Rigidbody2D rb;
    private SpriteRenderer sprite;
    float x, y;

    void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        dash = GetComponent<PlayerDash>();
        direction = Vector2.zero;
    }

    private float time;
    private void Update()
    {
        time += Time.deltaTime;

        if (!dash.enabled)
        {
            if (time > dashCadence)
            {
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    dash.enabled = true;
                    SoundManager.instance.dashSound();
                    time = 0;
                }
            }

            x = Input.GetAxisRaw("Horizontal");
            y = Input.GetAxisRaw("Vertical");

            if (!GetComponent<PlayerImpaleAttack>().isAttacking)
            {
                if (rb.velocity.x > 0) sprite.sprite = dcha;
                else if (rb.velocity.x < 0) sprite.sprite = izq;
            }
        }
    }
    private void FixedUpdate()
    {
        direction = new Vector2(x, y);
        if (!dash.enabled) rb.velocity = direction.normalized * speed;
    }
    public Vector2 getDirection()
    {
        return direction;
    }
}