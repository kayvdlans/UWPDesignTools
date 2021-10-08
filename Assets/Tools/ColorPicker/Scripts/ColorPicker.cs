using Microsoft.MixedReality.Toolkit;
using Microsoft.MixedReality.Toolkit.Input;
using Microsoft.MixedReality.Toolkit.Rendering;
using Microsoft.MixedReality.Toolkit.UI;
using TMPro;
using UnityEngine;

namespace ARP.UWP.Tools
{
    [RequireComponent(typeof(GradientControl), typeof(RGBControl), typeof(HSVControl))]
    public class ColorPicker : MonoBehaviour
    {
        private const float GRADIENT_DRAG_MAX_DISTANCE = 0.5f;
        private const string SHADER_NAME_BORDER_BRIGHTNESS = "_BorderMinValue";
        private const string SHADER_NAME_IRIDESCENCE_INTENSITY = "_IridescenceIntensity";

        public delegate void UpdateTargetEvent(ColorableGroup target);
        public event UpdateTargetEvent OnUpdateTarget = null;

        private ColorableGroup target = null;
        public ColorableGroup Target
        {
            get => target;
            set => target = value;
        }

        [SerializeField, Header("Alpha & Hex")] private PinchSlider sliderAlpha = null;
        [SerializeField, Space] private TextMeshPro textAlpha = null;
        [SerializeField] private TextMeshPro textHex = null;

        [SerializeField, Header("Extra Properties")] private GameObject extraPropertiesButton = null;
        [SerializeField, Space] private PinchSlider sliderBorderBrightness = null;
        [SerializeField] private PinchSlider sliderIridiscenceIntensity = null;
        [SerializeField, Space] private TextMeshPro textBorderBrightness = null;
        [SerializeField] private TextMeshPro textIridiscenceIntensity = null;

        private GradientControl gradientControl = null;
        private RGBControl rgbControl = null;
        private HSVControl hsvControl = null;

        private Color customColor;
        private float hue, saturation, brightness, alpha = 1f;
        private float borderBrightness, iridescenceIntensity = 1f;

        private bool isDraggingSliders = false;

        public Color CustomColor { get {  return customColor; } set {  customColor = value; } }
        public float Hue { get { return hue; } set {  hue = value; } }
        public float Saturation { get { return saturation; } set {  saturation = value; } }
        public float Brightness { get { return brightness; } set { brightness = value; } }
        public float Alpha => alpha;

        private void Awake()
        {
            gradientControl = GetComponent<GradientControl>();
            rgbControl = GetComponent<RGBControl>();
            hsvControl = GetComponent<HSVControl>();

            rgbControl.OnUpdateRGB += UpdateColorRGB;
            hsvControl.OnUpdateHSV += UpdateColorHSV;
        }

        private void Start()
        {
            gameObject.SetActive(false);
        }

        #region UI Logic
        public void ApplyColor()
        {
            gradientControl.ApplyColor();

            foreach (MeshRenderer renderer in target.Renderers)
            {
                if (target != null && renderer.material != null)
                {
                    MaterialInstance instance = renderer.EnsureComponent<MaterialInstance>();

                    if (target.Type == ColorableGroup.ObjectType.Text)
                    {
                        instance.Material.SetColor("_FaceColor", customColor);
                    }
                    else
                    {
                        instance.Material.color = customColor;
                    }
                }
            }
        }

        public void ApplySliderValues()
        {
            rgbControl.ApplySliderValues(customColor);
            hsvControl.ApplySliderValues(hue, saturation, brightness);

            sliderAlpha.SliderValue = Mathf.Clamp01(customColor.a);
        }

        public void UpdateSliderText()
        {
            rgbControl.UpdateSliderText(customColor);
            hsvControl.UpdateSliderText(hue, saturation, brightness);

            textHex.text = "#" + ColorUtility.ToHtmlStringRGBA(customColor);
            textAlpha.text = Mathf.Clamp(Mathf.RoundToInt(customColor.a * 100), 0, 100) + "%";
        }

