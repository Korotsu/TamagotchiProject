using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEditor;

namespace Tamagotchi
{
    [CustomPropertyDrawer(typeof(ImpactedNeed))]
    public class ImpactedNeedPropertyDrawer : PropertyDrawer
    {
        private float offset = 5.0f;
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            EditorGUI.BeginProperty(position, label, property);

            var indent = EditorGUI.indentLevel;
            EditorGUI.indentLevel = 0;

            var actionManagerValue = property.serializedObject.targetObject as ActionManager;

            var actionRect = new Rect(position.x, position.y, position.width, EditorGUIUtility.singleLineHeight);
            var selected = property.FindPropertyRelative("selected");

            List<string> options = actionManagerValue.tamagotchiManager.needs.Select(need => need.name).ToList();
            selected.intValue = EditorGUI.Popup(actionRect, selected.intValue, options.ToArray());

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