/*
 * Description:             CameraInspector.cs
 * Author:                  TonyTnag
 * Create Date:             2023/03/14
 */

using UnityEditor;
using UnityEngine;
using UnityEngine.Rendering;

/// <summary>
/// CameraInspector.cs
/// Camera组件Inspector扩展显示
/// </summary>
[CanEditMultipleObjects]
[CustomEditor(typeof(Camera))]
public class CameraInspector : CameraEditor
{
    // Note:
    // opaqueSortMode和TransparentSortMode属性好像不属于Serialized成员，FindProperty得不到

    /// <summary>
    /// 目标组件对象
    /// </summary>
    private Camera mTarget;

    // Note:
    // 这里不能重写OnEnable，会导致CameraEditor原始部分流程异常

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        if(mTarget == null)
        {
            mTarget = (Camera)target;
        }
        if (mTarget != null)
        {
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("opaqueSortMode", GUILayout.Width(170f));
            EditorGUI.BeginChangeCheck();
            var newOpaqueSortMode = (OpaqueSortMode)EditorGUILayout.EnumPopup(mTarget.opaqueSortMode);
            if(EditorGUI.EndChangeCheck())
            {
                mTarget.opaqueSortMode = newOpaqueSortMode;
            }
            EditorGUILayout.EndHorizontal();
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("transparencySortMode", GUILayout.Width(170f));
            EditorGUI.BeginChangeCheck();
            var newTransparencySortMode = (TransparencySortMode)EditorGUILayout.EnumPopup(mTarget.transparencySortMode);
            if(EditorGUI.EndChangeCheck())
            {
                mTarget.transparencySortMode = newTransparencySortMode;
            }
            EditorGUILayout.EndHorizontal();
        }
    }
}