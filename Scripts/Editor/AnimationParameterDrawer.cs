using UnityEngine;

namespace Bipolar.Humanoid3D.Animation
{
<<<<<<<< HEAD:Scripts/Editor/AnimationParameterDrawer.cs
#if UNITY_EDITOR
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
========
    [System.Serializable]
    public struct AnimationParameter
    {
        [SerializeField]
        private string name;
        [SerializeField]
        private int hash;
        private bool hasValue;

        public int Value
        {
            get
            {
                if (hasValue == false)
                {
                    hash = Animator.StringToHash(name);
                    hasValue = true;
                }
                return hash;
            }
        }

        public AnimationParameter(string name)
        {
            this.name = name;
            hash = Animator.StringToHash(name);
            hasValue = true;
>>>>>>>> 8e2cf2edbcfb14e9128bd393a1facebfe24ade42:Scripts/Animation/AnimationParameter.cs
        }
    }
#endif
}
