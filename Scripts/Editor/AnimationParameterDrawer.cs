using UnityEditor;
using UnityEngine;
using Bipolar.Humanoid3D.Animation;

namespace Bipolar.Humanoid3D.Editor
{
    [CustomPropertyDrawer(typeof(AnimationParameter))]
    public class AnimationParameterDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            var nameProperty = property.FindPropertyRelative("name");
            EditorGUI.PropertyField(position, nameProperty, label);
        }
    }
}
