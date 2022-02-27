using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEditor;

namespace Tamagotchi
{
    /*[CustomEditor(typeof(ActionManager))]
    [CanEditMultipleObjects]
    public class ActionEditor : Editor
    {
        SerializedProperty actions;

        SerializedProperty tamagotchiManager;

        //SerializedProperty actions;

        //SerializedProperty impactedNeeds;

        private ActionManager actionManagerValue;

        //private Action actionValue;

        private int selected = 0;

        void OnEnable()
        {
            actions             = serializedObject.FindProperty("actions");
            tamagotchiManager   = serializedObject.FindProperty("tamagotchiManager");
            //actions = serializedObject.FindProperty("actions");
            //impactedNeeds = serializedObject.FindProperty("impactedNeeds");
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();
            EditorGUILayout.PropertyField(tamagotchiManager);
            EditorGUILayout.PropertyField(actions);

            //EditorGUILayout.PropertyField(actions);
            //EditorGUILayout.PropertyField(impactedNeeds);

            //actionValue = serializedObject. as Action;
            actionManagerValue = serializedObject.targetObject as ActionManager;

            foreach (Action action in actionManagerValue.actions)
            {
                //SerializedProperty actionProperty = action.fin serializedObject.FindProperty("action");
                //EditorGUILayout.PropertyField(actionProperty);
            }

            if (actionManagerValue)
            {
                List<string> options = actionManagerValue.tamagotchiManager.needs.Select(need => need.name).ToList();
                selected = EditorGUILayout.Popup("Label", selected, options.ToArray());
            }
            serializedObject.ApplyModifiedProperties();
        }
    }*/

    [CustomPropertyDrawer(typeof(Action))]
    public class ActionEditor : PropertyDrawer
    {
        //SerializedProperty action;

        //SerializedProperty impactedNeeds;

        //private ActionManager actionManagerValue;

        //private Action actionValue;

        //private int selected = 0;

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            EditorGUI.BeginProperty(position, label, property);

            var indent = EditorGUI.indentLevel;
            EditorGUI.indentLevel = 0;

            var actionRect = new Rect(position.x, position.y, position.width, EditorGUIUtility.singleLineHeight);

            var INIndexListRect = new Rect(position.x, position.y + actionRect.height, position.width, EditorGUIUtility.singleLineHeight);

            var action = property.FindPropertyRelative("action");
            var INIndexList = property.FindPropertyRelative("INIndexList");

            EditorGUI.PropertyField(actionRect, action, new GUIContent(action.stringValue));

            EditorGUI.indentLevel += 1;

            EditorList.ShowProperty(INIndexList, ref INIndexListRect, ref position, new GUIContent("Impacted Needs"));

            EditorGUI.indentLevel -= 1;

            //var actionManagerValue = property.serializedObject.targetObject as ActionManager;

            /*if (actionManagerValue)
            {
                for (int i = 0; i < INIndexList.arraySize; i++)
                {
                    //INIndexList.InsertArrayElementAtIndex(i);
                    var index = INIndexList.GetArrayElementAtIndex(i);
                    var indexRect = new Rect(position.x, position.y, position.width, EditorGUIUtility.singleLineHeight);

                    List<string> options = actionManagerValue.tamagotchiManager.needs.Select(need => need.name).ToList();
                    index.intValue = EditorGUI.Popup(indexRect, "impacted Need = ", index.intValue, options.ToArray());
                }
            }*/

            EditorGUI.indentLevel = indent;

            EditorGUI.EndProperty();
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            float totalHeight = EditorGUI.GetPropertyHeight(property);
            var action = property.serializedObject.FindProperty(fieldInfo.Name).GetArrayElementAtIndex(0);

            action.NextVisible(true);

            while (action.NextVisible(false))
            {
                totalHeight += EditorGUI.GetPropertyHeight(action, true);
            }

            return totalHeight;
        }
    }
}
