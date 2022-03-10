using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace Tamagotchi
{
    [CustomPropertyDrawer(typeof(Modifier))]
    public class ModifierEditor : PropertyDrawer
    {
        private float offset = 5.0f;

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            EditorGUI.BeginProperty(position, label, property);

            var indent = EditorGUI.indentLevel;
            EditorGUI.indentLevel = 0;

            var modifierNameRect = new Rect(position.x, position.y, position.width, EditorGUIUtility.singleLineHeight);
            var modifierName = property.FindPropertyRelative("modifierName");

            var modifierFoldout = property.FindPropertyRelative("modifierFoldout");

            modifierFoldout.boolValue = EditorGUI.Foldout(modifierNameRect, modifierFoldout.boolValue, new GUIContent(modifierName.stringValue));

            var value = EditorGUI.GetPropertyHeight(modifierFoldout) + modifierNameRect.y + offset - position.y;
            if (modifierFoldout.boolValue)
            {
                EditorGUI.indentLevel++;

                var influenceRect = new Rect(position.x, modifierNameRect.y + modifierNameRect.height + offset, position.width, EditorGUIUtility.singleLineHeight);
                var influence = property.FindPropertyRelative("influence");
                EditorGUI.PropertyField(influenceRect, influence);

                var impactRect = new Rect(position.x, influenceRect.y + influenceRect.height + offset, position.width, EditorGUIUtility.singleLineHeight);
                var impact = property.FindPropertyRelative("impact");
                EditorGUI.PropertyField(impactRect, impact);

                var chanceToTriggerRect = new Rect(position.x, impactRect.y + impactRect.height + offset, position.width, EditorGUIUtility.singleLineHeight);
                var chanceToTrigger = property.FindPropertyRelative("chanceToTrigger");
                EditorGUI.PropertyField(chanceToTriggerRect, chanceToTrigger);

                var typeRect = new Rect(position.x, chanceToTriggerRect.y + chanceToTriggerRect.height + offset, position.width, EditorGUIUtility.singleLineHeight);
                var type = property.FindPropertyRelative("type");
                EditorGUI.PropertyField(typeRect, type);

                var currentPosition = typeRect.height + typeRect.y + offset;
                if (type.enumValueIndex > 0)
                {
                    var timeLimitedRect = new Rect(position.x, typeRect.y + typeRect.height + offset, position.width, EditorGUIUtility.singleLineHeight);
                    var timeLimited = property.FindPropertyRelative("timeLimited");
                    EditorGUI.PropertyField(timeLimitedRect, timeLimited);

                    currentPosition += timeLimitedRect.height + offset;

                    if (timeLimited.boolValue)
                    {
                        var durationRect = new Rect(position.x, currentPosition, position.width, EditorGUIUtility.singleLineHeight);
                        var duration = property.FindPropertyRelative("duration");
                        EditorGUI.PropertyField(durationRect, duration);

                        currentPosition += durationRect.height + offset;
                    }

                    else
                    {
                        var numberOfUtilizationRect = new Rect(position.x, currentPosition, position.width, EditorGUIUtility.singleLineHeight);
                        var numberOfUtilization = property.FindPropertyRelative("numberOfUtilization");
                        EditorGUI.PropertyField(numberOfUtilizationRect, numberOfUtilization);

                        currentPosition += numberOfUtilizationRect.height + offset;
                    }

                    if (type.enumValueIndex == 2)
                    {
                        var intervalOfUtilizationRect = new Rect(position.x, currentPosition, position.width, EditorGUIUtility.singleLineHeight);
                        var intervalOfUtilization = property.FindPropertyRelative("intervalOfUtilization");
                        EditorGUI.PropertyField(intervalOfUtilizationRect, intervalOfUtilization);

                        currentPosition += intervalOfUtilizationRect.height + offset;
                    }
                }

                var impactedNeedsRect = new Rect(position.x, currentPosition, position.width, EditorGUIUtility.singleLineHeight);
                var impactedNeeds = property.FindPropertyRelative("impactedNeeds");
                EditorGUI.PropertyField(impactedNeedsRect, impactedNeeds);

                var conditionsRect = new Rect(position.x, EditorGUI.GetPropertyHeight(impactedNeeds) + impactedNeedsRect.y + offset, position.width, EditorGUIUtility.singleLineHeight);
                var conditions = property.FindPropertyRelative("conditions");
                EditorGUI.PropertyField(conditionsRect, conditions);

                value = EditorGUI.GetPropertyHeight(conditions) + conditionsRect.y + offset - position.y;

                EditorGUI.indentLevel--;
            }

            var editorGUISize = property.FindPropertyRelative("editorGUISize");

            editorGUISize.floatValue = value;

            EditorGUI.indentLevel = indent;

            EditorGUI.EndProperty();
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            var editorGUISize = property.FindPropertyRelative("editorGUISize");
            return editorGUISize.floatValue;
        }
    }
}
