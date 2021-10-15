using Microsoft.MixedReality.Toolkit.Input;
using UnityEngine;

namespace ARP.UWP.Tools
{
    public class GradientControl : MonoBehaviour, IMixedRealityTouchHandler
    {
        private const float MAX_DRAG_DIST = 0.5f;

        [SerializeField] private MeshRenderer gradientMesh = null;
        [SerializeField] private GameObject gradientDragger = null;
        [SerializeField] private ColorPicker colorPicker = null;

        private Vector3 dragStartPos;
        private Vector3 dragCurrentPos;
        private bool isDragging = false;

        private void Awake()
        {
            dragStartPos = gradientDragger.transform.localPosition;
            dragCurrentPos = dragStartPos;
        }

        private void OnEnable()
        {
            CalculateDraggerPosition();
        }

        private void Update()
        {
            if (isDragging)
            {
                ConstrainDragging();
            }
        }

        public void OnTouchCompleted(HandTrackingInputEventData eventData)
        {
        }

        public void OnTouchStarted(HandTrackingInputEventData eventData)
        {
        }

        public void OnTouchUpdated(HandTrackingInputEventData eventData)
        {
            gradientDragger.transform.position = eventData.InputData;
            ConstrainDragging();
            colorPicker.ApplyColor();
            colorPicker.UpdateControls();
        }

        public void CalculateDraggerPosition()
        {
            float xPosition = ((colorPicker.Saturation + MAX_DRAG_DIST) * -1) + 1;
            float yPosition = colorPicker.Brightness - MAX_DRAG_DIST;
            dragCurrentPos.x = Mathf.Clamp(xPosition, -MAX_DRAG_DIST, MAX_DRAG_DIST);
            dragCurrentPos.y = Mathf.Clamp(yPosition, -MAX_DRAG_DIST, MAX_DRAG_DIST);
            gradientDragger.transform.localPosition = dragCurrentPos;
        }

        private void ConstrainDragging()
        {
            Vector3 pos = gradientDragger.transform.localPosition;
            
            //Horizontal
            if (gradientDragger.transform.localPosition.x >= dragStartPos.x + MAX_DRAG_DIST)
            {
                dragCurrentPos.x = dragStartPos.x + MAX_DRAG_DIST;
            }
            else if (gradientDragger.transform.localPosition.x <= dragStartPos.x - MAX_DRAG_DIST)
            {
                dragCurrentPos.x = dragStartPos.x - MAX_DRAG_DIST;
            }
            else
            {
                dragCurrentPos.x = gradientDragger.transform.localPosition.x;
            }

            //Vertical
            if (gradientDragger.transform.localPosition.y >= dragStartPos.y + MAX_DRAG_DIST)
            {
                dragCurrentPos.y = dragStartPos.y + MAX_DRAG_DIST;
            }
            else if (gradientDragger.transform.localPosition.y <= dragStartPos.y - MAX_DRAG_DIST)
            {
                dragCurrentPos.y = dragStartPos.y - MAX_DRAG_DIST;
            }
            else
            {
                dragCurrentPos.y = gradientDragger.transform.localPosition.y;
            }

            gradientDragger.transform.localPosition = dragCurrentPos;

            colorPicker.Saturation = Mathf.Abs(dragCurrentPos.x + (MAX_DRAG_DIST * -1));
            colorPicker.Brightness = dragCurrentPos.y + MAX_DRAG_DIST;

            Color color = Color.HSVToRGB(colorPicker.Hue, colorPicker.Saturation, colorPicker.Brightness);
            colorPicker.CustomColor.r = color.r;
            colorPicker.CustomColor.g = color.g;
            colorPicker.CustomColor.b = color.b;

            colorPicker.UpdateControls();
            colorPicker.ApplyColor();
        }

        public void ClickGradientTexture(MixedRealityPointerEventData eventData)
        {
            gradientDragger.transform.position = eventData.Pointer.Result.Details.Point;
            ConstrainDragging();
            colorPicker.ApplyColor();
            colorPicker.UpdateControls();
        }

        public void StartDrag()
        {
            isDragging = true;
        }

        public void StopDrag()
        {
            isDragging = false;
        }

        public void ApplyColor()
        {
            if (gradientMesh != null && gradientMesh.material != null)
            {
                gradientMesh.material.color = Color.HSVToRGB(colorPicker.Hue, 1, 1);
            }
        }
    }
}
