using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using GracevaleTest.Logic.Player;

namespace GracevaleTest.UI
{
    public class MarkerControl : MonoBehaviour
    {
        [SerializeField]
        private HealthBarControl _healthBar;

        [SerializeField]
        private Transform _popupsParent;

        [SerializeField]
        private GameObject _popupPrefab;

        private WorldToUiCoordinatesConverter _coordinatesConverter;
        private IMarkerPositionProvider _positionProvider;

        private PopupMessageControlPool _popupPool;

        private readonly List<PopupMessageControl> _popupsList = new List<PopupMessageControl>();

        public HealthBarControl HealthBar => _healthBar;

        private void Awake()
        {
            _popupPool = new PopupMessageControlPool(_popupPrefab, _popupsParent);
        }

        private void Update()
        {
            if (_positionProvider == null || _coordinatesConverter == null)
                return;

            transform.localPosition = _coordinatesConverter.GetUiPosition(_positionProvider.MarkerWorldPosition);
        }

        public void AddPopupMessage(string text, Color color)
        {
            var popupMessage = _popupPool.PopItem(_popupsParent);
            popupMessage.transform.localPosition = Vector3.zero;

            popupMessage.SetText(text);
            popupMessage.SetColor(color);

            popupMessage.AnimationEnded += PopupMessage_AnimationEnded;

            _popupsList.Add(popupMessage);
        }

        public void ClearMessages()
        {
            foreach (var popup in _popupsList)
            {
                UninitPopup(popup);
            }

            _popupsList.Clear();
        }

        public void SetCoordinatesConverter(WorldToUiCoordinatesConverter coordinatesConverter)
        {
            _coordinatesConverter = coordinatesConverter;
        }

        public void SetPositionProvider(IMarkerPositionProvider positionProvider)
        {
            _positionProvider = positionProvider;
        }

        private void PopupMessage_AnimationEnded(object sender, System.EventArgs e)
        {
            var popupMessage = sender as PopupMessageControl;
            UninitPopup(popupMessage);

            _popupsList.Remove(popupMessage);
        }

        private void UninitPopup(PopupMessageControl control)
        {
            control.AnimationEnded -= PopupMessage_AnimationEnded;
            _popupPool.PushItem(control);
        }
    }
}