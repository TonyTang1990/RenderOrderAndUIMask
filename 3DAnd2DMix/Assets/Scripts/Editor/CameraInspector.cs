/*
 * Description:             CameraInspector.cs
 * Author:                  TonyTnag
 * Create Date:             2023/03/14
 */

using UnityEditor;
using UnityEngine;

/// <summary>
/// CameraInspector.cs
/// Camera组件Inspector扩展显示
/// </summary>
[CustomEditor(typeof(Camera))]
public class CameraInspector : Editor
{
    // Note:
    // opaqueSortMode和TransparentSortMode属性好像不属于Serialized成员，FindProperty得不到

    /// <summary>
    /// 目标组件对象
    /// </summary>
    private Camera mTarget;

    /// <summary>
    /// opaqueSortMode属性
    /// </summary>
    private SerializedProperty mOpaqueSortModeProperty;

    /// <summary>
    /// transparencySortMode属性
    /// </summary>
    private SerializedProperty mTransparencySortModeProperty;

    private void Enable()
    {
        mTarget = (Camera)target;
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        serializedObject.Update();
        if(mTarget != null)
        {
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LableField("opaqueSortMode", GUILayout.Width(170f));
            EditorGUI.BeginChangeCheck();
            var newOpaqueSortMode = (OpaqueSortMode)EditorGUILayout.EnumPopup(mTarget.opaqueSortMode);
            if(EditorGUI.EndChangeCheck())
            {
                mTarget.opaqueSortMode = newOpaqueSortMode;
            }
            EditorGUILayout.EndHorizontal();
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LableField("transparencySortMode", GUILayout.Width(170f));
            EditorGUI.BeginChangeCheck();
            var newTransparencySortMode = (OpaqueSortMode)EditorGUILayout.EnumPopup(mTarget.transparencySortMode);
            if(EditorGUI.EndChangeCheck())
            {
                mTarget.transparencySortMode = newTransparencySortMode;
            }
            EditorGUILayout.EndHorizontal();
        }
        serializedObject.ApplyModifiedProperties();
    }
}