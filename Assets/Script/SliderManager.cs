
using UnityEngine;
using UnityEngine.UI;

public class SliderManager : MonoBehaviour
{
    public Slider slider;
    public Text fileName;

    // Start is called before the first frame update
    void Start()
    {
        slider = GetComponent<Slider>();

        float maxValve = 100f;
        float defaultValve = 0f;

        slider.maxValue = maxValve;
        slider.value = defaultValve;
    }
}
