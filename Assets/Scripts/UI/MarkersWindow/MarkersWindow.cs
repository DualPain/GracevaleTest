using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GracevaleTest.UI
{
    public class MarkersWindow : MonoBehaviour
    {
        [SerializeField]
        private Transform _markerParent;

        [SerializeField]
        private GameObject _markerPrefab;

        private MarkerControlPool _markersPool;
        private WorldToUiCoordinatesConverter _coordinatesConverter;

        private void Awake()
        {
            _markersPool = new MarkerControlPool(_markerPrefab, _markerParent);
        }

        public void SetCoordinatesConverter(WorldToUiCoordinatesConverter coordinatesConverter)
        {
            _coordinatesConverter = coordinatesConverter;
        }

        public MarkerControl AddMarker()
        {
            var marker = _markersPool.PopItem(_markerParent);
            marker.SetCoordinatesConverter(_coordinatesConverter);
            return marker;
        }

        public void RemoveMarker(MarkerControl control)
        {
            _markersPool.PushItem(control);
        }
    }
}