using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomPropertyDrawer(typeof(KeyValue<,>))]
public class KeyValueDraw : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        EditorGUI.BeginProperty(position, label, property);

        SerializedProperty key = property.FindPropertyRelative("key");
        SerializedProperty value = property.FindPropertyRelative("value");

        int indent = EditorGUI.indentLevel;
        EditorGUI.indentLevel = 0;

        float widthSize = position.width / 2;
        float offsetSize = 1;

        Rect pos1 = new Rect(position.x, position.y, widthSize - offsetSize, position.height);
        Rect pos2 = new Rect(position.x + widthSize * 1, position.y, widthSize - offsetSize, position.height);

        EditorGUI.PropertyField(pos1, key, GUIContent.none);
        EditorGUI.PropertyField(pos2, value, GUIContent.none);

        EditorGUI.indentLevel = indent;

        EditorGUI.EndProperty();
    }
}
