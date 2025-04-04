using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AbilitySlider : MonoBehaviour
{
    [SerializeField]
    GravitySim _gravitySim;
    public Slider slider;
    private void Awake()
    {
        InitSlider();
    }
    public void InitSlider() 
    {
        _gravitySim = FindObjectOfType<GravitySim>();
        slider.value = 0.5f;
        slider.onValueChanged.AddListener(SliderDelegate);    
    }

    private void SliderDelegate(float val)
    {
        if (_gravitySim != null) 
        {
            _gravitySim.AttackModifier = val;
        }
    }
}
