using ARP.UWP.Tools.Interfaces;
using UnityEngine;

namespace ARP.UWP.Tools.Components
{
    public class ActivatableObject : MonoBehaviour, IActivatable
    {
        public void SetActive(bool active)
        {
            gameObject.SetActive(active);
        }
    }
}