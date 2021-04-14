using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorPalette : MonoBehaviour
{
    public Material material;
    float h = 0;
    private void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        Graphics.Blit(source, destination, material);
    }
    private void Update()
    {
        //h += Time.deltaTime;
        //if (h > 1) h = 0;
        //material.SetFloat("_H", h);
    }
    public void ChangeColorHue()
    {
        material.SetFloat("_H", Random.value);
    }
}