        private void HandleExtraProperties()
        {
            extraPropertiesButton.SetActive(target.HasExtraProperties);

            if (!target.HasExtraProperties)
            {
                return;
            }

            MaterialInstance instance = target.Renderers[0].EnsureComponent<MaterialInstance>();

            if (target.HasBorderLight)
            {
                borderBrightness = instance.Material.GetFloat(SHADER_NAME_BORDER_BRIGHTNESS);
            }

            if (target.HasIridescence)
            {
                iridescenceIntensity = instance.Material.GetFloat(SHADER_NAME_IRIDESCENCE_INTENSITY);
            }

            sliderBorderBrightness.gameObject.SetActive(target.HasBorderLight);
            textBorderBrightness.gameObject.SetActive(target.HasBorderLight);

            sliderIridiscenceIntensity.gameObject.SetActive(target.HasIridescence);
            textIridiscenceIntensity.gameObject.SetActive(target.HasIridescence);

            ApplyExtraPropertyValues();
        }

        public void ApplyExtraPropertyValues(bool onlyText = false)
        {
            if (!onlyText)
            {
                sliderBorderBrightness.SliderValue = Mathf.Clamp01(borderBrightness);
                sliderIridiscenceIntensity.SliderValue = Mathf.Clamp01(iridescenceIntensity);

            }
            textBorderBrightness.text = Mathf.Clamp(Mathf.RoundToInt(borderBrightness * 100), 0, 100) + "%";
            textIridiscenceIntensity.text = Mathf.Clamp(Mathf.RoundToInt(iridescenceIntensity * 100), 0, 100) + "%";
        }

        public void UpdateExtraProperties()
        {
            if (isDraggingSliders)
            {
                borderBrightness = sliderBorderBrightness.SliderValue;
                iridescenceIntensity = sliderIridiscenceIntensity.SliderValue;

                foreach (Renderer renderer in target.Renderers)
                {
                    MaterialInstance instance = renderer.EnsureComponent<MaterialInstance>();

                    if (target.HasBorderLight)
                    {
                        instance.Material.SetFloat(SHADER_NAME_BORDER_BRIGHTNESS, borderBrightness);
                    }

                    if (target.HasIridescence)
                    {
                        instance.Material.SetFloat(SHADER_NAME_IRIDESCENCE_INTENSITY, iridescenceIntensity);
                    }
                }

                ApplyExtraPropertyValues(true);
            }
        }
        #endregion

        #region Public Functions
        public void SummonColorPicker(ColorableGroup colorable)
        {
            gameObject.SetActive(true);
            target = colorable;         
            ExtractColorFromMaterial(target);
            OnUpdateTarget(target);
        }

        private void UpdateColorHSV(float hue, float saturation, float brightness)
        {
            this.hue = hue;
            this.saturation = saturation;
            this.brightness = brightness;
            customColor = Color.HSVToRGB(hue, saturation, brightness);
            customColor.a = alpha;

            ApplyColor();
            UpdateSliderText();
        }

        private void UpdateColorRGB(float r, float g, float b)
        {
            customColor.r = r;
            customColor.g = g;
            customColor.b = b;
            alpha = sliderAlpha.SliderValue;
            customColor.a = alpha;
            Color.RGBToHSV(customColor, out hue, out saturation, out brightness);

            ApplyColor();
            UpdateSliderText();
        }

        public void ExtractColorFromMaterial(ColorableGroup colorable)
        {
            if (colorable.Type == ColorableGroup.ObjectType.Text)
            {
                customColor = colorable.Renderers[0].material.GetColor("_FaceColor");
            }
            else
            {
                customColor = colorable.Renderers[0].material.color;
            }

            Color.RGBToHSV(customColor, out hue, out saturation, out brightness);

            gradientControl.CalculateDraggerPosition();
            ApplyColor();
            ApplySliderValues();
            UpdateSliderText();
            HandleExtraProperties();
        }

        public void StartDrag(GameObject dragger)
        {
            dragger.SetActive(true);
            isDraggingSliders = true;
        }

        public void StopDrag(GameObject dragger)
        {
            dragger.SetActive(false);
            isDraggingSliders = false;

            ApplySliderValues();
        }
        #endregion

        private void OnDestroy()
        {
            rgbControl.OnUpdateRGB -= UpdateColorRGB;
            hsvControl.OnUpdateHSV -= UpdateColorHSV;
        }
    }
}