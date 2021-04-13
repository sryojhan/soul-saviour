using UnityEngine;

public class InitialSpeed : MonoBehaviour
{
    [SerializeField] float bulletSpeed;
    Vector2 direction;
    Rigidbody2D rb;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        rb.velocity = direction.normalized * bulletSpeed * Time.fixedDeltaTime;
    }
    public void setDirection(Vector2 dir)
    {
        direction = dir;
    }
}
