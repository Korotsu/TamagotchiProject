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
        private Button actionButton = null;

        private int     bestStatIndex = 0;
        private float   bestStatValue = 0.0f;

        // Start is called before the first frame update
        void Start()
        {
            if (requestText)
            {
                InvokeRepeating(nameof(CheckAllStatistics), 2.0f, 5.0f);
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

        }

        public void StartAction()
        {
            foreach (var influencedNeeds in actionManager.actions[actionDropdown.value].impactedNeeds)
            {
                needs[influencedNeeds.selected].request.statValue = 0.0f; //Do Action (change value according to the needed behavior);
            }
        }

        private void CheckAllStatistics()
        {
            bestStatValue = needs[bestStatIndex].request.statValue;

            for(int i = 0; i < needs.Count; i++)
            {
                float tempStatValue = needs[i].request.CheckStatistic();

                if (tempStatValue > bestStatValue)
                {
                    bestStatValue = tempStatValue;
                    bestStatIndex = i;
                }
            }

            if (bestStatValue > needs[bestStatIndex].debugStep)
                requestText.text = needs[bestStatIndex].request.request;

            else
                requestText.text = string.Empty;
        }
    }
}
