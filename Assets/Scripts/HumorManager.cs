using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Tamagotchi
{
    public class HumorManager : Manager
    {
        [SerializeField]
        private TMP_Text moodText = null;

        [SerializeField]
        private List<Mood> moods = new List<Mood>();

        private Mood activeMood = null;

        private float bestScore = 0.0f;

        private void Update()
        {
            bestScore = 0.0f;
            foreach (Mood mood in moods)
            {
                float score = mood.GetValidatedScore(ref tamagotchiManager);
                if (mood != activeMood && moodText && score > bestScore)
                {
                    if (activeMood != null)
                        activeMood.RemoveConsequences(ref tamagotchiManager);

                    moodText.text = mood.moodName;
                    activeMood = mood;
                    activeMood.StartConsequences(ref tamagotchiManager);
                    bestScore = score;
                }
            }


            if (activeMood != null)
            {
                activeMood.CheckConsequences(ref tamagotchiManager);

                if (activeMood.GetValidatedScore(ref tamagotchiManager) == 0.0f)
                {
                    moodText.text = "";
                    activeMood.RemoveConsequences(ref tamagotchiManager);
                    activeMood = null;
                }

            }
        }
    }

    [System.Serializable]
    public class Mood
    {
        public string moodName = "";

        [SerializeField, Min(0)]
        private float currentValue = 0.0f;

        [SerializeField, Range(0.0f, 1.0f)]
        private float validationStep = 0.0f;

        [SerializeField]
        private AnimationCurve feltScoreCurve = null;

        private float FeltScore => feltScoreCurve.Evaluate(currentValue);

        public List<Influencer> consequences = new List<Influencer>();

        [System.NonSerialized]
        public List<Influencer> activesConsequences = new List<Influencer>();

        [SerializeField]
        private List<MoodInfluencer> moodInfluencers = new List<MoodInfluencer>();

        public void UpdateValue(ref TamagotchiManager tamagotchiManager)
        {
            float allValue = 0.0f;
            float allCoef = 0.0f;

            foreach (MoodInfluencer mood in moodInfluencers)
            {
                allValue += tamagotchiManager.needs[mood.need.selected].statistic.FeltScore * mood.influenceCoef;
                allCoef += mood.influenceCoef;
            }

            currentValue = allValue / allCoef;
        }

        public float GetValidatedScore(ref TamagotchiManager tamagotchiManager)
        {
            UpdateValue(ref tamagotchiManager);
            return FeltScore >= validationStep ? FeltScore : 0.0f;
        }

        public void StartConsequences(ref TamagotchiManager tamagotchiManager)
        {
            activesConsequences = new List<Influencer>(consequences);
            foreach (Influencer consequence in consequences)
            {
                consequence.Start(ref tamagotchiManager);
            }
        }

        public void CheckConsequences(ref TamagotchiManager tamagotchiManager)
        {
            foreach (Influencer influencer in new List<Influencer>(activesConsequences))
            {
                if (!influencer.CheckState())
                {
                    influencer.Remove(ref tamagotchiManager);
                    activesConsequences.Remove(influencer);
                }
            }
        }

        public void RemoveConsequences(ref TamagotchiManager tamagotchiManager)
        {
            foreach (Influencer consequence in activesConsequences)
            {
                consequence.Remove(ref tamagotchiManager);
            }
            activesConsequences.Clear();
        }
    }

    [System.Serializable]
    public class MoodInfluencer
    {
        [Min(1.0f)]
        public float influenceCoef = 0.0f;

        public NeedReference need = null;
    }
}
