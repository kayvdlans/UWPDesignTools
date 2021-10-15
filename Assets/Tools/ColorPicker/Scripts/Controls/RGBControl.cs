using ARP.UWP.Tools.Colors;
using Microsoft.MixedReality.Toolkit.UI;
using TMPro;
using UnityEngine;

namespace ARP.UWP.Tools
{
    public class RGBControl : Control
    {
        [SerializeField, Header("Sliders")] private PinchSlider sliderRed = null;
        [SerializeField] private PinchSlider sliderGreen = null;
        [SerializeField] private PinchSlider sliderBlue = null;

        [SerializeField, Header("Text Objects")] private TextMeshPro textRed = null;
        [SerializeField] private TextMeshPro textGreen = null;
        [SerializeField] private TextMeshPro textBlue = null;

        public override void DoUpdate()
        {
            if (isDraggingSlider)
            {
                colorPicker.CustomColor.r = sliderRed.SliderValue;
                colorPicker.CustomColor.g = sliderGreen.SliderValue;
                colorPicker.CustomColor.b = sliderBlue.SliderValue;

                Color.RGBToHSV(colorPicker.CustomColor, out colorPicker.Hue, out colorPicker.Saturation, out colorPicker.Brightness);

                colorPicker.ApplyColor();

                UpdateTextObjects();
            }
        }

        public override void UpdateSliderValues()
        {
            sliderRed.SliderValue = Mathf.Clamp(colorPicker.CustomColor.r, 0, 1);
            sliderGreen.SliderValue = Mathf.Clamp(colorPicker.CustomColor.g, 0, 1);
            sliderBlue.SliderValue = Mathf.Clamp(colorPicker.CustomColor.b, 0, 1);
        }

        public override void UpdateTextObjects()
        {
            textRed.text = Mathf.Clamp(Mathf.RoundToInt(colorPicker.CustomColor.r * 255), 0, 255).ToString();
            textGreen.text = Mathf.Clamp(Mathf.RoundToInt(colorPicker.CustomColor.g * 255), 0, 255).ToString();
            textBlue.text = Mathf.Clamp(Mathf.RoundToInt(colorPicker.CustomColor.b * 255), 0, 255).ToString();
        }
    }
}