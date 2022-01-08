using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class FillBar : MonoBehaviour
{
    public Slider slider;
    public TextMeshProUGUI displayText;

    private float currentValue = 0f;
    public float CurrentValue
    {
        get
        {
            return currentValue;
        }
        set
        {
            currentValue = value;
            slider.value = currentValue;
            displayText.text = (slider.value * 100).ToString("0.00") + "%";
        }
    }

}
