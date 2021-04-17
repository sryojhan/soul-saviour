using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] float dashCadence;

    private Vector2 direction;
    PlayerDash dash;
    private Rigidbody2D rb;
    float x, y;

    void Start()
    {
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