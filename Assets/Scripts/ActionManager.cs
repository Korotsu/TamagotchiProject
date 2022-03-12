using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tamagotchi
{
    [System.Serializable]
    public class NeedReference
    {
        public int selected;
    }

    [System.Serializable]
    public class Action
    {
        [SerializeField]
        public string action = "";

        [SerializeField]
        public List<Impacter> impacters = new List<Impacter>();
    }

    [System.Serializable]
    public class ActionManager : MonoBehaviour
    {
        public TamagotchiManager tamagotchiManager;

        public List<Action> actions = new List<Action>();
    }
}
