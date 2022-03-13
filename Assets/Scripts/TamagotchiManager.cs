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
        private Image dialogueBubble = null;

        [SerializeField]
        private TMP_Dropdown actionDropdown = null;

        [SerializeField]
        private GameObject gaugePrefab = null;

        [SerializeField]
        private RectTransform gaugeContainer = null;

        [SerializeField, Min(0.0f)]
        private float gaugeContainerSpaces;

        [SerializeField, Min(0.0f)]
        private float gaugeSizes;

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

            if (gaugeContainer)
            {
                gaugeContainer.GetComponent<VerticalLayoutGroup>().spacing = gaugeContainerSpaces;
                gaugeContainer.sizeDelta = new Vector2(gaugeContainer.sizeDelta.x, (gaugeSizes + gaugeContainerSpaces) * needs.Count);
                foreach (Need need in needs)
                {
                    GameObject newGauge = Instantiate(gaugePrefab, gaugeContainer.transform);

                    need.gauge = newGauge.transform.GetChild(0).GetChild(0).GetComponent<Image>();
                    TMP_Text gaugeText = newGauge.transform.GetChild(0).GetChild(1).GetComponent<TMP_Text>();

                    gaugeText.text = need.statistic.statName;
                    gaugeText.color = need.textColor;
                    need.gauge.color = need.gaugeColor;
                }
            }
        }

        // Update is called once per frame
        void Update()
        {
            foreach (Need need in needs)
            {
                need.statistic.UpdateValue();
            }
        }

        public void StartAction()
        {
            Action action = actionManager.actions[actionDropdown.value];
            foreach (Impacter impacter in action.impacters)
            {
                foreach (NeedReference needRef in impacter.impactedNeeds)
                {
                    if (needRef.selected != -1)
                        needs[needRef.selected].statistic.ApplyImpacter(impacter.impactValue);
                }
            }
        }

        private void CheckAllStatistics()
        {
            bestStatValue = needs[bestStatIndex].statistic.FeltScore;

            for (int i = 0; i < needs.Count; i++)
            {
                float tempStatValue = needs[i].CheckStatistic();

                if (tempStatValue > bestStatValue && tempStatValue > needs[i].step)
                {
                    bestStatValue = tempStatValue;
                    bestStatIndex = i;
                }
            }

            if (bestStatValue > needs[bestStatIndex].step)
            {
                requestText.text = needs[bestStatIndex].request;
                dialogueBubble.enabled = true;
            }

            else
            {
                requestText.text = string.Empty;
                dialogueBubble.enabled = false;
            }
        }
    }
}
