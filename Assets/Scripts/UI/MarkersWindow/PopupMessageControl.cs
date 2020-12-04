using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace GracevaleTest.UI
{
    public class PopupMessageControl : MonoBehaviour
    {
        [SerializeField]
        private Text _text;

        public event EventHandler AnimationEnded;

        public void SetText(string text)
        {
            _text.text = text;
        }

        public void SetColor(Color color)
        {
            _text.color = color;
        }


        //call from animation event
        private void FadeEnded()
        {
            AnimationEnded?.Invoke(this, EventArgs.Empty);
        }
    }
}