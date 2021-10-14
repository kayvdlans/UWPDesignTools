using ARP.UWP.Tools.Colors;
using Microsoft.MixedReality.Toolkit.UI;
using TMPro;
using UnityEngine;

namespace ARP.UWP.Tools
{
    public class HSVControl : Control
    {
        [SerializeField, Header("Sliders")] private PinchSlider sliderHue = null;
        [SerializeField] private PinchSlider sliderSaturation = null;
        [SerializeField] private PinchSlider sliderBrightness = null;

        [SerializeField, Header("Text Objects")] private TextMeshPro textHue = null;
        [SerializeField] private TextMeshPro textSaturation = null;
        [SerializeField] private TextMeshPro textBrightness = null;

        public override void DoUpdate()
        {
            if (isDraggingSlider)
            {
                Color color = Color.HSVToRGB(sliderHue.SliderValue, sliderSaturation.SliderValue, sliderBrightness.SliderValue);
                colorPicker.CustomColor.r = color.r;
                colorPicker.CustomColor.g = color.g;
                colorPicker.CustomColor.b = color.b;
                colorPicker.ApplyColor();

                UpdateTextObjects();
            }
        }

        public override void UpdateSliderValues()
        {
            Color.RGBToHSV(colorPicker.CustomColor, out float hue, out float saturation, out float brightness);
            sliderHue.SliderValue = Mathf.Clamp(hue, 0, 1);
            sliderSaturation.SliderValue = Mathf.Clamp(saturation, 0, 1);
            sliderBrightness.SliderValue = Mathf.Clamp(brightness, 0, 1);
        }

        public override void UpdateTextObjects()
        {
            Color.RGBToHSV(colorPicker.CustomColor, out float hue, out float saturation, out float brightness);
            textHue.text = Mathf.Clamp(Mathf.RoundToInt(hue * 360), 0, 360).ToString();
            textSaturation.text = Mathf.Clamp(Mathf.RoundToInt(saturation * 100), 0, 100) + "%";
            textBrightness.text = Mathf.Clamp(Mathf.RoundToInt(brightness * 100), 0, 100) + "%";
        }
    }
}
