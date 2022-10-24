using Microsoft.MixedReality.Toolkit.UI;
using UnityEngine;

namespace SG.Tools.LayeredMenu.Utility
{
    public class OnEnableActivateToggle : MonoBehaviour
    {
        private Interactable interactable = null;

        private void Awake()
        {
            interactable = GetComponent<Interactable>();
        }

        private void OnEnable()
        {
            interactable.IsToggled = true;
        }
    }
}