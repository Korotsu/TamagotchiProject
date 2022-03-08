using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tamagotchi
{
    [System.Serializable]
    public class Action
    {
        [SerializeField]
        public string action = "";

        //[SerializeField]
        private List<Need> impactedNeeds = new List<Need>();

        [SerializeField]
        public List<int> INIndexList = new List<int>();
    }

    [System.Serializable]
    public class ActionManager : MonoBehaviour
    {
        public TamagotchiManager tamagotchiManager;
        
        public List<Action> actions = new List<Action>();

    }
}
