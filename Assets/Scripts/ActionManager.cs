using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tamagotchi
{
    [System.Serializable]
    public class ImpactedNeed
    {
        public int selected;
    }

    [System.Serializable]
    public class Action
    {
        [SerializeField]
        public string action = "";

        [SerializeField]
        public List<ImpactedNeed> impactedNeeds = new List<ImpactedNeed>();
    }

    [System.Serializable]
    public class ActionManager : MonoBehaviour
    {
        public TamagotchiManager tamagotchiManager;

        public List<Action> actions = new List<Action>();
    }
}
