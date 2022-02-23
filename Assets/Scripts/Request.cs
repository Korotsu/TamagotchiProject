using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tamagotchi
{
    [System.Serializable]
    public class Request
    {
        [SerializeField]
        public string request = "";

        [SerializeField]
        public float statValue = 0.0f;
        
        public float CheckStatistic()
        {
            //statValue = statistic.GetStatValue();

            return statValue > Random.value ? statValue : 0.0f;
        }
    }
}
