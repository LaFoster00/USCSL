using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using NaughtyAttributes.Editor;
using UnityEditor;
using UnityEngine;

[CustomPropertyDrawer(typeof(ClampAttribute))]
public class ClampPropertyDrawer : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        ClampAttribute clampAttribute = (ClampAttribute)attribute;

        // Get the target object
        Object target = property.serializedObject.targetObject;
        SerializedObject serializedObject = new SerializedObject(target);
        
        // Use the nested property path to find the min and max properties
        string minPath = property.propertyPath.Replace(property.name, clampAttribute.MinFieldName);
        string maxPath = property.propertyPath.Replace(property.name, clampAttribute.MaxFieldName);

        SerializedProperty minProperty = serializedObject.FindProperty(minPath);
        SerializedProperty maxProperty = serializedObject.FindProperty(maxPath);

        if (minProperty != null && maxProperty != null)
        {
            float minValue = minProperty.floatValue;
            float maxValue = maxProperty.floatValue;

            // Draw the property with clamping
            SpecialCaseDrawerAttribute specialCaseAttribute = PropertyUtility.GetAttribute<SpecialCaseDrawerAttribute>(property);
            if (specialCaseAttribute != null)
            {
                specialCaseAttribute.GetDrawer().OnGUI(position, property);
            }
            else
            {
                // Check if visible
                bool visible = PropertyUtility.IsVisible(property);
                if (!visible)
                {
                    return;
                }

                // Validate
                ValidatorAttribute[] validatorAttributes = PropertyUtility.GetAttributes<ValidatorAttribute>(property);
                foreach (var validatorAttribute in validatorAttributes)
                {
                    validatorAttribute.GetValidator().ValidateProperty(property);
                }

                // Check if enabled and draw
                EditorGUI.BeginChangeCheck();
                bool enabled = PropertyUtility.IsEnabled(property);

                using (new EditorGUI.DisabledScope(disabled: !enabled))
                {
                    property.floatValue = EditorGUI.Slider(position, label, property.floatValue, minValue, maxValue);
                }

                // Call OnValueChanged callbacks
                if (EditorGUI.EndChangeCheck())
                {
                    PropertyUtility.CallOnValueChangedCallbacks(property);
                }
            }
        }
        else
        {
            EditorGUI.PropertyField(position, property, label);
            Debug.LogError($"Cannot find min or max field: {clampAttribute.MinFieldName} or {clampAttribute.MaxFieldName}");
        }
    }
}