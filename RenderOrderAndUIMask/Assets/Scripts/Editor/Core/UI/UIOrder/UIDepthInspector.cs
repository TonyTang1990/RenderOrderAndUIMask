/*
 * Description:             UIDepth.cs
 * Author:                  TONYTANG
 * Create Date:             2023/03/16
 */

using UnityEditor;
using UnityEngine;

namespace TH.Modules.UI
{
    /// <summary>
    /// UIDepthInspector.cs
    /// UIDepth Inspector扩展
    /// </summary>
    [CustomEditor(typeof(UIDepth))]
    public class UIDepthInspector : Editor
    {
        /// <summary>
        /// 目标组件
        /// </summary>
        private UIDepth mTarget;

        private void OnEnable()
        {
            mTarget = (UIDepth)target;
        }

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            serializedObject.Update();
            if(GUILayout.Button("刷新SortingOrder", GUILayout.ExpandWidth(true)))
            {
                mTarget?.refreshUIDepth();
            }
            serializedObject.ApplyModifiedProperties();
        }
    }
}
