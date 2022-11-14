using System;
using UnityEditor;
using UnityEngine;

namespace Game.Common.ScriptableData.Editor
{
    [CustomPropertyDrawer(typeof(ScriptableDataIdSlotAttribute))]
    public class ScriptableDataIdSlotDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            ScriptableDataIdSlotAttribute attribute = this.attribute as ScriptableDataIdSlotAttribute;
            GUI.enabled = false;
            var propertySerializedObject = property.serializedObject;
            var scriptableObjectProperty = propertySerializedObject.FindProperty(attribute.ScriptableObjectField);
            if (scriptableObjectProperty == null)
            {
                return;
                throw new Exception("Expected scriptable object property field to exist for " + propertySerializedObject.targetObject.name);
            }
            if (scriptableObjectProperty.objectReferenceValue)
            {
                var scriptableObjectSerializedObject = new SerializedObject(scriptableObjectProperty.objectReferenceValue);
                var idField = scriptableObjectSerializedObject.FindProperty(attribute.ScriptableObjectIdField);
                if (idField == null)
                    throw new Exception($"Id field \"{idField}\" doesn't exist for scriptable object \"{scriptableObjectProperty.objectReferenceValue.name}\".");
                if (idField.stringValue != property.stringValue)
                {
                    property.stringValue = idField.stringValue;
                    propertySerializedObject.ApplyModifiedProperties();
                }
            }
            else if (property.stringValue != "")
            {
                property.stringValue = "";
                propertySerializedObject.ApplyModifiedProperties();
            }
            EditorGUI.PropertyField(position, property, label, true);
            GUI.enabled = true;
        }
    }
}