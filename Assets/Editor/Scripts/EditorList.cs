using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public static class EditorList
{
	public static void ShowProperty(SerializedProperty list, ref Rect rect, ref Rect position, GUIContent name)
	{
		EditorGUI.PropertyField(rect,list, name);
		/*EditorGUI.indentLevel += 1;
		for (int i = 0; i < list.arraySize; i++)
		{
			/*var indexRect = new Rect(position.x, position.y, position.width, EditorGUIUtility.singleLineHeight);
			EditorGUI.PropertyField(indexRect, list.GetArrayElementAtIndex(i));*/

			/*var index = INIndexList.GetArrayElementAtIndex(i);
			var indexRect = new Rect(position.x, position.y, position.width, EditorGUIUtility.singleLineHeight);

			List<string> options = actionManagerValue.tamagotchiManager.needs.Select(need => need.name).ToList();
			index.intValue = EditorGUI.Popup(indexRect, "impacted Need = ", index.intValue, options.ToArray());*/
			//position.height += indexRect.height;
		/*}
		EditorGUI.indentLevel -= 1;*/
	}
}
