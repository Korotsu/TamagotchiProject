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
        private List<Need> needs = new List<Need>();

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
                InvokeRepeating(nameof(CheckAllStatistics), 2.0f, 1.0f);
            }

            if (actionDropdown)
            {
                List<TMP_Dropdown.OptionData> optionDataList = 
                    needs.Select(need => new TMP_Dropdown.OptionData(need.request.request)).ToList();

                actionDropdown.options = optionDataList;
            }
        }

        // Update is called once per frame
        void Update()
        {

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

            requestText.text = needs[bestStatIndex].request.request;
        }
    }
}
