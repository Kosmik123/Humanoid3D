using System.Collections.Generic;
using UnityEditor;
using UnityEditor.IMGUI.Controls;
using UnityEngine;

namespace Bipolar.FSM.Editor
{
    public class FSMWindow : EditorWindow
    {
        private FSMTreeView treeView;
        private TreeViewState treeViewState;

        [MenuItem("Window/FSM TreeView")]
        private static void ShowWindow()
        {
            var window = GetWindow<FSMWindow>();
            window.titleContent = new GUIContent("FSM Editor");
            window.minSize = new Vector2(300, 400);
            window.Show();
        }

        private void OnEnable()
        {
            treeViewState ??= new TreeViewState();
            treeView ??= new FSMTreeView(treeViewState);
            treeView.Reload();
        }

        private void OnGUI()
        {
            var rect = new Rect(0, 0, 300, 400);
            treeView.OnGUI(rect);
        }
    }

    public class FSMTreeView : TreeView
    {
        public FSMTreeView(TreeViewState state) : base(state)
        {
        }

        protected override TreeViewItem BuildRoot()
        {
            showBorder = false;
            var root = new TreeViewItem
            {
                id = 0,
                depth = -1,
                displayName = "FSM Root",
            };

            var allItems = new List<TreeViewItem>
            {
                new TreeViewItem { id = 1, depth = 0, displayName = "Element A" },
                new TreeViewItem { id = 2, depth = 1, displayName = "Element A.1" },
                new TreeViewItem { id = 4, depth = 0, displayName = "Element B" },
                new TreeViewItem { id = 3, depth = 1, displayName = "Element A.2" },
                new TreeViewItem { id = 8, depth = 2, displayName = "Element A.2" },
                new TreeViewItem { id = 23, depth = 1, displayName = "Element A.2" },
            };

            SetupParentsAndChildrenFromDepths(root, allItems);
            return root;
        }

        
    }


    public class FSMTreeViewItemBase : TreeViewItem
    {
        public FSMTreeViewItemBase(int id, int depth, string displayName) : base(id, depth, displayName)
        {
        }
    }


    public class StateTreeViewItem : FSMTreeViewItemBase
    {
        public StateTreeViewItem(int id, int depth, string displayName) : base(id, depth, displayName)
        {
        }
    }

    public class TransitionTreeViewItem : FSMTreeViewItemBase
    {
        public TransitionTreeViewItem(int id, int depth, string displayName) : base(id, depth, displayName)
        {
        }
    }

}

