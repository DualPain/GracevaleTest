using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using GracevaleTest.Logic.Player;
using GracevaleTest.UI;
using MVC;

namespace GracevaleTest.Logic.UI
{
    public struct MainWindowModel
    {
        public PlayerController LeftPlayer;
        public PlayerController RightPlayer;
    }

    public class MainWindowController : ControllerValueBase<MainWindowModel, MainWindow>
    {
        private PlayerPanelController _leftPlayerPanelController;
        private PlayerPanelController _rightPlayerPanelController;

        public event EventHandler<bool> RestartGame;

        public MainWindowController(Data.Data data, IIconsProvider iconsProvider)
        {
            _leftPlayerPanelController = new PlayerPanelController(data, iconsProvider);
            _rightPlayerPanelController = new PlayerPanelController(data, iconsProvider);
        }

        protected override void InternalLoadModel(MainWindowModel model)
        {
            base.InternalLoadModel(model);

            _leftPlayerPanelController.LoadModel(model.LeftPlayer);
            _rightPlayerPanelController.LoadModel(model.RightPlayer);
        }

        protected override void InternalUnloadModel(MainWindowModel model)
        {
            base.InternalUnloadModel(model);

            _leftPlayerPanelController.UnloadModel();
            _rightPlayerPanelController.UnloadModel();
        }

        protected override void InternalConnectView(MainWindow view)
        {
            base.InternalConnectView(view);

            _leftPlayerPanelController.ConnectView(view.LeftPlayerPanel);
            _rightPlayerPanelController.ConnectView(view.RightPlayerPanel);

            view.RestartButtonClicked += View_RestartButtonClicked;
        }
       
        protected override void InternalDisconnectView(MainWindow view)
        {
            base.InternalDisconnectView(view);

            _leftPlayerPanelController.DisconnectView();
            _rightPlayerPanelController.DisconnectView();

            view.RestartButtonClicked -= View_RestartButtonClicked;
        }

        private void View_RestartButtonClicked(object sender, bool useBuffs)
        {
            RestartGame?.Invoke(this, useBuffs);
        }

    }
}