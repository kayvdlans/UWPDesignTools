using SG.Tools.LayeredMenu.Interfaces;
using UnityEngine;

namespace SG.Tools.LayeredMenu.Components
{
    public class ActivatableObject : MonoBehaviour, IActivatable
    {
        public void SetActive(bool active)
        {
            gameObject.SetActive(active);
        }
    }
}