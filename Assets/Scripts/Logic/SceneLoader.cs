using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using GracevaleTest.Data;
using GracevaleTest.Data.Parser;
using GracevaleTest.Logic.Game;
using GracevaleTest.Logic.Player;
using GracevaleTest.Logic.UI;
using GracevaleTest.UI;

namespace GracevaleTest
{
    public class SceneLoader : MonoBehaviour
    {
        [SerializeField]
        private Canvas _canvas;

        [SerializeField]
        private MainWindow _mainWindow;

        [SerializeField]
        private MarkersWindow _markersWindow;

        [SerializeField]
        private PlayerView _leftPlayer;

        [SerializeField]
        private PlayerView _rightPlayer;

        private void Start()
        {
            var parser = new JsonParser();
            var dataProvider = new ResourcesDataProvider(parser);

            var iconsProvider = new ResourcesIconsProvider("Icons");

            var mainWindowController = new MainWindowController(dataProvider.Data, iconsProvider);
            mainWindowController.ConnectView(_mainWindow);

            var coordinatesConverter = new WorldToUiCoordinatesConverter(_canvas.GetComponent<RectTransform>(), Camera.main);
            _markersWindow.SetCoordinatesConverter(coordinatesConverter);

            var markersWindowController = new MarkersWindowController();
            markersWindowController.ConnectView(_markersWindow);

            var gameController = new GameController(dataProvider.Data, mainWindowController, markersWindowController, _leftPlayer, _rightPlayer);
            gameController.StartGame(true);
        }
    }
}