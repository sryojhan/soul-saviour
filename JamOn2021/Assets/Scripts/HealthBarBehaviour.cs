using UnityEngine;
using UnityEngine.UI;

public class HealthBarBehaviour : MonoBehaviour
{
    [SerializeField] GameObject healthBar;
    Slider healthBarSlider;
    void Start()
    {
        healthBarSlider = healthBar.GetComponent<Slider>();
        setHealthBar();
    }

    void setHealthBar()
    {
        healthBarSlider.minValue = 0;
        healthBarSlider.maxValue = GetComponent<BossBattle>().getHealth();
        healthBarSlider.value = healthBarSlider.maxValue;
    }
    public void setSliderValue(float value)
    {
        healthBarSlider.value = value;
    }
}
