using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tamagotchi
{
    [Serializable]
    public class Modifier
    {
        [SerializeField] private string _modifierName;
        public string modifierName => _modifierName;

        [SerializeField, Range(0, 1)]
        public float chanceToTrigger = 0.0f;

        [System.NonSerialized]
        public List<Influencer> activeInfluencers = new List<Influencer>();

        [SerializeField]
        public List<Influencer> influencers = new List<Influencer>();

        [System.NonSerialized]
        public List<Impacter> activeImpacters = new List<Impacter>();

        [SerializeField]
        public List<Impacter> impacters = new List<Impacter>();

        [SerializeField]
        public List<Condition> conditions = new List<Condition>();

        public bool ApplyModifier(ref TamagotchiManager tamagotchiManager)
        {
            foreach (Impacter impacter in new List<Impacter>(activeImpacters))
            {
                if (!impacter.running)
                    activeImpacters.Remove(impacter);
            }

            foreach (Influencer influencer in new List<Influencer>(activeInfluencers))
            {
                if (!influencer.CheckState())
                {
                    influencer.Remove(ref tamagotchiManager);
                    activeInfluencers.Remove(influencer);
                }
            }

            return activeImpacters.Count + activeInfluencers.Count != 0;
        }

        public bool CheckConditions(TamagotchiManager tamagotchiManager)
        {
            foreach (Condition condition in conditions)
            {
                //Check if the condition isn't validated then return false because we do not met all needed condition to launch the modifier;
                if (condition.needReference.selected != -1 && (tamagotchiManager.needs[condition.needReference.selected].statistic.FeltScore >= condition.conditionPercentage) != condition.isSuperior)
                    return false;
            }

            return true;
        }

        public void Activate(ref ModifiersManager modifiersManager)
        {
            foreach (Impacter impacter in impacters)
            {
                impacter.Start(ref modifiersManager);
                activeImpacters.Add(impacter);
            }

            foreach (Influencer influencer in influencers)
            {
                influencer.Start(ref modifiersManager.tamagotchiManager);
                activeInfluencers.Add(influencer);
            }
        }

    }

    [Serializable]
    public class Condition
    {
        [SerializeField]
        public NeedReference needReference;

        [SerializeField]
        public bool isSuperior = false;

        [SerializeField, Range(0, 1)]
        public float conditionPercentage;
    }

    public enum EImpacterType
    {
        once,
        continuous,
        regular,
    }

    [Serializable]
    public class Influencer
    {
        [SerializeField]
        public string influencerName = "";

        [SerializeField]
        private float editorGUISize = 0.0f;

        [SerializeField]
        private bool influencerFoldout = false;

        [SerializeField]
        private float influenceCoef;

        [SerializeField]
        private bool timeLimited;

        [SerializeField, Min(0.0f)]
        public float duration = 0.0f;

        [SerializeField, Min(0.0f)]
        public float startTime = 0.0f;

        [SerializeField, Min(0.0f)]
        public float actualTime = 0.0f;

        [SerializeField]
        public List<NeedReference> impactedNeeds = new List<NeedReference>();

        public void Start(ref TamagotchiManager tamagotchiManager)
        {
            startTime = Time.realtimeSinceStartup;
            foreach (NeedReference needRef in impactedNeeds)
            {
                if (needRef.selected != -1)
                    tamagotchiManager.needs[needRef.selected].statistic.ApplyInfluencer(influenceCoef);
            }
        }

        public bool CheckState()
        {
            actualTime = Time.realtimeSinceStartup;
            return !timeLimited || actualTime - startTime < duration;
        }

        public void Remove(ref TamagotchiManager tamagotchiManager)
        {
            foreach (NeedReference needRef in impactedNeeds)
            {
                if (needRef.selected != -1)
                    tamagotchiManager.needs[needRef.selected].statistic.ApplyInfluencer(1 / influenceCoef);
            }
        }
    }

    [Serializable]
    public class Impacter
    {
        [SerializeField]
        public string impacterName;

        [SerializeField]
        private float editorGUISize = 0.0f;

        [SerializeField]
        private bool impacterFoldout = false;

        [SerializeField]
        public float impactValue;

        [SerializeField]
        public EImpacterType type = EImpacterType.once;

        [SerializeField]
        public bool timeLimited = false;

        [SerializeField, Min(-1.0f)]
        public float duration = 0.0f;

        [SerializeField, Min(0.0f)]
        public float startTime = 0.0f;

        [SerializeField, Min(0.0f)]
        public float actualTime = 0.0f;

        [SerializeField, Min(-1)]
        public int numberOfUtilization = 0;

        [SerializeField, Min(0.0f)]
        public int actualNumberOfUtilization = 0;

        [SerializeField, Min(0.0f)]
        public float lastCoroutineStartTime = 0.0f;

        [SerializeField, Min(0.01f)]
        private float intervalOfUtilization;

        [SerializeField]
        public List<NeedReference> impactedNeeds = new List<NeedReference>();

        [System.NonSerialized]
        public bool running = false;

        [System.NonSerialized]
        private TamagotchiManager tamagotchiManager;

        public void Start(ref ModifiersManager modifiersManager)
        {
            if (timeLimited)
                startTime = Time.realtimeSinceStartup;

            else
                actualNumberOfUtilization = 0;

            tamagotchiManager = modifiersManager.tamagotchiManager;
            modifiersManager.StartCoroutine(Impact());
        }

        IEnumerator Impact()
        {
            running = true;
            while ((timeLimited && (actualTime - startTime < duration || duration == -1)) || numberOfUtilization > actualNumberOfUtilization)
            {
                actualTime = Time.realtimeSinceStartup;
                if (type == EImpacterType.regular && actualTime - lastCoroutineStartTime < intervalOfUtilization)
                {
                    yield return new WaitForSeconds(intervalOfUtilization - (actualTime - lastCoroutineStartTime));
                }

                foreach (NeedReference needRef in impactedNeeds)
                {
                    if (needRef.selected != -1)
                        tamagotchiManager.needs[needRef.selected].statistic.ApplyImpacter(impactValue);
                }

                if (!timeLimited)
                    actualNumberOfUtilization++;

                actualTime = Time.realtimeSinceStartup;
                lastCoroutineStartTime = Time.realtimeSinceStartup;

                yield return null;
            }

            running = false;
        }
    }
}
