using Microsoft.MixedReality.Toolkit.Input;
using UnityEngine;

namespace ARP.UWP.Tools.Utility
{
    public class ButtonSizer : MonoBehaviour
    {
        private const string FRONTPLATE_PARENT_NAME = "CompressableButtonVisuals";
        private const string BACKPLATE_PARENT_NAME = "BackPlate";
        private const string TOGGLE_BACKPLATE_PARENT_NAME = "BackPlateToggleState";

        [SerializeField] private Vector2 scale = new Vector2(.032f, .032f);
        [SerializeField] private bool hasToggleState;

        private Transform frontplate = null;
        private Transform backplate = null;
        private Transform toggleBackplate = null;
        private new BoxCollider collider = null;
        private NearInteractionTouchable touchable = null;

        private float frontplateZScale = 1f;
        private float backplateZScale = 1f;
        private float toggleBackplateZScale = 1f;

        private void FindReferences()
        {
            frontplate = transform.Find(FRONTPLATE_PARENT_NAME).GetChild(0);
            frontplateZScale = frontplate.localScale.z;

            backplate = transform.Find(BACKPLATE_PARENT_NAME).GetChild(0);
            backplateZScale = backplate.localScale.z;

            if (hasToggleState)
            {
                toggleBackplate = transform.Find(TOGGLE_BACKPLATE_PARENT_NAME).GetChild(0);
                toggleBackplateZScale = toggleBackplate.localScale.z;
            }

            collider = GetComponent<BoxCollider>();
            touchable = GetComponent<NearInteractionTouchable>();
        }

        public void ApplyProperties()
        {
            FindReferences();

            frontplate.localScale = new Vector3(scale.x, scale.y, frontplateZScale);
            backplate.localScale = new Vector3(scale.x, scale.y, backplateZScale);

            if (hasToggleState)
            {
                toggleBackplate.localScale = new Vector3(scale.x, scale.y, toggleBackplateZScale);
            }

            collider.size = new Vector3(scale.x, scale.y, collider.size.z);
            touchable.SetTouchableCollider(collider);
        }
    }
}