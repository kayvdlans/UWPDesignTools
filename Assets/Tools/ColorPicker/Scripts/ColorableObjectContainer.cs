using Microsoft.MixedReality.Toolkit.UI;
using Microsoft.MixedReality.Toolkit.Utilities;
using UnityEngine;

namespace ARP.UWP.Tools
{
    public class ColorableObjectContainer : MonoBehaviour
    {
        [SerializeField] private GridObjectCollection buttonCollection;
        [SerializeField] private Interactable selectButtonPrefab;

        private ColorPicker colorPicker = null;
        private ColorableGroup[] colorables = null;
        private Interactable[] interactables = null;

        private void Awake()
        {
            colorPicker = GetComponent<ColorPicker>();
        }

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
                interactable.GetComponent<ButtonConfigHelper>().MainLabelText = colorables[i].LabelName;
                interactable.OnClick.AddListener(() =>
                {
                    UntoggleAll();
                    interactable.IsToggled = true;
                    colorPicker.SummonColorPicker(current);
                });

                interactables[i] = interactable;
            }

            interactables[0].TriggerOnClick();

            Invoke(nameof(UpdateCollection), 0.0f);
        }

        private void UpdateCollection()
        {
            buttonCollection.UpdateCollection();
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