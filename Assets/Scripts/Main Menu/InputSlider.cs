using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InputSlider : MonoBehaviour
{
    public TMP_Text outputText;

    public void setSliderValue(float value)
    {
        outputText.text = value.ToString();
    }
}
