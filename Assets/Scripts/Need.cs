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
        private string name = "";

        //[SerializeField]
        //private Statistic statistic = new Statistic();

        [SerializeField]
        public Request request = new Request();
    }
}
