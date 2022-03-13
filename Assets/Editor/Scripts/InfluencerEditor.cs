using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace Tamagotchi
{
    [CustomPropertyDrawer(typeof(Influencer))]
    public class InfluencerEditor : PropertyDrawer
    {
        private float offset = 5.0f;

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            EditorGUI.BeginProperty(position, label, property);

            int indent = EditorGUI.indentLevel;
            EditorGUI.indentLevel = 0;

            SerializedProperty influencerName = property.FindPropertyRelative("influencerName");
            SerializedProperty influencerFoldout = property.FindPropertyRelative("influencerFoldout");

            Rect influencerFoldoutRect = new Rect(position.x, position.y, position.width, EditorGUIUtility.singleLineHeight);

            influencerFoldout.boolValue = EditorGUI.Foldout(influencerFoldoutRect, influencerFoldout.boolValue, new GUIContent(influencerName.stringValue));

            float value = EditorGUI.GetPropertyHeight(influencerFoldout) + influencerFoldoutRect.y + offset - position.y;
            if (influencerFoldout.boolValue)
            {
                EditorGUI.indentLevel++;

                Rect influencerNameRect = new Rect(position.x, influencerFoldoutRect.y + influencerFoldoutRect.height + offset, position.width, EditorGUIUtility.singleLineHeight);
                EditorGUI.PropertyField(influencerNameRect, influencerName);

                Rect influenceRect = new Rect(position.x, influencerNameRect.y + influencerNameRect.height + offset, position.width, EditorGUIUtility.singleLineHeight);
                SerializedProperty influence = property.FindPropertyRelative("influenceCoef");
                EditorGUI.PropertyField(influenceRect, influence);


                Rect timeLimitedRect = new Rect(position.x, influenceRect.y + influenceRect.height + offset, position.width, EditorGUIUtility.singleLineHeight);
                SerializedProperty timeLimited = property.FindPropertyRelative("timeLimited");
                EditorGUI.PropertyField(timeLimitedRect, timeLimited);

                float currentPosition = timeLimitedRect.height + timeLimitedRect.y + offset;

                if (timeLimited.boolValue)
                {
                    Rect durationRect = new Rect(position.x, currentPosition, position.width, EditorGUIUtility.singleLineHeight);
                    SerializedProperty duration = property.FindPropertyRelative("duration");
                    EditorGUI.PropertyField(durationRect, duration);

                    currentPosition += durationRect.height + offset;
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
