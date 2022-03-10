using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tamagotchi
{
    [Serializable]
    public class Modifier
    {
        [SerializeField]
        public string modifierName = "";

        [SerializeField]
        public float editorGUISize = 0.0f;

        [SerializeField]
        public bool modifierFoldout = false;

        [SerializeField]
        private float influence = 0.0f;

        [SerializeField]
        private float impact = 0.0f;

        [SerializeField, Range(0, 1)]
        private float chanceToTrigger = 0.0f;

        [SerializeField]
        private EModifierType type = EModifierType.once;

        [SerializeField]
        private bool timeLimited = false;

        [SerializeField, Min(0.0f)]
        private float duration = 0.0f;

        [SerializeField, Min(0)]
        private int numberOfUtilization = 0;

        [SerializeField, Min(0.0f)]
        private float intervalOfUtilization = 0.0f;

        [SerializeField]
        public List<ImpactedNeed> impactedNeeds = new List<ImpactedNeed>();

        [SerializeField]
        public List<Condition> conditions = new List<Condition>();
    }

    [Serializable]
    public class Condition
    {
        [SerializeField]
        public ImpactedNeed need;

        [SerializeField]
        public bool isSuperior = false;

        [SerializeField, Range(0, 1)]
        public float conditionPercentage;
    }

    public enum EModifierType
    {
        once,
        continuous,
        regular,
    }

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

        [SerializeField, Min(-1)] private int repeatNtimes;
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
