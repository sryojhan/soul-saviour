using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class changeColor : MonoBehaviour
{
    public Slider pepe;
    public Image[] images;
    Image image;
    private void Start()
    {
        pepe.value = ColorPalette.currentHue;
        image = GetComponent<Image>();
        cambiacoloratodo(Color.HSVToRGB(ColorPalette.currentHue, 1, 1));
    }
    public void colorChange(float value)
    {
        ColorPalette.currentHue = value;
        if(image) cambiacoloratodo(Color.HSVToRGB(value, 1, 1));
    }

    void cambiacoloratodo(Color a)
    {
        for(int i = 0; i< images.Length; i++)
        {
            images[i].color = a;
        }
        image.color = a;
    }
}
