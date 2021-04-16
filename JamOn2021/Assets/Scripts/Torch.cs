using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Torch : MonoBehaviour
{
    [SerializeField] InvokeBoss invoke;
    SpriteRenderer sRenderer;

    bool theresPlayer = false;
    bool lighted = false;

    Color oriColor;


    private void Awake()
    {
        sRenderer = GetComponent<SpriteRenderer>();
        oriColor = sRenderer.color;
    }

    private void Update()
    {
        if (theresPlayer && !lighted && Input.GetKeyDown(KeyCode.E)) // && player.hasSoul();
        {
            lighted = true;
            print("torch lighted");
            invoke.lightTorch();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<PlayerSweepAttack>())
        {
            sRenderer.color = new Color(0.7f, 0.32f, 0.67f);
            theresPlayer = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.GetComponent<PlayerSweepAttack>())
        {
            sRenderer.color = oriColor;
            theresPlayer = false;
        }
    }
}
