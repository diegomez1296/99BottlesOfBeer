using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ColorAnimation : MonoBehaviour
{
    private Text text;
    public float minValue = 180;
    public float maxValue = 255;
    private float currentValue;
    private bool isMax = false;
    
    private List<float> textColorValues;

    void Awake()
    {
        text = GetComponent<Text>();
        textColorValues = getColorValues(text);
        currentValue = minValue;
    }

    void Update()
    {
        ChangeColor();  
    }

    void ChangeColor()
    {
        if(!isMax)
        {
            text.color = new Color(textColorValues[0], textColorValues[1], textColorValues[2], currentValue/255f);
            currentValue++;
            if (currentValue >= maxValue) 
                isMax = true;
        }
        else if(isMax)
        {
            text.color = new Color(textColorValues[0], textColorValues[1], textColorValues[2], currentValue/255f);
            currentValue--;
            if (currentValue <= minValue) 
                isMax = false;
        }
    }

    private List<float> getColorValues(Text text) 
    {
        List<float> textColorValues = new List<float>();
        textColorValues.Add(text.color.r);
        textColorValues.Add(text.color.g);
        textColorValues.Add(text.color.b);
        return textColorValues;
    }
}
