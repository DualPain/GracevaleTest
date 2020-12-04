using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GracevaleTest.Logic.Player
{
    public interface IMarkerPositionProvider
    {
        Vector3 MarkerWorldPosition { get; }
    }

    public class PlayerView : MonoBehaviour, IMarkerPositionProvider
    {
        [SerializeField]
        private Animator _animator;

        [SerializeField]
        private Transform _markerPosition;

        public event EventHandler AttackEnded;

        public Vector3 MarkerWorldPosition => _markerPosition.position;

        public void Attack()
        {
            _animator.SetTrigger("Attack");
        }

        public void Dead()
        {
            _animator.SetBool("Dead", true);
        }

        public void Reset()
        {
            _animator.SetBool("Dead", false);
        }

        //call from animation event
        private void AttackEndedHandler()
        {
            AttackEnded?.Invoke(this, EventArgs.Empty);
        }
    }
}