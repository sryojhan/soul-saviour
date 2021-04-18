using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class changeColor : MonoBehaviour
{
    Image image;
    private void Start()
    {
        image = GetComponent<Image>();
        image.color = Color.HSVToRGB(ColorPalette.currentHue, 1, 1);
    }
    public void colorChange(float value)
    {
        ColorPalette.currentHue = value;
        if(image)
        image.color = Color.HSVToRGB(value, 1, 1);
    }
}
