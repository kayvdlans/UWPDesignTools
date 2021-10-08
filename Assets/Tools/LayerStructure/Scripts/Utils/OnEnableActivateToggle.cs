using Microsoft.MixedReality.Toolkit.UI;
using UnityEngine;

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
