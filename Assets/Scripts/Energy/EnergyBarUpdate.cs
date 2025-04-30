using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

[RequireComponent(typeof(ValueBar))]
public class EnergyBarUpdate : MonoBehaviour
{
    private ValueBar energyBar;
    [SerializeField] UIDocument gameplayHud;

    private VisualElement bar;
    private VisualElement barBackground;

    private void Start()
    {
        energyBar = GetComponent<ValueBar>();
        energyBar.getOnUpdate += UpdateBar;

        bar = gameplayHud.rootVisualElement.Q<VisualElement>("EnergyBar");
        barBackground = gameplayHud.rootVisualElement.Q<VisualElement>("EnergyBarBackground");
    }

    private void OnDisable()
    {
        energyBar.getOnUpdate -= UpdateBar;
    }

    public void UpdateBar(float value)
    { 
        bar.style.width = new Length(value, LengthUnit.Percent);
        barBackground.style.width = bar.style.width;
    }
}
