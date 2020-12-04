using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GracevaleTest.UI
{
    public class WorldToUiCoordinatesConverter
    {
        private RectTransform _canvas;
        private Camera _worldCamera;

        public WorldToUiCoordinatesConverter(RectTransform canvas, Camera worldCamera)
        {
            _canvas = canvas;
            _worldCamera = worldCamera;
        }

        public Vector2 GetUiPosition(Vector3 worldPosition)
        {
            var viewport_position = _worldCamera.WorldToViewportPoint(worldPosition);
            var canvas_rect = _canvas.GetComponent<RectTransform>();

            return new Vector2((viewport_position.x * canvas_rect.sizeDelta.x) - (canvas_rect.sizeDelta.x * 0.5f),
                               (viewport_position.y * canvas_rect.sizeDelta.y) - (canvas_rect.sizeDelta.y * 0.5f));
        }
    }
}