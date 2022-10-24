using Microsoft.MixedReality.Toolkit.Utilities;
using UnityEngine;

namespace SG.Tools.Utility
{
    public class ScrollableGridObjectCollection : GridObjectCollection
    {
        [SerializeField, Header("Scrolling")] private int maxVisible = 3;
        [SerializeField] private GameObject previousButton;
        [SerializeField] private GameObject nextButton;

        private int firstIndex = -1;
        private int lastIndex = -1;

        private int ChildMax => transform.childCount - maxVisible >= maxVisible ? transform.childCount - maxVisible : transform.childCount;
        private int FirstIndex
        {
            get => firstIndex;
            set
            {
                if (previousButton is null || nextButton is null)
                {
                    return;
                }

                firstIndex = Mathf.Clamp(value, 0, ChildMax);
                lastIndex = Mathf.Clamp(firstIndex + maxVisible - 1, 0, transform.childCount - 1);

                previousButton.SetActive(firstIndex > 0);
                nextButton.SetActive(lastIndex < transform.childCount - 1);

                UpdateActiveObjects();
            }
        }

        private void Awake()
        {
            DeactivateAllObjects();

            FirstIndex = 0;
        }

        public void DeactivateAllObjects()
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                transform.GetChild(i).gameObject.SetActive(false);
            }
        }

        private void UpdateActiveObjects()
        {
            int current = firstIndex;
            while (current <= lastIndex)
            {
                transform.GetChild(current++).gameObject.SetActive(true);
            }

            if (firstIndex > 0)
            {
                transform.GetChild(firstIndex - 1).gameObject.SetActive(false);
            }

            if (lastIndex < transform.childCount - 1)
            {
                transform.GetChild(lastIndex + 1).gameObject.SetActive(false);
            }

            UpdateCollection();
        }

        public void SetIndex(int index)
        {
            FirstIndex = index;
        }

        public void OnClickPrevious()
        {
            FirstIndex--;
        }

        public void OnClickNext()
        {
            FirstIndex++;
        }
    }
}