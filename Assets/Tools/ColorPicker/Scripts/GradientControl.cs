using Microsoft.MixedReality.Toolkit.Input;
using UnityEngine;

namespace ARP.UWP.Tools
{
    public class GradientControl : MonoBehaviour, IMixedRealityTouchHandler
    {
        private const float GRADIENT_DRAG_MAX_DISTANCE = 0.5f;

        [SerializeField] private MeshRenderer gradientMesh = null;
        [SerializeField] private GameObject gradientDragger = null;

        private ColorPicker colorPicker = null;

        private Vector3 gradientDragStartPosition;
        private Vector3 gradientDragCurrentPosition;
        private bool isDragging = false;

        private void Awake()
        {
            colorPicker = GetComponent<ColorPicker>();

            gradientDragStartPosition = gradientDragger.transform.localPosition;
            gradientDragCurrentPosition = gradientDragStartPosition;
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
            colorPicker.UpdateSliderText();
            colorPicker.ApplySliderValues();
        }

        public void CalculateDraggerPosition()
        {
            float xPosition = ((colorPicker.Saturation + GRADIENT_DRAG_MAX_DISTANCE) * -1) + 1;
            float yPosition = colorPicker.Brightness - GRADIENT_DRAG_MAX_DISTANCE;
            gradientDragCurrentPosition.x = Mathf.Clamp(xPosition, -GRADIENT_DRAG_MAX_DISTANCE, GRADIENT_DRAG_MAX_DISTANCE);
            gradientDragCurrentPosition.y = Mathf.Clamp(yPosition, -GRADIENT_DRAG_MAX_DISTANCE, GRADIENT_DRAG_MAX_DISTANCE);
            gradientDragger.transform.localPosition = gradientDragCurrentPosition;
        }

        private void ConstrainDragging()
        {
            // Horizontal
            {
                if (gradientDragger.transform.localPosition.x >= gradientDragStartPosition.x + GRADIENT_DRAG_MAX_DISTANCE)
                {
                    gradientDragCurrentPosition.x = gradientDragStartPosition.x + GRADIENT_DRAG_MAX_DISTANCE;
                }
                else if (gradientDragger.transform.localPosition.x <= gradientDragStartPosition.x - GRADIENT_DRAG_MAX_DISTANCE)
                {
                    gradientDragCurrentPosition.x = gradientDragStartPosition.x - GRADIENT_DRAG_MAX_DISTANCE;
                }
                else
                {
                    gradientDragCurrentPosition.x = gradientDragger.transform.localPosition.x;
                }
            }

            //Vertical
            {
                if (gradientDragger.transform.localPosition.y >= gradientDragStartPosition.y + GRADIENT_DRAG_MAX_DISTANCE)
                {
                    gradientDragCurrentPosition.y = gradientDragStartPosition.y + GRADIENT_DRAG_MAX_DISTANCE;
                }
                else if (gradientDragger.transform.localPosition.y <= gradientDragStartPosition.y - GRADIENT_DRAG_MAX_DISTANCE)
                {
                    gradientDragCurrentPosition.y = gradientDragStartPosition.y - GRADIENT_DRAG_MAX_DISTANCE;
                }
                else
                {
                    gradientDragCurrentPosition.y = gradientDragger.transform.localPosition.y;
                }
            }

            gradientDragger.transform.localPosition = gradientDragCurrentPosition;
            colorPicker.Saturation = Mathf.Abs(gradientDragCurrentPosition.x + (GRADIENT_DRAG_MAX_DISTANCE * -1));
            colorPicker.Brightness = gradientDragCurrentPosition.y + GRADIENT_DRAG_MAX_DISTANCE;

            Color color = Color.HSVToRGB(colorPicker.Hue, colorPicker.Saturation, colorPicker.Brightness);
            color.a = colorPicker.Alpha;
            colorPicker.CustomColor = color;

            colorPicker.UpdateSliderText();
            colorPicker.ApplyColor();
        }

        public void ClickGradientTexture(MixedRealityPointerEventData eventData)
        {
            gradientDragger.transform.position = eventData.Pointer.Result.Details.Point;
            ConstrainDragging();
            colorPicker.ApplyColor();
            colorPicker.ApplySliderValues();
            colorPicker.UpdateSliderText();
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
