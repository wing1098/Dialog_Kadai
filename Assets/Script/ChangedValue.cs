using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangedValue : MonoBehaviour
{
    public Text value;

    // Start is called before the first frame update
    void Start()
    {
        value = GetComponent<Text>();
    }

    // Update is called once per frame
    public void OnSliderValueChanged(float sliderValue)
    {
        value.text = sliderValue.ToString("0");
    }
}
