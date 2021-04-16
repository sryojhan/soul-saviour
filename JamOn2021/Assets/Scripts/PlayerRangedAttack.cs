using UnityEngine;

public class PlayerRangedAttack : MonoBehaviour
{
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] float cooldown;
    [SerializeField] AudioClip clip;
    AudioSource audioS;

    private float time;
    void Start()
    {
        time = cooldown;
        audioS = GetComponent<AudioSource>();
    }

    void Update()
    {
        time += Time.deltaTime;
        if (time > cooldown)
        {
            if (Input.GetMouseButtonDown(1)) //Boton derecho
            {
                shoot();
                time = 0;
            }
        }
    }

    void shoot()
    {
        Vector3 mouseWorldPoint = new Vector2(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y);
        GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
        Vector3 bulletDir = mouseWorldPoint - transform.position;

        float angle = Mathf.Atan2(bulletDir.y, bulletDir.x) * Mathf.Rad2Deg;
        bullet.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

        bullet.GetComponent<InitialSpeed>().setDirection(bulletDir.normalized);

        audioS.PlayOneShot(clip);
    }
}
