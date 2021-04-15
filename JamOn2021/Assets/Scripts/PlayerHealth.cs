using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int lifes = 3;

    public float invulneravilityTime = 1;
    public int numberOfTicks = 10;


    private bool isInvulnerable = false;


    public Image[] healthIcons;


    private float t = 0;
    private float tick_timer = 0;

    private SpriteRenderer sprite;
    private void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
    }
    private void Update()
    {
        if (isInvulnerable)
        {
            if ((t += Time.deltaTime) > invulneravilityTime)
            {
                t = 0;
                tick_timer = 0;
                isInvulnerable = false;
                sprite.enabled = true;
            }
            else
            {
                if ((tick_timer += Time.deltaTime) > invulneravilityTime / numberOfTicks)
                {
                    sprite.enabled = !sprite.enabled;
                    tick_timer = 0;
                }
            }
        }

        if (Input.GetKeyDown(KeyCode.Space)) looseLife();
        if (Input.GetKeyDown(KeyCode.E)) restoreLife();
    }

    public bool looseLife()
    {
        if (!isInvulnerable)
        {
            isInvulnerable = true;
            --lifes;
            healthIcons[lifes].color = Color.gray;
            if (lifes <= 0) { die(); return false; }
            return true;
        }
        return false;
    }


    public void restoreLife()
    {
        lifes = Mathf.Min(++lifes, 3);
        healthIcons[lifes - 1].color = Color.white;
    }

    public void die()
    {
        lifes = 0;
    }
}
