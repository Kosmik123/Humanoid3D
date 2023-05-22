using UnityEngine;
using UnityEditor;

namespace Bipolar.Humanoid3D.Animation
{
    [System.Serializable]
    public struct AnimationParameter
    {
        [SerializeField]
        private string name;
        [SerializeField]
        private int hash;

        public int Value => hash;

        public AnimationParameter(string name)
        {
            this.name = name;
            hash = Animator.StringToHash(name);
        }
    }

    [CustomPropertyDrawer(typeof(AnimationParameter))]
    public class AnimationParameterDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            EditorGUI.BeginProperty(position, label, property);

            float labelWidth = 0.43f;
            var labelRect = position;
            labelRect.width *= labelWidth;
            EditorGUI.PrefixLabel(labelRect, label);

            var fieldRect = position;
            fieldRect.width *= (1 - labelWidth);
            fieldRect.x += labelRect.width;
            EditorGUI.TextField(fieldRect, property.FindPropertyRelative("name").stringValue);

            EditorGUI.EndProperty();
        }
    }
}
