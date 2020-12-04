using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using GracevaleTest.Logic.Player;
using GracevaleTest.UI;
using MVC;

namespace GracevaleTest.Logic.UI
{
    public struct MarkersWindowModel
    {
        public PlayerController[] Players;
    }

    public class MarkersWindowController : ControllerValueBase<MarkersWindowModel, MarkersWindow>
    {
        private readonly Dictionary<PlayerController, MarkerController> _markersDictionary = new Dictionary<PlayerController, MarkerController>();

        protected override void InternalLoadModel(MarkersWindowModel model)
        {
            base.InternalLoadModel(model);

            foreach (var player in model.Players)
            {
                var markerController = new MarkerController();                    

                var marker = _view.AddMarker();

                marker.SetPositionProvider(player.View);

                markerController.ConnectView(marker);
                markerController.LoadModel(player.Model);

                _markersDictionary.Add(player, markerController);
            }
        }

        protected override void InternalUnloadModel(MarkersWindowModel model)
        {
            base.InternalUnloadModel(model);

            foreach (var pair in _markersDictionary)
            {
                var markerController = pair.Value;

                var markerView = markerController.View;
                markerView.SetPositionProvider(null);

                markerController.UnloadModel();
                markerController.DisconnectView();                

                _view.RemoveMarker(markerView);
            }

            _markersDictionary.Clear();
        }
    }
}