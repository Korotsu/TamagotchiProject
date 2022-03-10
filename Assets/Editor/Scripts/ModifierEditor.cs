using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace Tamagotchi
{
    [CustomPropertyDrawer(typeof(Impacter))]
    public class ImpacterEditor : PropertyDrawer
    {
        private float offset = 5.0f;

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            EditorGUI.BeginProperty(position, label, property);

            var indent = EditorGUI.indentLevel;
            EditorGUI.indentLevel = 0;

            var impacterName = property.FindPropertyRelative("impacterName");
            var impacterFoldout = property.FindPropertyRelative("impacterFoldout");

            var impacterFoldoutRect = new Rect(position.x, position.y, position.width, EditorGUIUtility.singleLineHeight);

            impacterFoldout.boolValue = EditorGUI.Foldout(impacterFoldoutRect, impacterFoldout.boolValue, new GUIContent(impacterName.stringValue));

            var value = EditorGUI.GetPropertyHeight(impacterFoldout) + impacterFoldoutRect.y + offset - position.y;
            if (impacterFoldout.boolValue)
            {
                EditorGUI.indentLevel++;

                var impacterNameRect = new Rect(position.x, impacterFoldoutRect.y + impacterFoldoutRect.height + offset, position.width, EditorGUIUtility.singleLineHeight);
                EditorGUI.PropertyField(impacterNameRect, impacterName);

                var impactRect = new Rect(position.x, impacterNameRect.y + impacterNameRect.height + offset, position.width, EditorGUIUtility.singleLineHeight);
                var impact = property.FindPropertyRelative("impactValue");
                EditorGUI.PropertyField(impactRect, impact);

                var typeRect = new Rect(position.x, impactRect.y + impactRect.height + offset, position.width, EditorGUIUtility.singleLineHeight);
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

                value = EditorGUI.GetPropertyHeight(impactedNeeds) + impactedNeedsRect.y + offset - position.y;

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
