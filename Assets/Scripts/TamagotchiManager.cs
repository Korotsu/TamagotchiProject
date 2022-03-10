using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine.UI;
using UnityEngine;

namespace Tamagotchi
{
    public class TamagotchiManager : MonoBehaviour
    {
        [SerializeField]
        public List<Need> needs = new List<Need>();

        [SerializeField]
        private ActionManager actionManager = null;

        [SerializeField]
        private TMP_Text requestText = null;

        [SerializeField]
        private TMP_Dropdown actionDropdown = null;

        [SerializeField]
        private float StatisticsCheckRefreshRate = 2.0f;

        private int bestStatIndex = 0;
        private float bestStatValue = 0.0f;

        // Start is called before the first frame update
        void Start()
        {
            if (requestText)
            {
                InvokeRepeating(nameof(CheckAllStatistics), 2.0f, StatisticsCheckRefreshRate);
            }

            if (actionDropdown)
            {
                List<TMP_Dropdown.OptionData> optionDataList =
                    actionManager.actions.Select(action => new TMP_Dropdown.OptionData(action.action)).ToList();

                actionDropdown.options = optionDataList;
            }
        }

        // Update is called once per frame
        void Update()
        {
            foreach (var need in needs)
            {
                need.statistic.UpdateValue();
            }
        }

        public void StartAction()
        {
            var action = actionManager.actions[actionDropdown.value];
            foreach (var impacter in action.impacters)
            {
                foreach (var need in impacter.impactedNeeds)
                {
                    needs[need.selected].statistic.ApplyImpacter(impacter.impactValue);
                }
            }
        }

        private void CheckAllStatistics()
        {
            bestStatValue = needs[bestStatIndex].statistic.FeltScore;

            for (int i = 0; i < needs.Count; i++)
            {
                float tempStatValue = needs[i].CheckStatistic();

                if (tempStatValue > bestStatValue && tempStatValue > needs[i].debugStep)
                {
                    bestStatValue = tempStatValue;
                    bestStatIndex = i;
                }
            }

            if (bestStatValue > needs[bestStatIndex].debugStep)
                requestText.text = needs[bestStatIndex].request;

            else
                requestText.text = string.Empty;
        }
    }
}
