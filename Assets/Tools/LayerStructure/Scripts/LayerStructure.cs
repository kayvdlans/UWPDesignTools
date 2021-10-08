using System.Collections.Generic;
using UnityEngine;

namespace ARP.UWP.Tools
{
    public class LayerStructure : MonoBehaviour
    {
        [SerializeField] private GameObject backButton = null;
        [SerializeField] private Layer baseLayer = null;

        private List<Layer> layers = new List<Layer>();
        private Layer currentLayer = null;

        private void Awake()
        {
            currentLayer = baseLayer;
            currentLayer.gameObject.SetActive(true);
        }

        public void OpenLayer(Layer gameObject)
        {
            layers.Add(currentLayer);
            currentLayer = gameObject;
            currentLayer.gameObject.SetActive(true);

            UpdateLayerData();
        }

        public void CloseCurrentLayer()
        {
            currentLayer.gameObject.SetActive(false);
            currentLayer = layers[layers.Count - 1];
            layers.Remove(currentLayer);

            UpdateLayerData();
        }

        private void UpdateLayerData()
        {
            backButton.SetActive(layers.Count > 0);

            currentLayer.UpdatePosition(0);
            currentLayer.UpdateTransparency(0);
            currentLayer.UpdateInteractability(true);
            currentLayer.SetObjectsActive(true);

            for (int i = 0; i < layers.Count; i++)
            {
                layers[i].UpdatePosition(layers.Count - i);
                layers[i].UpdateTransparency(layers.Count - i);
                layers[i].UpdateInteractability(false);
                layers[i].SetObjectsActive(false);
            }
        }
    }
}