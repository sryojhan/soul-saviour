using UnityEngine;

public class SpecialEnemyBehaviour : MonoBehaviour
{

    [SerializeField] GameObject player;
    [SerializeField] float distanceToPlayer;
    [SerializeField] float velocity;
    private bool active;
    Vector2 playerEnemyDirection;
    Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        active = false;
    }

    void Update()
    {
        playerEnemyDirection = (player.transform.position - transform.position);
        if (playerEnemyDirection.magnitude < distanceToPlayer)
        {
            active = true;
        }
    }
    private void FixedUpdate()
    {
        if (active)
        {
            rb.velocity = playerEnemyDirection * velocity;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.GetComponent<PlayerMovement>() != null)
        {

        }
    }
}
