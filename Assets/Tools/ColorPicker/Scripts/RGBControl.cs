using Microsoft.MixedReality.Toolkit.UI;
using TMPro;
using UnityEngine;

namespace ARP.UWP.Tools
{
    public class RGBControl : MonoBehaviour
    {
        public delegate void RGBUpdateEvent(float r, float g, float b);
        public event RGBUpdateEvent OnUpdateRGB = null;

        [SerializeField, Header("RGB")] private PinchSlider sliderRed = null;
        [SerializeField] private PinchSlider sliderGreen = null;
        [SerializeField] private PinchSlider sliderBlue = null;
        [SerializeField, Space] private TextMeshPro textRed = null;
        [SerializeField] private TextMeshPro textGreen = null;
        [SerializeField] private TextMeshPro textBlue = null;

        private ColorPicker colorPicker = null;
        private GradientControl gradientControl = null;
        private bool isDraggingSliders = false;

        private void Awake()
        {
            colorPicker = GetComponent<ColorPicker>();
            gradientControl = GetComponent<GradientControl>();
        }

        private void Update()
        {
            if (isDraggingSliders)
            {
                gradientControl.CalculateDraggerPosition();
            }
        }

        public void ApplySliderValues(Color color)
        {
            sliderRed.SliderValue = Mathf.Clamp01(color.r);
            sliderGreen.SliderValue = Mathf.Clamp01(color.g);
            sliderBlue.SliderValue = Mathf.Clamp01(color.b);
        }

        public void UpdateSliderText(Color color)
        {
            textRed.text = Mathf.Clamp(Mathf.RoundToInt(color.r * 255), 0, 255).ToString();
            textGreen.text = Mathf.Clamp(Mathf.RoundToInt(color.g * 255), 0, 255).ToString();
            textBlue.text = Mathf.Clamp(Mathf.RoundToInt(color.b * 255), 0, 255).ToString();
        }

        public void UpdateColorRGB()
        {
            if (isDraggingSliders)
            {
                OnUpdateRGB(sliderRed.SliderValue, sliderGreen.SliderValue, sliderBlue.SliderValue);
            }
        }
    }
}