using System;
using UnityEngine;
using UnityEngine.UI;

namespace GracevaleTest.UI
{
    public class PlayerPanelHierarchy : MonoBehaviour
    {
        [SerializeField]
        private Button _attackButton;

        [SerializeField]
        private Transform _statParent;

        [SerializeField]
        private GameObject _statPrefab;

        public event EventHandler AttackButtonClicked;

        private StatControlPool _statControlPool;

        private void Awake()
        {
            _statControlPool = new StatControlPool(_statPrefab, _statParent);

            _attackButton.onClick.AddListener(() => AttackButtonClicked?.Invoke(this, EventArgs.Empty));
        }

        public StatControl AddStatControl()
        {
            var statControl = _statControlPool.PopItem(_statParent);
            return statControl;
        }

        public void RemoveStatControl(StatControl statControl)
        {
            _statControlPool.PushItem(statControl);
        }
    }
}