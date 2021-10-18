using ARP.UWP.Tools.ColorPicker.Controls;
using UnityEngine;

namespace ARP.UWP.Tools.ColorPicker
{
    public class ColorPickerController : MonoBehaviour
    {
        public delegate void UpdateTargetEvent(ColorableGroup target);
        public event UpdateTargetEvent OnUpdateTarget = null;

        private ColorableGroup target;
        public ColorableGroup Target
        { 
            get { return target; }
            set
            {
                target = value;
                OnUpdateTarget(value);
            } 
        }

        [SerializeField] private GradientControl gradientControl = null;
        [SerializeField] private RGBControl rgbControl = null;
        [SerializeField] private HSVControl hsvControl = null;
        [SerializeField] private AlphaHexControl alphaHexControl = null;
        [SerializeField] private ColorPickerObjectContainer objectContainer = null;

        [HideInInspector] public Color CustomColor;
        [HideInInspector] public float Hue, Saturation, Brightness, Alpha = 1f;

        private void Start()
        {
            gameObject.SetActive(false);

            OnUpdateTarget += ExtractColorFromMaterial;
        }

        public void SummonColorPicker(GameObject target)
        {
            objectContainer.SummonContainer(target);
            gameObject.SetActive(true);   
        }

        public void ExtractColorFromMaterial(ColorableGroup colorable)
        {
            CustomColor = colorable.CurrentColor;

            Color.RGBToHSV(CustomColor, out Hue, out Saturation, out Brightness);
            ApplyColor();
            UpdateControls();        

            gradientControl.CalculateDraggerPosition();
            //HandleExtraProperties();
        }

        public void UpdateControls()
        {
            rgbControl.UpdateTextObjects();
            rgbControl.UpdateSliderValues();

            hsvControl.UpdateTextObjects();
            hsvControl.UpdateSliderValues();

            alphaHexControl.UpdateTextObjects();
            alphaHexControl.UpdateSliderValues();
        }

        public void ApplyColor()
        {
            gradientControl.ApplyColor();

            Target.ApplyColor(CustomColor);
        }


       /*
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
               */
    }
}