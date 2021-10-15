using Microsoft.MixedReality.Toolkit;
using Microsoft.MixedReality.Toolkit.Input;
using Microsoft.MixedReality.Toolkit.Rendering;
using Microsoft.MixedReality.Toolkit.UI;
using TMPro;
using UnityEngine;

namespace ARP.UWP.Tools
{
    public class ColorPicker : MonoBehaviour
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

        [HideInInspector] public Color CustomColor;
        [HideInInspector] public float Hue, Saturation, Brightness, Alpha = 1f;

        private void Start()
        {
            gameObject.SetActive(false);
        }

        public void SummonColorPicker(ColorableGroup colorable)
        {
            gameObject.SetActive(true);
            
            Target = colorable;
            ExtractColorFromMaterial(colorable);
        }

        public void ExtractColorFromMaterial(ColorableGroup colorable)
        {
            if (colorable.Type == ColorableGroup.ObjectType.Text)
            {
                CustomColor = colorable.Renderers[0].material.GetColor("_FaceColor");
            }
            else
            {
                CustomColor = colorable.Renderers[0].material.color;
            }

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

            foreach (Renderer renderer in Target.Renderers)
            {
                if (renderer.material != null)
                {
                    if (Target.Type == ColorableGroup.ObjectType.Text)
                    {
                        renderer.material.SetColor("_FaceColor", CustomColor);
                    }
                    else
                    {
                        renderer.material.color = CustomColor;
                    }
                }
            }
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