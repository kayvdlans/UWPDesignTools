using UnityEngine;

namespace ARP.UWP.Tools.ColorPicker.Controls
{
    public abstract class Control : MonoBehaviour
    {
        [SerializeField, Header("Base Control")] protected ColorPickerController colorPicker = null;

        protected bool isDraggingSlider = false;

        protected void OnEnable()
        {
            UpdateSliderValues();
            UpdateTextObjects();
        }

        public abstract void DoUpdate();
        public abstract void UpdateSliderValues();
        public abstract void UpdateTextObjects();

        public void StartDrag(GameObject dragger)
        {
            dragger.SetActive(true);
            isDraggingSlider = true;
        }

        public void StopDrag(GameObject dragger)
        {
            dragger.SetActive(false);
            isDraggingSlider = false;
            UpdateSliderValues();
        }
    }
}