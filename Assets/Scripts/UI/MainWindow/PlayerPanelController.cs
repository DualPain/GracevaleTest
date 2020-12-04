using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

using GracevaleTest.Logic.Player;
using GracevaleTest.UI;
using MVC;

namespace GracevaleTest.Logic.UI
{
    public class PlayerPanelController : ControllerReferenceBase<PlayerController, PlayerPanelHierarchy>
    {
        private Data.Data _data;
        private IIconsProvider _iconsProvider;

        private readonly Dictionary<int, StatControl> _statControlsDictionary = new Dictionary<int, StatControl>();
        private readonly List<StatControl> _buffControlsList = new List<StatControl>();

        public PlayerPanelController(Data.Data data, IIconsProvider iconsProvider)
        {
            _data = data;
            _iconsProvider = iconsProvider;
        }

        protected override void InternalLoadModel(PlayerController model)
        {
            base.InternalLoadModel(model);

            foreach (var statData in _data.stats)
            {
                var statControl = _view.AddStatControl();

                var icon = _iconsProvider.GetIcon(statData.icon);
                statControl.SetIcon(icon);

                _statControlsDictionary[statData.id] = statControl;

                var statValue = model.Model.Stats[statData.id];
                SetStatControlText(statControl, statValue);

                statControl.transform.SetAsLastSibling();
            }

            foreach (var buffId in model.Model.Buffs)
            {
                var statControl = _view.AddStatControl();

                var buffData = _data.buffs.Single(b => b.id == buffId);

                var icon = _iconsProvider.GetIcon(buffData.icon);
                statControl.SetIcon(icon);
                statControl.SetText(buffData.title);

                statControl.transform.SetAsLastSibling();

                _buffControlsList.Add(statControl);
            }

            model.Model.Stats.ItemChanged += Stats_ItemChanged;
        }

        protected override void InternalUnloadModel(PlayerController model)
        {
            base.InternalUnloadModel(model);

            foreach (var statControl in _statControlsDictionary.Values)
            {
                _view.RemoveStatControl(statControl);
            }

            _statControlsDictionary.Clear();

            foreach (var statControl in _buffControlsList)
            {
                _view.RemoveStatControl(statControl);
            }

            _buffControlsList.Clear();

            model.Model.Stats.ItemChanged -= Stats_ItemChanged;
        }

        protected override void InternalConnectView(PlayerPanelHierarchy view)
        {
            base.InternalConnectView(view);

            view.AttackButtonClicked += View_AttackButtonClicked;
        }

        protected override void InternalDisconnectView(PlayerPanelHierarchy view)
        {
            base.InternalDisconnectView(view);

            view.AttackButtonClicked -= View_AttackButtonClicked;
        }

        private void View_AttackButtonClicked(object sender, EventArgs e)
        {
            if (_model == null)
                return;

            _model.Attack();
        }

        private void Stats_ItemChanged(int key, float oldValue, float value)
        {
            SetStatControlText(key, value);
        }

        private void SetStatControlText(int key, float value)
        {
            var statControl = _statControlsDictionary[key];
            SetStatControlText(statControl, value);
        }

        private void SetStatControlText(StatControl control, float value)
        {
            control.SetText(value.ToString("F2"));
        }
    }
}