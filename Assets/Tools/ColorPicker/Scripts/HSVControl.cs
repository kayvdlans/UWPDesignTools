using Microsoft.MixedReality.Toolkit.UI;
using TMPro;
using UnityEngine;

namespace ARP.UWP.Tools
{
    public class HSVControl : MonoBehaviour
    {
        public delegate void HSVUpdateEvent(float hue, float saturation, float brightness);
        public event HSVUpdateEvent OnUpdateHSV = null;

        [SerializeField, Header("HSV")] private PinchSlider sliderHue = null;
        [SerializeField] private PinchSlider sliderSaturation = null;
        [SerializeField] private PinchSlider sliderBrightness = null;
        [SerializeField, Space] private TextMeshPro textHue = null;
        [SerializeField] private TextMeshPro textSaturation = null;
        [SerializeField] private TextMeshPro textBrightness = null;

        private GradientControl gradientControl = null;
        private bool isDraggingSliders = false;

        private void Awake()
        {
            gradientControl = GetComponent<GradientControl>();
        }

        private void Update()
        {
            if (isDraggingSliders)
            {
                gradientControl.CalculateDraggerPosition();
            }
        }

        public void ApplySliderValues(float hue, float saturation, float brightness)
        {
            sliderHue.SliderValue = Mathf.Clamp01(hue);
            sliderSaturation.SliderValue = Mathf.Clamp01(saturation);
            sliderBrightness.SliderValue = Mathf.Clamp01(brightness);
        }

        public void UpdateSliderText(float hue, float saturation, float brightness)
        {
            textHue.text = Mathf.Clamp(Mathf.RoundToInt(hue * 360), 0, 360).ToString();
            textSaturation.text = Mathf.Clamp(Mathf.RoundToInt(saturation * 100), 0, 100) + "%";
            textBrightness.text = Mathf.Clamp(Mathf.RoundToInt(brightness * 100), 0, 100) + "%";
        }

        public void UpdateColorHSV()
        {
            if (isDraggingSliders)
            {
                OnUpdateHSV(sliderHue.SliderValue, sliderSaturation.SliderValue, sliderBrightness.SliderValue);
            }
        }
    }
}
