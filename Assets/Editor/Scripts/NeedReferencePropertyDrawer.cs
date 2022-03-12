using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEditor;

namespace Tamagotchi
{
    [CustomPropertyDrawer(typeof(NeedReference))]
    public class NeedReferencePropertyDrawer : PropertyDrawer
    {
        private const float offset = 5.0f;

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            EditorGUI.BeginProperty(position, label, property);

            int indent = EditorGUI.indentLevel;
            EditorGUI.indentLevel = 0;

            ActionManager       actionManagerValue      = property.serializedObject.targetObject as ActionManager;
            ModifiersManager    modifierManagerValue    = property.serializedObject.targetObject as ModifiersManager;
            HumorManager        humorManagerValue       = property.serializedObject.targetObject as HumorManager;

            Rect impactedNeedRect = new Rect(position.x, position.y, position.width, EditorGUIUtility.singleLineHeight);
            
            SerializedProperty selected = property.FindPropertyRelative("selected");

            
            var options = new List<string>();

            if (actionManagerValue)
                options = actionManagerValue.tamagotchiManager.needs.Select(need => need.name).ToList();

            else if (modifierManagerValue)
                options = modifierManagerValue.tamagotchiManager.needs.Select(need => need.name).ToList();
            
            else if (humorManagerValue)
                options = humorManagerValue.tamagotchiManager.needs.Select(need => need.name).ToList();

            
            selected.intValue = EditorGUI.Popup(impactedNeedRect, selected.intValue, options.ToArray());
            EditorGUI.indentLevel = indent;

            EditorGUI.EndProperty();
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            property.NextVisible(true);
            float totalHeight = EditorGUI.GetPropertyHeight(property);

            return totalHeight + 2 * offset;
        }
    }
}