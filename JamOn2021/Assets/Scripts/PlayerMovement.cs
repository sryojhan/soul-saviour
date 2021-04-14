using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float speed;
     
    private Vector2 direction;
    PlayerDash dash;
    private Rigidbody2D rb;

    bool aDown = false, sDown = false, wDown = false, dDown = false, awDown = false, asDown = false, wdDown = false, sdDown = false;

    void Start() { 
        rb = GetComponent<Rigidbody2D>();
        dash = GetComponent<PlayerDash>();
        direction = Vector2.zero;
    }
    private void Update()
    {
        aDown = false; sDown = false; wDown = false; dDown = false;  awDown = false; asDown = false; wdDown = false; sdDown = false;
        if (!dash.enabled)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                dash.enabled = true;
            }
            else if (Input.GetKey(KeyCode.A) && Input.GetKey(KeyCode.W)) awDown = true;
            else if (Input.GetKey(KeyCode.A) && Input.GetKey(KeyCode.S)) asDown = true;
            else if (Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.D)) wdDown = true;
            else if (Input.GetKey(KeyCode.S) && Input.GetKey(KeyCode.D)) sdDown = true;
            else if (Input.GetKey(KeyCode.A)) aDown = true;
            else if (Input.GetKey(KeyCode.S)) sDown = true;
            else if (Input.GetKey(KeyCode.W)) wDown = true;
            else if (Input.GetKey(KeyCode.D)) dDown = true;
        }
    }
    private void FixedUpdate()
    {
        if (!dash.enabled)
        {
            //DASH CON UTLIMA DIRECCION TOMANDA
            /*if (awDown) { rb.velocity = (Vector2.left + Vector2.up).normalized * speed * Time.fixedDeltaTime; direction = rb.velocity.normalized; }
            else if (asDown) { rb.velocity = (Vector2.left + Vector2.down).normalized * speed * Time.fixedDeltaTime; direction = rb.velocity.normalized; }
            else if (wdDown) { rb.velocity = (Vector2.right + Vector2.up).normalized * speed * Time.fixedDeltaTime; direction = rb.velocity.normalized; }
            else if (sdDown) { rb.velocity = (Vector2.right + Vector2.down).normalized * speed * Time.fixedDeltaTime; direction = rb.velocity.normalized; }
            else if (aDown) { rb.velocity = Vector2.left * speed * Time.fixedDeltaTime; direction = rb.velocity.normalized; }
            else if (wDown) { rb.velocity = Vector2.up * speed * Time.fixedDeltaTime; direction = rb.velocity.normalized; }
            else if (sDown) { rb.velocity = Vector2.down * speed * Time.fixedDeltaTime; direction = rb.velocity.normalized; }
            else if (dDown) { rb.velocity = Vector2.right * speed * Time.fixedDeltaTime; direction = rb.velocity.normalized; }
            else { rb.velocity = Vector2.zero; direction = rb.velocity.normalized; }*/

            if (awDown) rb.velocity = (Vector2.left + Vector2.up).normalized * speed * Time.fixedDeltaTime;
            else if (asDown) rb.velocity = (Vector2.left + Vector2.down).normalized * speed * Time.fixedDeltaTime; 
            else if (wdDown) rb.velocity = (Vector2.right + Vector2.up).normalized * speed * Time.fixedDeltaTime;
            else if (sdDown) rb.velocity = (Vector2.right + Vector2.down).normalized * speed * Time.fixedDeltaTime; 
            else if (aDown) rb.velocity = Vector2.left * speed * Time.fixedDeltaTime;
            else if (wDown) rb.velocity = Vector2.up * speed * Time.fixedDeltaTime; 
            else if (sDown) rb.velocity = Vector2.down * speed * Time.fixedDeltaTime; 
            else if (dDown) rb.velocity = Vector2.right * speed * Time.fixedDeltaTime;
            else rb.velocity = Vector2.zero; 

            direction = rb.velocity;
        }
    }
    public Vector2 getDirection()
    {
        return direction;
    }
}
