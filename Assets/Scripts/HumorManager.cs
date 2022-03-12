using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tamagotchi
{
    public class HumorManager : MonoBehaviour
    {
        [SerializeField] public TamagotchiManager tamagotchiManager;

        [SerializeField] public List<MoodInfluencer> moodInfluencers;
    }

    [System.Serializable]
    public class MoodInfluencer
    {
        private TamagotchiManager tamagotchiManager;
        
        NeedReference aaa;
    }
}