/*
 * Description:             PenetrateImageEditor.cs
 * Author:                  TONYTANG
 * Create Date:             2026/02/06
 */
 
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.UI;
using UnityEngine;

/// <summary>
/// PenetrateImageEditor.cs
/// PenetrateImage的Inspector扩展
/// </summary>
[CustomEditor(typeof(PenetrateImage))]
[CanEditMultipleObjects]
public class PenetrateImageEditor : ImageEditor
{
    /// <summary>
    /// 是否激活透明Alpha穿透阈值属性
    /// </summary> <summary>
    private SerializedProperty mEnableAlphaHitTestMinimusThreshold;

    /// <summary>
    /// 透明Alpha穿透阈值属性
    /// </summary>
    private SerializedProperty mAlphaHitTestMinimumThreshold;
    
    protected override void OnEnable()
    {
        base.OnEnable();
        mEnableAlphaHitTestMinimusThreshold = serializedObject.FindProperty("EnableAlphaHitTestMinimusThreshold");
        mAlphaHitTestMinimumThreshold = serializedObject.FindProperty("AlphaHitTestMinimumThreshold");
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        serializedObject.Update();

        EditorGUI.BeginChangeCheck();
        EditorGUILayout.PropertyField(mEnableAlphaHitTestMinimusThreshold);
        bool enableChanged = EditorGUI.EndChangeCheck();

        EditorGUI.BeginChangeCheck();
        EditorGUILayout.PropertyField(mAlphaHitTestMinimumThreshold);
        bool thresholdChanged = EditorGUI.EndChangeCheck();

        // 应用序列化属性更改
        serializedObject.ApplyModifiedProperties();

        if (enableChanged || thresholdChanged)
        {
            UpdateAllAlphaHitTextMinimusThresholdoTargets();
        }
    }

    /// <summary>
    /// 更新所有对象的透明Alpha穿透阈值
    /// </summary>
    private void UpdateAllAlphaHitTextMinimusThresholdoTargets()
    {
        if(targets == null)
        {
            return;
        }
        foreach (var targetObj in targets)
        {
            var penetrateImage = targetObj as PenetrateImage;
            if (penetrateImage != null)
            {
                penetrateImage.UpdateAlphaHitTestMinimumThreshold();
                EditorUtility.SetDirty(targetObj);
            }
        }
    }
}
