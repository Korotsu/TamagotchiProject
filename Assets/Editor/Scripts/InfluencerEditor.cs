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

            var indent = EditorGUI.indentLevel;
            EditorGUI.indentLevel = 0;

            var influencerName = property.FindPropertyRelative("influencerName");
            var influencerFoldout = property.FindPropertyRelative("influencerFoldout");

            var influencerFoldoutRect = new Rect(position.x, position.y, position.width, EditorGUIUtility.singleLineHeight);

            influencerFoldout.boolValue = EditorGUI.Foldout(influencerFoldoutRect, influencerFoldout.boolValue, new GUIContent(influencerName.stringValue));

            var value = EditorGUI.GetPropertyHeight(influencerFoldout) + influencerFoldoutRect.y + offset - position.y;
            if (influencerFoldout.boolValue)
            {
                EditorGUI.indentLevel++;

                var influencerNameRect = new Rect(position.x, influencerFoldoutRect.y + influencerFoldoutRect.height + offset, position.width, EditorGUIUtility.singleLineHeight);
                EditorGUI.PropertyField(influencerNameRect, influencerName);

                var influenceRect = new Rect(position.x, influencerNameRect.y + influencerNameRect.height + offset, position.width, EditorGUIUtility.singleLineHeight);
                var influence = property.FindPropertyRelative("influenceCoef");
                EditorGUI.PropertyField(influenceRect, influence);


                var timeLimitedRect = new Rect(position.x, influenceRect.y + influenceRect.height + offset, position.width, EditorGUIUtility.singleLineHeight);
                var timeLimited = property.FindPropertyRelative("timeLimited");
                EditorGUI.PropertyField(timeLimitedRect, timeLimited);

                var currentPosition = timeLimitedRect.height + timeLimitedRect.y + offset;

                if (timeLimited.boolValue)
                {
                    var durationRect = new Rect(position.x, currentPosition, position.width, EditorGUIUtility.singleLineHeight);
                    var duration = property.FindPropertyRelative("duration");
                    EditorGUI.PropertyField(durationRect, duration);

                    currentPosition += durationRect.height + offset;
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
