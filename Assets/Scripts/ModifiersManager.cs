using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tamagotchi
{
    [System.Serializable]
    public class ModifiersManager : MonoBehaviour
    {
        public TamagotchiManager tamagotchiManager;

        public List<Modifier> modifiers = new List<Modifier>();

        private List<Modifier> activeModifiers = new List<Modifier>();

        private List<Modifier> inactiveModifiers = new List<Modifier>();

        // Start is called before the first frame update
        void Start()
        {
            inactiveModifiers = modifiers;

            InvokeRepeating(nameof(CheckAllModifiers), 2.0f, 5.0f);
        }

        void CheckAllModifiers()
        {

        }

        void CheckActiveModifiers()
        {

        }

        void CheckInactiveModifiers()
        {

        }
    }
}
