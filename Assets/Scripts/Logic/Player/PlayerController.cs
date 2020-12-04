using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using MVC;

namespace GracevaleTest.Logic.Player
{
    public class PlayerController : ControllerReferenceBase<PlayerState, PlayerView>
    {
        public PlayerState Model => _model;

        public PlayerView View => _view;

        public PlayerController Target { get; set; }

        private bool _canAttack = true;

        protected override void InternalConnectView(PlayerView view)
        {
            base.InternalConnectView(view);

            view.AttackEnded += View_AttackEnded;
        }

        protected override void InternalDisconnectView(PlayerView view)
        {
            base.InternalDisconnectView(view);

            view.AttackEnded -= View_AttackEnded;
        }

        public void Attack()
        {
            if (!_canAttack)
                return;

            _canAttack = false;

            _view.Attack();

            if (Target != null)
            {
                Target.TakeDamage(_model.Stats[StatsId.DAMAGE_ID], out var damage);

                var bloodSucker = _model.Stats[StatsId.LIFE_STEAL_ID];
                if (bloodSucker > Mathf.Epsilon)
                {
                    var heal = damage * bloodSucker / 100f;
                    var hp = _model.Stats[StatsId.LIFE_ID];

                    _model.Stats[StatsId.LIFE_ID] = Mathf.Clamp(hp + heal, 0, _model.Stats[StatsId.MAX_HEALTH]);
                }

                if (Target.Model.Stats[StatsId.LIFE_ID] < Mathf.Epsilon)
                {
                    Target = null;
                }
            }
        }

        public void TakeDamage(float attack, out float damage)
        {
            damage = attack * _model.Stats[StatsId.ARMOR_ID] / 100f;
            damage = Mathf.Clamp(damage, 0, _model.Stats[StatsId.LIFE_ID]);

            var healthLeft = _model.Stats[StatsId.LIFE_ID] -= damage;
            if (healthLeft < Mathf.Epsilon)
            {
                _canAttack = false;
                if (_view != null)
                {
                    _view.Dead();
                }
            }
        }

        public void Reset()
        {
            _canAttack = true;
        }

        private void View_AttackEnded(object sender, System.EventArgs e)
        {
            _canAttack = true;
        }
    }
}