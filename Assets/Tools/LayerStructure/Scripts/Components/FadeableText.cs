using ARP.UWP.Tools.Interfaces;
using DG.Tweening;
using TMPro;
using UnityEngine;

namespace ARP.UWP.Tools.Components
{
    public class FadeableText : MonoBehaviour, IFadeable
    {
        private TMP_Text text = null;

        public void Fade(int depth, float step, float time)
        {
            if (text is null)
            {
                text = GetComponent<TMP_Text>();
            }

            text.DOFade(1 - Mathf.Clamp01(depth * step), time);
        }
    }
}