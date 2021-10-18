using Microsoft.MixedReality.Toolkit.UI;
using TMPro;
using UnityEngine;

namespace ARP.UWP.Tools.ColorPicker.Controls
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
                colorPicker.Hue = sliderHue.SliderValue;
                colorPicker.Saturation = sliderSaturation.SliderValue;
                colorPicker.Brightness = sliderBrightness.SliderValue;

                colorPicker.CustomColor = Color.HSVToRGB(colorPicker.Hue, colorPicker.Saturation, colorPicker.Brightness);
                colorPicker.CustomColor.a = colorPicker.Alpha;
                colorPicker.ApplyColor();

                UpdateTextObjects();
            }
        }

        public override void UpdateSliderValues()
        {
            sliderHue.SliderValue = Mathf.Clamp(colorPicker.Hue, 0, 1);
            sliderSaturation.SliderValue = Mathf.Clamp(colorPicker.Saturation, 0, 1);
            sliderBrightness.SliderValue = Mathf.Clamp(colorPicker.Brightness, 0, 1);
        }

        public override void UpdateTextObjects()
        {
            textHue.text = Mathf.Clamp(Mathf.RoundToInt(colorPicker.Hue * 360), 0, 360).ToString();
            textSaturation.text = Mathf.Clamp(Mathf.RoundToInt(colorPicker.Saturation * 100), 0, 100) + "%";
            textBrightness.text = Mathf.Clamp(Mathf.RoundToInt(colorPicker.Brightness * 100), 0, 100) + "%";
        }
    }
}
