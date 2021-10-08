using ARP.UWP.Tools.Interfaces;
using DG.Tweening;
using Microsoft.MixedReality.Toolkit.UI;
using UnityEngine;

namespace ARP.UWP.Tools
{
    public class Layer : MonoBehaviour
    {
        private const float UPDATE_TIME = 0.5f;
        private const float PER_DEPTH_TRANSPARENCY_BACKPLATE = 1f / 3f;
        private const float PER_DEPTH_TRANSPARENCY_CONTENT = 1f / 2f;

        private static readonly Vector3 PER_DEPTH_TRANSLATION = new Vector3(-0.05f, 0.025f, 0.05f);

        [SerializeField] private Renderer backplate = null;

        private Interactable[] interactables = null;
        private IActivatable[] activatableObjects = null;
        private IFadeable[] fadeableObjects = null;
        private Vector3 basePosition = Vector3.zero;

        private void Awake()
        {
            interactables = GetComponentsInChildren<Interactable>();
            activatableObjects = GetComponentsInChildren<IActivatable>();
            fadeableObjects = GetComponentsInChildren<IFadeable>();
            basePosition = transform.localPosition;
        }

        public void UpdateInteractability(bool enabled)
        {
            foreach (Interactable interactable in interactables)
            {
                interactable.IsEnabled = enabled;
            }
        }

        public void UpdatePosition(int depth)
        {
            transform.DOLocalMove(basePosition + (PER_DEPTH_TRANSLATION * depth), UPDATE_TIME);
        }

        public void UpdateTransparency(int depth)
        {
            backplate.material.DOFade(1 - Mathf.Clamp01(depth * PER_DEPTH_TRANSPARENCY_BACKPLATE), UPDATE_TIME);

            foreach (IFadeable fadeable in fadeableObjects)
            {
                fadeable.Fade(depth, PER_DEPTH_TRANSPARENCY_CONTENT, UPDATE_TIME);
            }
        }

        public void SetObjectsActive(bool enabled)
        {
            foreach (IActivatable activatable in activatableObjects)
            {
                activatable.SetActive(enabled);
            }
        }
    }
}