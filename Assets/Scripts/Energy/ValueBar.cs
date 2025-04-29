using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ValueBar : MonoBehaviour
{
    private float valueCurrent;
    [SerializeField] private float valueMax;
    [SerializeField] private float valueMin;

    [SerializeField] private bool isMaxOnStart;

    private bool isMax;
    private bool isMin;

    private float valuePercentage;

    private Action onMax;
    private Action onMin;
    private Action<float> onUpdate;

    #region Properties

    public float getValueCurrent
    {
        get { return valueCurrent; }
        private set { valueCurrent = value; }
    }

    public float getValueMax
    {
        get { return valueMax; }
        private set { valueMax = value; }
    }

    public float getValueMin
    {
        get { return valueMin; }
        private set { valueMin = value; }
    }

    public bool getIsMax
    {
        get { return isMax; }
        private set { isMax = value; }
    }

    public bool getIsMin
    {
        get { return isMin; }
        private set { isMin = value; }
    }

    public float getValuePercentage
    {
        get { return valuePercentage; }
        private set { valuePercentage = value; }
    }

    public bool getIsMaxOnStart
    {
        get { return isMaxOnStart; }
        set { isMaxOnStart = value; }
    }

    public Action getOnMax
    {
        get { return onMax; }
        set { onMax = value; }
    }

    public Action getOnMin
    {
        get { return onMin; }
        set { onMin = value; }
    }

    public Action<float> getOnUpdate
    {
        get { return onUpdate; }
        set { onUpdate = value; }
    }

    #endregion

    private void Awake()
    {
        if(isMaxOnStart)
        {
            SetValue(valueMax);
        }
    }

    public void SetValue(float valueToSet)
    { 
        valueCurrent = valueToSet;
        UpdateValue();
    }

    public void AddValue(float valueToAdd)
    { 
        valueCurrent += valueToAdd;
        UpdateValue();
    }

    public void AddValueMax(float valueToAdd)
    {
        valueMax += valueToAdd;
        UpdateValue();
    }
    public void SetValueMax(float valueToSet)
    {
        valueMax = valueToSet;
        UpdateValue();
    }

    public void AddValueMin(float valueToAdd)
    {
        valueMin += valueToAdd;
        UpdateValue();
    }

    public void SetValueMin(float valueToSet)
    {
        valueMin = valueToSet;
        UpdateValue();
    }

    public void UpdateValue()
    {
        valueCurrent = Mathf.Clamp(valueCurrent, valueMin, valueMax);

        valuePercentage = valueCurrent / valueMax;

        isMin = valueCurrent <= valueMin;
        isMax = valueCurrent >= valueMax;

        if (isMin)
        {
            onMin?.Invoke();
        }

        if (isMax)
        {
            onMax?.Invoke();
        }

        onUpdate?.Invoke(valueCurrent);

    }
}
