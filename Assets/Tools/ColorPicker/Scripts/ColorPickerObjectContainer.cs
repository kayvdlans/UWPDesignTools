using Microsoft.MixedReality.Toolkit.UI;
using SG.Tools.Utility;
using UnityEngine;

namespace SG.Tools.ColorPicker
{
    public class ColorPickerObjectContainer : MonoBehaviour
    {
        [SerializeField] private ColorPickerController colorPicker;
        [SerializeField] private ScrollableGridObjectCollection buttonCollection;
        [SerializeField] private Interactable selectButtonPrefab;

        private ColorableGroup[] colorables = null;
        private Interactable[] interactables = null;

        public void SummonContainer(GameObject container)
        {
            RemoveLingeringObjects();

            colorables = container.GetComponentsInChildren<ColorableGroup>(false);
            interactables = new Interactable[colorables.Length];

            CreateSelectButtons();
        }

        private void RemoveLingeringObjects()
        {
            for (int i = buttonCollection.transform.childCount - 1; i >= 0; i--)
            {
                Destroy(buttonCollection.transform.GetChild(i).gameObject);
            }

            colorables = null;
            interactables = null;
        }

        private void CreateSelectButtons()
        {
            for (int i = 0; i < colorables.Length; i++)
            {
                ColorableGroup current = colorables[i];
                Interactable interactable = Instantiate(selectButtonPrefab, buttonCollection.transform);
                interactable.OnClick.AddListener(() =>
                {
                    UntoggleAll();
                    interactable.IsToggled = true;
                    colorPicker.Target = current;
                });

                ColorableGroupToggle groupToggle = interactable.GetComponent<ColorableGroupToggle>();
                groupToggle.SetIcon(current.Data.icon);
                groupToggle.SetText(current.Data.label);

                interactables[i] = interactable;
            }

            interactables[0].TriggerOnClick();

            Invoke(nameof(UpdateCollection), 0.0f);
        }

        private void UpdateCollection()
        {
            buttonCollection.SetIndex(0);
        }

        private void UntoggleAll()
        {
            foreach (Interactable interactable in interactables)
            {
                interactable.IsToggled = false;
            }
        }
    }
}