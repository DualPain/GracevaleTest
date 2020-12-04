using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace GracevaleTest.UI
{
    public class MainWindow : MonoBehaviour
    {
        [SerializeField]
        private PlayerPanelHierarchy _leftPlayerPanel;

        [SerializeField]
        private PlayerPanelHierarchy _rightPlayerPanel;

        [SerializeField]
        private Button _restartWithoutBuffsButton;

        [SerializeField]
        private Button _restartWithBuffsButton;

        public event EventHandler<bool> RestartButtonClicked;

        public PlayerPanelHierarchy LeftPlayerPanel => _leftPlayerPanel;
        public PlayerPanelHierarchy RightPlayerPanel => _rightPlayerPanel;

        private void Awake()
        {
            _restartWithoutBuffsButton.onClick.AddListener(() => OnRestart(false));
            _restartWithBuffsButton.onClick.AddListener(() => OnRestart(true));
        }

        private void OnRestart(bool withBuffs)
        {
            RestartButtonClicked?.Invoke(this, withBuffs);
        }
    }
}