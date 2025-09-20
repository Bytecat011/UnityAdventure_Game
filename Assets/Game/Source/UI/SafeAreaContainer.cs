using System;
using UnityEngine;

namespace Game.UI
{
    [RequireComponent(typeof(RectTransform))]
    public class SafeAreaContainer : MonoBehaviour
    {
        private RectTransform _rectTransform;
        private Rect _lastSafeArea;

        private void Awake()
        {
            _rectTransform = GetComponent<RectTransform>();
            UpdateSafeArea();
        }

        private void Update()
        {
            if (Screen.safeArea != _lastSafeArea)
                UpdateSafeArea();
        }

        private void UpdateSafeArea()
        {
            _lastSafeArea = Screen.safeArea;
            
            Vector2 anchorMin = _lastSafeArea.position;
            Vector2 anchorMax = _lastSafeArea.position + _lastSafeArea.size;
            
            anchorMin.x /= Screen.width;
            anchorMin.y /= Screen.height;
            anchorMax.x /= Screen.width;
            anchorMax.y /= Screen.height;
            
            _rectTransform.anchorMin = anchorMin;
            _rectTransform.anchorMax = anchorMax;
        }
    }
}