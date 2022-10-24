using UnityEngine;

namespace SG.Tools.ColorPicker.Data
{
    [CreateAssetMenu(menuName = "ARP/Tools/ColorPicker/ColorableData")]
    public class ColorableData : ScriptableObject
    {
        public string label;
        public Texture2D icon;

        //Relevant for Text Objects that use _FaceColor instead of _Color
        public bool usesAlternateShaderColor = false;
        public string shaderMainColorName = "_Color";

        //TODO: FIX property control to add sliders based on properties. use ScrollableGridObjectCollection
        public bool enableExtraProperties = false;
        public string[] propertyNames = new string[0];
    }
}