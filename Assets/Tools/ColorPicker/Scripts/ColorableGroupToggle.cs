using Microsoft.MixedReality.Toolkit.UI;
using UnityEngine;

namespace SG.Tools.ColorPicker
{
    public class ColorableGroupToggle : MonoBehaviour
    {
        private ButtonConfigHelper buttonConfigHelper = null;

        public void SetText(string text)
        {
            if (buttonConfigHelper is null)
            {
                buttonConfigHelper = GetComponent<ButtonConfigHelper>();
            }

            buttonConfigHelper.MainLabelText = text;
        }

        public void SetIcon(Texture2D texture2D)
        {
            if (buttonConfigHelper is null)
            {
                buttonConfigHelper = GetComponent<ButtonConfigHelper>();
            }

            buttonConfigHelper.SetQuadIcon(texture2D);
        }
    }
}
