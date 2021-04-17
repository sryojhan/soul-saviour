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

    [Header("Mask")]
    public Transform mask;

    public float maskMaxSize = 12;
    public float maskSpeed = 1;
    private float t = 0;
    private float tick_timer = 0;

    private bool dead = false;


    private ColorPalette colorPalette;
    private SpriteRenderer sprite;
    private void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
           
        if (mask)
            mask.localScale = Vector3.zero;

        colorPalette = Camera.main.GetComponent<ColorPalette>();

    }
    private void Update()
    {
        if (dead)
        {
            if(mask)
            mask.localScale = Vector3.one * maskMaxSize * t;
            if (colorPalette)
                colorPalette.ChangeColorSaturation(1 - t);
            if ((t += Time.deltaTime * maskSpeed) > 1)
                completeDeath();
            return;
        }

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
           // healthIcons[lifes].color = Color.gray;
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
        dead = true;
        mask.position = transform.position;
    }

    private void completeDeath()
    {
        enabled = false;
        //Camera.main.GetComponent<ColorPalette>().enabled = false;
        print("i am dead");
    }
}
