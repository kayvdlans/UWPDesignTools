using TMPro;
using UnityEngine;

namespace SG.Tools.LayeredMenu.Utility
{
    public class TextSetter : MonoBehaviour
    {
        [SerializeField] private TMP_Text text = null;

        public void SetText(string text)
        {
            this.text.SetText(text);
        }
    }
}
