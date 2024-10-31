#if NAUGHTY_ATTRIBUTES
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEditor.Animations;
using System.Reflection;
using NaughtyAttributes.Editor;
using Bipolar.Humanoid3D.Animation;

namespace Bipolar.Humanoid3D.Editor
{
    [CustomPropertyDrawer(typeof(AnimatorParameterAttribute))]
    public class AnimatorParameterAttributeDrawer : PropertyDrawerBase
    {
        private const string InvalidAnimatorControllerWarningMessage = "Target animator controller is null";

        private AnimatorParameterAttribute animatorParameterAttribute; 
        private AnimatorController animatorController;

        protected override float GetPropertyHeight_Internal(SerializedProperty property, GUIContent label)
        {
            float height = base.GetPropertyHeight_Internal(property, label);
            if (animatorController == null)
            {
                animatorParameterAttribute ??= PropertyUtility.GetAttribute<AnimatorParameterAttribute>(property);
                animatorController = GetAnimatorController(property, animatorParameterAttribute.AnimatorName);
                if (animatorController == null)
                    height += GetHelpBoxHeight();
            }

            return height;
        }

        protected override void OnGUI_Internal(Rect rect, SerializedProperty property, GUIContent label)
        {
            EditorGUI.BeginProperty(rect, label, property);

            animatorParameterAttribute ??= PropertyUtility.GetAttribute<AnimatorParameterAttribute>(property);
            if (animatorController == null)
                animatorController = GetAnimatorController(property, animatorParameterAttribute.AnimatorName);

            if (animatorController == null)
            {
                DrawDefaultPropertyAndHelpBox(rect, property, InvalidAnimatorControllerWarningMessage, MessageType.Warning);
                return;
            }

            int parametersCount = animatorController.parameters.Length;
            List<AnimatorControllerParameter> animatorParameters = new List<AnimatorControllerParameter>(parametersCount);
            for (int i = 0; i < parametersCount; i++)
            {
                AnimatorControllerParameter parameter = animatorController.parameters[i];
                if (animatorParameterAttribute.AnimatorParamType == null || parameter.type == animatorParameterAttribute.AnimatorParamType)
                {
                    animatorParameters.Add(parameter);
                }
            }

            var nameProperty = property.FindPropertyRelative("name");
            var paramName = nameProperty.stringValue;

            int index = 0;
            for (int i = 0; i < animatorParameters.Count; i++)
            {
                if (paramName.Equals(animatorParameters[i].name, System.StringComparison.Ordinal))
                {
                    index = i + 1; // +1 because the first option is reserved for (None)
                    break;
                }
            }

            string[] displayOptions = GetDisplayOptions(animatorParameters);

            int newIndex = EditorGUI.Popup(rect, label.text, index, displayOptions);
            string newValue = newIndex == 0 ? null : animatorParameters[newIndex - 1].name;

            if (nameProperty.stringValue.Equals(newValue, System.StringComparison.Ordinal) == false)
            {
                nameProperty.stringValue = newValue;
            }

            EditorGUI.EndProperty();
        }

        public static AnimatorController GetAnimatorController(SerializedProperty property, string animatorName)
        {
            object target = PropertyUtility.GetTargetObjectWithProperty(property);

            FieldInfo animatorFieldInfo = ReflectionUtility.GetField(target, animatorName);
            if (animatorFieldInfo != null &&
                animatorFieldInfo.FieldType == typeof(Animator))
            {
                Animator animator = animatorFieldInfo.GetValue(target) as Animator;
                if (animator != null)
                {
                    AnimatorController animatorController = animator.runtimeAnimatorController as AnimatorController;
                    return animatorController;
                }
            }

            PropertyInfo animatorPropertyInfo = ReflectionUtility.GetProperty(target, animatorName);
            if (animatorPropertyInfo != null &&
                animatorPropertyInfo.PropertyType == typeof(Animator))
            {
                Animator animator = animatorPropertyInfo.GetValue(target) as Animator;
                if (animator != null)
                {
                    AnimatorController animatorController = animator.runtimeAnimatorController as AnimatorController;
                    return animatorController;
                }
            }

            MethodInfo animatorGetterMethodInfo = ReflectionUtility.GetMethod(target, animatorName);
            if (animatorGetterMethodInfo != null &&
                animatorGetterMethodInfo.ReturnType == typeof(Animator) &&
                animatorGetterMethodInfo.GetParameters().Length == 0)
            {
                Animator animator = animatorGetterMethodInfo.Invoke(target, null) as Animator;
                if (animator != null)
                {
                    AnimatorController animatorController = animator.runtimeAnimatorController as AnimatorController;
                    return animatorController;
                }
            }

            return null;
        }

        public static string[] GetDisplayOptions(List<AnimatorControllerParameter> animatorParams)
        {
            string[] displayOptions = new string[animatorParams.Count + 1];
            displayOptions[0] = "(None)";

            for (int i = 0; i < animatorParams.Count; i++)
            {
                displayOptions[i + 1] = animatorParams[i].name;
            }

            return displayOptions;
        }
    }
}
#endif

