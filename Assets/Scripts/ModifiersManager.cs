using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tamagotchi
{
    [System.Serializable]
    public class ModifiersManager : Manager
    {
        [SerializeField]
        private float checkActiveModifiersRefreshRate = 0.5f;

        [SerializeField]
        private float checkInactiveModifiersRefreshRate = 5.0f;

        [SerializeField]
        public List<Modifier> modifiers = new List<Modifier>();

        private List<Modifier> activeModifiers = new List<Modifier>();

        private List<Modifier> inactiveModifiers = new List<Modifier>();


        // Start is called before the first frame update
        private void Start()
        {
            inactiveModifiers = modifiers;

            InvokeRepeating(nameof(CheckActiveModifiers), 2.0f, checkActiveModifiersRefreshRate);
            InvokeRepeating(nameof(CheckInactiveModifiers), 2.0f, checkInactiveModifiersRefreshRate);
        }

        private void CheckActiveModifiers()
        {
            foreach (Modifier modifier in new List<Modifier>(activeModifiers))
            {
                if (!modifier.ApplyModifier(ref tamagotchiManager))
                {
                    inactiveModifiers.Add(modifier);
                    activeModifiers.Remove(modifier);
                }
            }
        }

        private void CheckInactiveModifiers()
        {
            foreach (var modifier in new List<Modifier>(inactiveModifiers))
            {
                if (modifier.CheckConditions(tamagotchiManager) && modifier.chanceToTrigger > UnityEngine.Random.value)
                {
                    ModifiersManager modifiersManager = this;
                    modifier.Activate(ref modifiersManager);
                    activeModifiers.Add(modifier);
                    inactiveModifiers.Remove(modifier);
                }
            }
        }
    }
}
