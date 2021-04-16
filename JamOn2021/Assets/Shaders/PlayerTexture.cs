using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTexture : MonoBehaviour
{
    public float frameRate = 10;
    public int PIXEL_SIZE = 8;

    public float xShape = 1;
    public float yShape = 1;

    public float xDef = 1;
    public float yDef = 1;

    public int numberOfCircles = 1;
    SpriteRenderer SpRenderer;

    float t = 0;
    float eye_timer = 0;

    Rigidbody2D rigidBody;
    void Start()
    {
        SpRenderer = GetComponent<SpriteRenderer>();
        rigidBody = GetComponent<Rigidbody2D>();
        frameRate = 1 / frameRate;
        createCircle();

        xShapeValue = xShape;
    }

    void createCircle()
    {
        SpRenderer.sprite = ProceduralTexture.PlayerSprite(PIXEL_SIZE, numberOfCircles, xShape, yShape, xDef, yDef);
    }
    [Header("Movement")]
    private float xShapeValue = 0;
    public float speedStretch = 0;

    [Header("Eye")]
    public float eye_minValue = 0.4f;
    public float eye_maxValue = 1f;
    public float eye_speed = 1;
    private bool blinking = false;
    public float blinkTime = 2;
    public float blinkTimeRandom = 2;
    private float nextBlinkTime = 2;
    public float blinkSpeed = 1;
    private float currentMaxValue = 0;

    private void Update()
    {
        if ((t += Time.deltaTime) > frameRate)
        {
            createCircle();
            t = 0;
        }

        if (!blinking)
        {
            eye_timer += Time.deltaTime * eye_speed;
            yShape = eye_minValue + Mathf.PingPong(eye_timer, eye_maxValue - eye_minValue);

            if (eye_timer > nextBlinkTime)
            {
                currentMaxValue = yShape;
                eye_timer = 0;
                blinking = true;
            }
        }
        else
        {
            eye_timer += Time.deltaTime * blinkSpeed;
            yShape = currentMaxValue - Mathf.PingPong(eye_timer, currentMaxValue);

            if (eye_timer > 1)
            {
                eye_timer = 0;
                blinking = false;
                nextBlinkTime = blinkTime + Random.value * blinkTimeRandom * eye_speed;
            }
        }

        if (rigidBody && rigidBody.velocity != Vector2.zero)
        {
            xShape = speedStretch;
        }
        else { xShape = xShapeValue; }
    }
}
