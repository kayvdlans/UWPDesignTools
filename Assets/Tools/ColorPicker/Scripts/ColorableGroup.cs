using System.Linq;
using UnityEngine;

namespace ARP.UWP.Tools.ColorPicker
{
    public class ColorableGroup : MonoBehaviour
    {
        public delegate void ColorUpdateEvent(Color color);
        public event ColorUpdateEvent OnUpdateColor;

        [SerializeField] private ColorableData data;

        private bool initializedColor = false;

        public ColorableData Data => data;
        public Color CurrentColor { get; private set; }

        private void Awake()
        {
            foreach (ColorableObject colorable in GetComponentsInChildren<ColorableObject>().Where(other => other.Data == Data))
            {
                colorable.AttachToGroup(this);
                
                if (!initializedColor)
                {
                    CurrentColor = colorable.GetMaterialColor();
                    initializedColor = true;
                }
            }
        }

        public void ApplyColor(Color color)
        {
            CurrentColor = color;
            OnUpdateColor(color);
        }
    }
}