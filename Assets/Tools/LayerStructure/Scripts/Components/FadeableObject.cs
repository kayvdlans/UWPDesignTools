using ARP.UWP.Tools.Interfaces;
using DG.Tweening;
using UnityEngine;

namespace ARP.UWP.Tools.Components
{
    public class FadeableObject : MonoBehaviour, IFadeable
    {
        private new Renderer renderer = null;

        public void Fade(int depth, float step, float time)
        {
            if (renderer is null)
            {
                renderer = GetComponent<Renderer>();
            }

            renderer.material.DOFade(1 - Mathf.Clamp01(depth * step), time);
        }
    }
}