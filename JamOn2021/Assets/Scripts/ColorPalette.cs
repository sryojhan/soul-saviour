using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorPalette : MonoBehaviour
{
    public Material material;
    //float h = 0;
    public static float currentHue = .5f;
    private void Start()
    {
        ChangeColorHue(currentHue);
        ChangeColorSaturation(1);
    }
    private void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        Graphics.Blit(source, destination, material);
    }

    public void ChangeColorHue()
    {
        material.SetFloat("_H", Random.value);
    }
    public void ChangeColorHue(float value)
    {
        material.SetFloat("_H", value);
    }
    public void ChangeColorSaturation(float value)
    {
        material.SetFloat("_S", value);
    }
}
