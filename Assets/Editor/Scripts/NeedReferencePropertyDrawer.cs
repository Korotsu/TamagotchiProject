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

            Manager managerValue = property.serializedObject.targetObject as Manager;

            if (managerValue.tamagotchiManager != null)
            {
                List<Need> needs = managerValue.tamagotchiManager.needs;

                Rect impactedNeedRect = new Rect(position.x, position.y, position.width, EditorGUIUtility.singleLineHeight);

                SerializedProperty selected = property.FindPropertyRelative("selected");
                SerializedProperty needName = property.FindPropertyRelative("needName");
                List<string> options = needs.Select(need => need.name).ToList();

                int previousValue = selected.intValue;
                selected.intValue = EditorGUI.Popup(impactedNeedRect, selected.intValue, options.ToArray());

                if (selected.intValue != -1)
                {
                    string newName = needs[selected.intValue].name;

                    if (selected.intValue == previousValue && needName.stringValue != newName)
                        selected.intValue = needs.FindIndex(need => need.name == needName.stringValue);
                    else
                        needName.stringValue = needs[selected.intValue].name;
                }

                else
                    needName.stringValue = "";
            }

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