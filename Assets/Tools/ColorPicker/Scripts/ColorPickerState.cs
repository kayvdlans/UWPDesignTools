using Microsoft.MixedReality.Toolkit.UI;
using UnityEngine;

namespace ARP.UWP.Tools
{
    [RequireComponent(typeof(ColorPicker))]
    public class ColorPickerState : MonoBehaviour
    {
        public enum State
        {
            Gradient,
            RGB,
            HSV,
            Extra
        }

        [SerializeField, Header("Toggle Buttons")] private Interactable toggleGradient = null;
        [SerializeField] private Interactable toggleRGB = null;
        [SerializeField] private Interactable toggleHSV = null;
        [SerializeField] private Interactable toggleExtra = null;

        [SerializeField, Header("Menus")] private GameObject menuGradient = null;
        [SerializeField] private GameObject menuRGB = null;
        [SerializeField] private GameObject menuHSV = null;
        [SerializeField] private GameObject menuAlpha = null;
        [SerializeField] private GameObject menuExtra = null;

        private ColorPicker colorPicker = null;
        private State currentState = State.Gradient;

        private void Awake()
        {
            colorPicker = GetComponent<ColorPicker>();
            colorPicker.OnUpdateTarget += OnUpdateTarget;
        }

        private void OnUpdateTarget(ColorableGroup target)
        {
            if (currentState == State.RGB || currentState == State.HSV)
            {
                menuAlpha.SetActive(true);
            }
        }

        public void UpdateState(int index)
        {
            UpdateState((State)index);
        }

        public void UpdateState(State state)
        {
            currentState = state;

            UpdateToggles();
            DisableAllMenus();
            EnableMenuByState(currentState);
        }

        private void UpdateToggles()
        {
            toggleGradient.IsToggled = currentState == State.Gradient;
            toggleRGB.IsToggled = currentState == State.RGB;
            toggleHSV.IsToggled = currentState == State.HSV;
            toggleExtra.IsToggled = currentState == State.Extra;
        }

        private void EnableMenuByState(State state)
        {
            switch (state)
            {
                case State.Gradient:
                    menuGradient.SetActive(true);
                    break;
                case State.RGB:
                    menuRGB.SetActive(true);
                    menuAlpha.SetActive(true);
                    break;
                case State.HSV:
                    menuHSV.SetActive(true);
                    menuAlpha.SetActive(true);
                    break;
                case State.Extra:
                    menuExtra.SetActive(true);
                    break;
            }
        }

        private void DisableAllMenus()
        {
            menuGradient.SetActive(false);
            menuRGB.SetActive(false);
            menuHSV.SetActive(false);
            menuAlpha.SetActive(false);
            menuExtra.SetActive(false);
        }

        private void OnDestroy()
        {
            colorPicker.OnUpdateTarget -= OnUpdateTarget;
        }
    }
}