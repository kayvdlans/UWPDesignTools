using UnityEngine;

namespace ARP.UWP.Tools
{
    public class ColorableGroup : MonoBehaviour
    {
        public enum ObjectType
        {
            Frontplate, //Hide Alpha, Show Extra Properties: Border Light Brightness (%)
            Backplate,  //Hide Alpha, Show Extra Properties: Border Light Brightness (%), Iridescence Intensity (%)
            Text,       //Show Alpha, Other Color Property
            Icon        //Hide Alpha
        }

        [SerializeField] private ObjectType type;
        [SerializeField] private string labelName;
        [SerializeField] private Renderer[] renderers = null;

        public ObjectType Type => type;
        public string LabelName => labelName == "" ? name : labelName;
        public Renderer[] Renderers => renderers;
        public bool HasTransparency => type == ObjectType.Text;
        public bool HasExtraProperties => type == ObjectType.Frontplate || type == ObjectType.Backplate;
        public bool HasBorderLight => type == ObjectType.Frontplate || type == ObjectType.Backplate;
        public bool HasIridescence => type == ObjectType.Backplate;
    }
}