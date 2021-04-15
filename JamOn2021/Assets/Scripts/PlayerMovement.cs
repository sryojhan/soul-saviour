using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float speed;

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
    private void Update()
    {
        if (!dash.enabled)
        {
            if (Input.GetKeyDown(KeyCode.Space)) dash.enabled = true;

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