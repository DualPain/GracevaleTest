using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace GracevaleTest.UI
{
    public class HealthBarControl : MonoBehaviour
    {
        [SerializeField]
        private Slider _slider;

        private void Awake()
        {
            _slider.minValue = 0;
        }

        public void SetCurrentHP(float hp)
        {
            _slider.value = hp;
        }

        public void SetMaxHP(float maxHp)
        {
            _slider.maxValue = maxHp;
        }
    }
}