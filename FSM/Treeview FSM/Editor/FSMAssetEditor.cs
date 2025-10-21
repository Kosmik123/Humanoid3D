namespace Bipolar.FSM.Editor
{
    using UnityEditor;
    using UnityEditor.Callbacks;
    using UnityEngine;

    [CustomEditor(typeof(FSMAsset))]
    public class FSMAssetEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            if (GUILayout.Button("Create New FSM"))
            {
                // Logic to create a new FSM
                Debug.Log("Creating new FSM...");
            }
        }

        private static class OnOpenAssetCallbackHandler
        {
            private const string FsmFileExtension = ".fsm";

            [OnOpenAsset]
            public static bool OnOpenAsset(int instanceId, int line)
            {
                var path = AssetDatabase.GetAssetPath(instanceId);
                if (path.EndsWith(FsmFileExtension, System.StringComparison.OrdinalIgnoreCase) == false)
                    return false;

                Debug.Log("This is an FSM asset");
                return true;
            }
        }
    }
}