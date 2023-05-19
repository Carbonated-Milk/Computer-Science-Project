using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public static HealthBar singleton;

    public Slider slider;
    public Image bar;
    void Awake()
    {
        singleton = this;
    }

    public void SetHealthBar(float fraction)
    {
        if (bar == null) return;
        slider.value = fraction;
        bar.color = Vector4.Lerp(Color.red, Color.green, fraction);
    }
}
