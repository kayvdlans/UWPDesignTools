using SG.Tools.ColorPicker.Data;
using UnityEngine;

namespace SG.Tools.ColorPicker
{
    public class ColorableObject : MonoBehaviour
    {
        [SerializeField] private ColorableData data;

        private new Renderer renderer = null;
        private ColorableGroup colorableGroup = null;

        public ColorableData Data => data;

        public void AttachToGroup(ColorableGroup group)
        {
            colorableGroup = group;
            colorableGroup.OnUpdateColor += OnColorUpdated;
        }

        public Color GetMaterialColor()
        {
            if (renderer is null)
            {
                renderer = GetComponent<Renderer>();
            }

            return data.usesAlternateShaderColor ? renderer.material.GetColor(data.shaderMainColorName) : renderer.material.color;
        }

        private void OnColorUpdated(Color color)
        {
            if (data.usesAlternateShaderColor)
            {
                renderer.material.SetColor(data.shaderMainColorName, color);
            }
            else
            {
                renderer.material.color = color;
            }
        }

        private void OnDestroy()
        {
            colorableGroup.OnUpdateColor -= OnColorUpdated;
        }
    }
}