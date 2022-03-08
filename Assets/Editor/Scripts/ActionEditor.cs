using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace Tamagotchi
{
    [CustomPropertyDrawer(typeof(Action))]
    public class ActionEditor : PropertyDrawer
    {

        private float offset = 5.0f;

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            EditorGUI.BeginProperty(position, label, property);

            var indent = EditorGUI.indentLevel;
            EditorGUI.indentLevel = 0;

            var actionRect = new Rect(position.x, position.y, position.width, EditorGUIUtility.singleLineHeight);
            var impactedNeedsRect = new Rect(position.x, position.y + actionRect.height + offset, position.width, EditorGUIUtility.singleLineHeight);

            var action = property.FindPropertyRelative("action");
            var impactedNeeds = property.FindPropertyRelative("impactedNeeds");

            EditorGUI.PropertyField(actionRect, action, new GUIContent(action.stringValue));
            EditorGUI.PropertyField(impactedNeedsRect, impactedNeeds);

            EditorGUI.indentLevel = indent;

            EditorGUI.EndProperty();
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            property.NextVisible(true);
            float totalHeight = EditorGUI.GetPropertyHeight(property);

            while (property.NextVisible(false) && property.depth >= 2)
            {
                totalHeight += EditorGUI.GetPropertyHeight(property, true);
            }

            return totalHeight + 2 * offset;
        }
    }
}
