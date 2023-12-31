using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using TMPro;
using System;

public class Score : MonoBehaviour
{
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI timeText; // formatted timeSpan to MM:SS:FF
    public string nameString;
    public TimeSpan timeSpan = new TimeSpan(0, 0, 0, 0, 0);

    public void setText(string name, TimeSpan time)
    {
        nameString = name;
        timeSpan = time;
        nameText.text = nameString;
        timeText.text = timeSpan.ToString(@"hh\:mm\:ss");
    }
}
