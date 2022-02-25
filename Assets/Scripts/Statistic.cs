using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using UnityEngine;

namespace Tamagotchi
{
    [Serializable]
    public class Statistic : MonoBehaviour
    {
        [SerializeField] private string         _statName;

        [SerializeField, Min(0)] private float  maxValue;
        [SerializeField] private float          currentValue;

        [SerializeField] private AnimationCurve feltScoreCurve;
        
        public string statName => _statName;
        public float FeltScore => feltScoreCurve.Evaluate(currentValue);


        [SerializeField] private List<Influencer> activeInfluencers;

        [SerializeField] private List<Impacter> startingImpacters;
        private List<Impacter> activeImpacters;

        
        public void AddInfluencer(Influencer newInfluencer)
        {
            if (activeInfluencers.All(influencer => influencer.inflName != newInfluencer.inflName))
                activeInfluencers.Add(newInfluencer);
            
        }

        public void AddImpacter(Impacter newImpacter)
        {
            currentValue += newImpacter.Impact(out bool willRepeat);

            if (willRepeat)
            {
                if (activeImpacters.All(impacter => impacter.inpcName != newImpacter.inpcName))
                    activeImpacters.Add(newImpacter);
            }
        }


        private void Start()
        {
            foreach (Impacter impacter in activeImpacters)
                AddImpacter(impacter);

            startingImpacters.Clear();
        }
        
        
        
    }
}
