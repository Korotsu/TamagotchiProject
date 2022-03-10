using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

namespace Tamagotchi
{
    [Serializable]
    public class Need
    {
        [SerializeField]
        public string name = "";

        [SerializeField]
        public float debugStep = 0.0f;

        [SerializeField]
        public string request = "";

        [SerializeField]
        public Statistic statistic = new Statistic();

        public float CheckStatistic()
        {
            var statValue = statistic.FeltScore;

            return statValue > UnityEngine.Random.value ? statValue : 0.0f;
        }
    }
}
