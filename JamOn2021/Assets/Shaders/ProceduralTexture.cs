using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProceduralTexture
{
    const float step = 0.01f;
    private static Texture2D generateClearTexture(int size)
    {
        Texture2D texture = new Texture2D(size, size);

        for (int x = 0; x < size; x++)
            for (int y = 0; y < size; y++)
                texture.SetPixel(x, y, Color.clear);

        texture.filterMode = FilterMode.Point;
        texture.Apply();
        return texture;
    }

    static void DrawCircleInTexture(Texture2D tex, int size, float xGeneral = 1, float yGeneral = 1, float xDef = 1, float yDef = 1)
    {
        int half = size / 2;
        for (float i = 0; i < Mathf.PI * 2; i += step)
        {
            float xDeformation = 1 + Random.Range(-xDef, xDef);
            float yDeformation = 1 + Random.Range(-yDef, yDef);

            int x = Mathf.FloorToInt(Mathf.Cos(i) * half * xDeformation * xGeneral) + half;
            int y = Mathf.FloorToInt(Mathf.Sin(i) * half * yDeformation * yGeneral) + half;

            float r = Random.value;
            Color c;
            if (r > .7f) c = Color.white;
            else
            {
                r = Random.value * .05f;
                c = Color.white * r;
                c.r--;
            }
            c.a = 1;
            tex.SetPixel(x, y, c);
        }

        tex.Apply();
    }

    public static Sprite PlayerSprite(int size, int circleCount = 1,float xGeneral = 1, float yGeneral = 1,  float xDef = 1, float yDef = 1)
    {
        Texture2D tex = generateClearTexture(size);
       
        for (int i = 0; i < circleCount; i++)
            DrawCircleInTexture(tex, size, xGeneral, yGeneral, xDef, yDef);

        return Sprite.Create(tex, new Rect(0, 0, size, size), Vector2.one / 2, size);
    }
}
