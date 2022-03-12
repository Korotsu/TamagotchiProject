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

            int indent = EditorGUI.indentLevel;
            EditorGUI.indentLevel = 0;

            SerializedProperty impacterName     = property.FindPropertyRelative("impacterName");
            SerializedProperty impacterFoldout  = property.FindPropertyRelative("impacterFoldout");
            
            Rect impacterFoldoutRect = new Rect(position.x, position.y, position.width, EditorGUIUtility.singleLineHeight);

            impacterFoldout.boolValue = EditorGUI.Foldout(impacterFoldoutRect, impacterFoldout.boolValue, new GUIContent(impacterName.stringValue));

            float value = EditorGUI.GetPropertyHeight(impacterFoldout) + impacterFoldoutRect.y + offset - position.y;
            if (impacterFoldout.boolValue)
            {
                EditorGUI.indentLevel++;

                Rect impacterNameRect = new Rect(position.x, impacterFoldoutRect.y + impacterFoldoutRect.height + offset, position.width, EditorGUIUtility.singleLineHeight);
                EditorGUI.PropertyField(impacterNameRect, impacterName);

                Rect impactRect = new Rect(position.x, impacterNameRect.y + impacterNameRect.height + offset, position.width, EditorGUIUtility.singleLineHeight);
                SerializedProperty impact = property.FindPropertyRelative("impactValue");
                EditorGUI.PropertyField(impactRect, impact);

                Rect typeRect = new Rect(position.x, impactRect.y + impactRect.height + offset, position.width, EditorGUIUtility.singleLineHeight);
                SerializedProperty type = property.FindPropertyRelative("type");
                EditorGUI.PropertyField(typeRect, type);

                float currentPosition = typeRect.height + typeRect.y + offset;
                if (type.enumValueIndex > 0)
                {
                    Rect timeLimitedRect = new Rect(position.x, typeRect.y + typeRect.height + offset, position.width, EditorGUIUtility.singleLineHeight);
                    SerializedProperty timeLimited = property.FindPropertyRelative("timeLimited");
                    EditorGUI.PropertyField(timeLimitedRect, timeLimited);

                    currentPosition += timeLimitedRect.height + offset;

                    if (timeLimited.boolValue)
                    {
                        Rect durationRect = new Rect(position.x, currentPosition, position.width, EditorGUIUtility.singleLineHeight);
                        SerializedProperty duration = property.FindPropertyRelative("duration");
                        EditorGUI.PropertyField(durationRect, duration);

                        currentPosition += durationRect.height + offset;
                    }

                    else
                    {
                        Rect numberOfUtilizationRect = new Rect(position.x, currentPosition, position.width, EditorGUIUtility.singleLineHeight);
                        SerializedProperty numberOfUtilization = property.FindPropertyRelative("numberOfUtilization");
                        EditorGUI.PropertyField(numberOfUtilizationRect, numberOfUtilization);

                        currentPosition += numberOfUtilizationRect.height + offset;
                    }

                    if (type.enumValueIndex == 2)
                    {
                        Rect intervalOfUtilizationRect = new Rect(position.x, currentPosition, position.width, EditorGUIUtility.singleLineHeight);
                        SerializedProperty intervalOfUtilization = property.FindPropertyRelative("intervalOfUtilization");
                        EditorGUI.PropertyField(intervalOfUtilizationRect, intervalOfUtilization);

                        currentPosition += intervalOfUtilizationRect.height + offset;
                    }
                }

                Rect impactedNeedsRect = new Rect(position.x, currentPosition, position.width, EditorGUIUtility.singleLineHeight);
                SerializedProperty impactedNeeds = property.FindPropertyRelative("impactedNeeds");
                EditorGUI.PropertyField(impactedNeedsRect, impactedNeeds);

                value = EditorGUI.GetPropertyHeight(impactedNeeds) + impactedNeedsRect.y + offset - position.y;

                EditorGUI.indentLevel--;
            }

            SerializedProperty editorGUISize = property.FindPropertyRelative("editorGUISize");

            editorGUISize.floatValue = value;

            EditorGUI.indentLevel = indent;

            EditorGUI.EndProperty();
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            SerializedProperty editorGUISize = property.FindPropertyRelative("editorGUISize");
            return editorGUISize.floatValue;
        }
    }
}
