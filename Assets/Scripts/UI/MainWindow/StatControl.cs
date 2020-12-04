using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace GracevaleTest.UI
{
    public class StatControl : MonoBehaviour
    {
        [SerializeField]
        private Image _icon;

        [SerializeField]
        private Text _text;

        public void SetIcon(Sprite icon)
        {
            _icon.sprite = icon;
        }

        public void SetText(string text)
        {
            _text.text = text;
        }
    }
}