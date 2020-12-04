using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using GracevaleTest.Logic.Player;
using GracevaleTest.UI;
using MVC;

namespace GracevaleTest.Logic.UI
{
    public class MarkerController : ControllerReferenceBase<PlayerState, MarkerControl>
    {
        public MarkerControl View => _view;

        protected override void InternalLoadModel(PlayerState model)
        {
            base.InternalLoadModel(model);

            var currentHp = model.Stats[StatsId.LIFE_ID];
            if (currentHp > 0)
            {
                _view.HealthBar.gameObject.SetActive(true);

                _view.HealthBar.SetMaxHP(model.Stats[StatsId.MAX_HEALTH]);
                _view.HealthBar.SetCurrentHP(currentHp);                
            }
            else
            {
                _view.HealthBar.gameObject.SetActive(false);
            }

            model.Stats.ItemChanged += Stats_ItemChanged;
        }

        protected override void InternalUnloadModel(PlayerState model)
        {
            base.InternalUnloadModel(model);

            model.Stats.ItemChanged -= Stats_ItemChanged;
        }

        private void Stats_ItemChanged(int id, float oldValue, float newValue)
        {
            if (id != StatsId.LIFE_ID)
                return;

            _view.HealthBar.SetCurrentHP(newValue);

            var difference = newValue - oldValue;
            if (Mathf.Approximately(difference, Mathf.Epsilon))
                return;

            var color = difference > 0 ? Color.green : Color.red;
            _view.AddPopupMessage(difference.ToString("F2"), color);

            if (newValue <= 0)
            {
                _view.HealthBar.gameObject.SetActive(false);
            }
        }
    }
}