using UnityEngine;

public class PlayerDash : MonoBehaviour
{
    [SerializeField] float dashSpeed;
    [SerializeField] float dashDistance;

    Vector3 initialPosition;
    Rigidbody2D rb;
    PlayerMovement pM;
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        pM = GetComponent<PlayerMovement>();
    }

    private void OnEnable()
    {
        initialPosition = transform.position;
        rb.AddForce(pM.getDirection() * dashSpeed, ForceMode2D.Impulse);
    }
    private void OnDisable()
    {
        rb.velocity = Vector2.zero;
    }
    void FixedUpdate()
    {
        if ((transform.position - initialPosition).magnitude > dashDistance) enabled = false;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        enabled = false;
    }
}
