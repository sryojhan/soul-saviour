using UnityEngine;

public class InitialSpeed : MonoBehaviour
{
    public float bulletSpeed;
    Vector2 direction;
    Rigidbody2D rb;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        rb.velocity = direction.normalized * bulletSpeed;
    }
    public void setDirection(Vector2 dir)
    {
        direction = dir;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(gameObject);
    }
}
