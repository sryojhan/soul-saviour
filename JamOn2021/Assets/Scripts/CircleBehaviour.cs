using UnityEngine;

public class CircleBehaviour : MonoBehaviour
{
    SpecialEnemyBehaviour enemy;
    SpriteRenderer sp;
    void Start()
    {
        enemy = GetComponentInParent<SpecialEnemyBehaviour>();
        sp = GetComponent<SpriteRenderer>();
        setCircle();
    }

    void setCircle()
    {
        Vector3 scale = transform.localScale;
        scale.x = enemy.getDistanceToAttack()*2;
        scale.y = scale.x;
        transform.localScale = scale;
        sp.color = new Color(sp.color.r, sp.color.g, sp.color.b, 0);
    }
}
