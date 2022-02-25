using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tamagotchi
{
    [Serializable]
    public class Influencer
    {
        [SerializeField] private string _inflName;
        public string inflName => _inflName;

        [SerializeField] private bool timeLimited;
        [SerializeField] private float lastsFor;

        [SerializeField] private float modifierCoef;

        /// <summary> Uses the modifier, by default consuming it if it is limited in time. </summary>
        /// <returns> Returns if it has been fully consumed, and its value. </returns>
        public float UseInfluencer(out bool isConsumed, bool consume = true)
        {
            if (timeLimited)
            {
                lastsFor -= Time.deltaTime;

                isConsumed = lastsFor <= 0f;
            }
            else isConsumed = false;
            
            
            return modifierCoef;
        }
    }

    [Serializable]
    public class Impacter
    {
        [SerializeField] private string _inpcName;
        public string inpcName => _inpcName;

        [SerializeField, Min(-1)] private int      repeatNtimes;
        [SerializeField, Min(0.01f)] private float repetitionInterval;

        [SerializeField] private float modifierValue;


        public float Impact(out bool willRepeat)
        {
            if (repeatNtimes > 0)
            {
                repeatNtimes--;

                willRepeat = repeatNtimes != 0;
            }
            else willRepeat = true;
            
            
            return modifierValue;
        }
    }
}
