using ARP.UWP.Tools.Colors;
using Microsoft.MixedReality.Toolkit.UI;
using TMPro;
using UnityEngine;

namespace ARP.UWP.Tools
{
    public class AlphaHexControl : Control
    {
        [SerializeField, Header("Sliders")] private PinchSlider sliderAlpha = null;

        [SerializeField, Header("Text Objects")] private TextMeshPro textAlpha = null;
        [SerializeField] private TextMeshPro textHex = null;

        public override void DoUpdate()
        {
            if (isDraggingSlider)
            {
                colorPicker.Alpha = sliderAlpha.SliderValue;
                colorPicker.CustomColor.a = colorPicker.Alpha;
                colorPicker.ApplyColor();

                UpdateTextObjects();
            }
        }

        public override void UpdateSliderValues()
        {
            sliderAlpha.SliderValue = Mathf.Clamp(colorPicker.CustomColor.a, 0, 1);
        }

        public override void UpdateTextObjects()
        {
            textAlpha.text = Mathf.Clamp(Mathf.RoundToInt(colorPicker.CustomColor.a * 100), 0, 100) + "%";
            textHex.text = "#" + ColorUtility.ToHtmlStringRGBA(colorPicker.CustomColor);
        }
    }
}