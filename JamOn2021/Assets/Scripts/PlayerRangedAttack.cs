using UnityEngine;

public class PlayerRangedAttack : MonoBehaviour
{
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] float cooldown;

    private bool shot;
    private float time;
    void Start()
    {
        shot = false;
        time = cooldown;
    }

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
            Vector3 mouseWorldPoint = new Vector2(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y);
            GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
            Vector3 bulletDir = mouseWorldPoint - transform.position;

            float angle = Mathf.Atan2(bulletDir.y, bulletDir.x) * Mathf.Rad2Deg;
            bullet.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

            bullet.GetComponent<InitialSpeed>().setDirection(bulletDir.normalized);
            shot = false;
        }
    }
}
