using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace Tamagotchi
{
    [Serializable]
    public class Need
    {
        [SerializeField]
        public string name = "";

        [SerializeField]
        public float step = 0.0f;

        [SerializeField]
        public string request = "";

        [SerializeField]
        public bool reverseGauge = false;
        
        [SerializeField]
        public Color gaugeColor;

        [SerializeField]
        public Color textColor;
        
        [SerializeField]
        public Statistic statistic = new Statistic();

        [System.NonSerialized]
        public Image gauge = null;

        public float CheckStatistic()
        {
            float statValue = statistic.FeltScore;
            gauge.fillAmount = reverseGauge ? statValue : 1 - statValue;

            return statValue > UnityEngine.Random.value ? statValue : 0.0f;
        }
    }
}
