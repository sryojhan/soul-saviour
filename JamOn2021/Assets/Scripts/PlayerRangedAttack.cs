using UnityEngine;

public class PlayerRangedAttack : MonoBehaviour
{
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] float cooldown;

    private bool shot;
    void Start()
    {
        shot = false;
    }

    private float time;
    void Update()
    {
        time += Time.deltaTime;
        if (time > cooldown)
        {
            if (Input.GetMouseButtonDown(1)) //Boton derecho
            {
                shot = true;
                time = 0;
            }
        }
    }

    private void FixedUpdate()
    {
        if (shot)
        {
            GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
            Vector3 mouseWorldPoint = new Vector2(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y);
            bullet.GetComponent<InitialSpeed>().setDirection(mouseWorldPoint-transform.position);
            shot = false;
        }
    }
}
