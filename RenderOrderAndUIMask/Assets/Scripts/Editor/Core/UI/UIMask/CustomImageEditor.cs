/*
 * Description:             CustomImageEditor.cs
 * Author:                  TONYTANG
 * Create Date:             2026/02/06
 */
 
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.UI;
using UnityEngine;

/// <summary>
/// CustomImageEditor.cs
/// CustomImage的Inspector扩展
/// </summary>
[CustomEditor(typeof(CustomImage))]
[CanEditMultipleObjects]
public class CustomImageEditor : ImageEditor
{
    /// <summary>
    /// 是否开启反向遮罩属性
    /// </summary>
    private SerializedProperty mEnableInvertMask;

    /// <summary>
    /// 是否激活透明Alpha透明可点击阈值属性
    /// </summary> <summary>
    private SerializedProperty mEnableAlphaHitTestMinimusThreshold;

    /// <summary>
    /// 透明Alpha可点击阈值属性
    /// </summary>
    private SerializedProperty mAlphaHitTestMinimumThreshold;
    
    protected override void OnEnable()
    {
        base.OnEnable();
        mEnableInvertMask = serializedObject.FindProperty("EnableInvertMask");
        mEnableAlphaHitTestMinimusThreshold = serializedObject.FindProperty("EnableAlphaHitTestMinimusThreshold");
        mAlphaHitTestMinimumThreshold = serializedObject.FindProperty("AlphaHitTestMinimumThreshold");
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        serializedObject.Update();

        EditorGUILayout.PropertyField(mEnableInvertMask);

        EditorGUI.BeginChangeCheck();
        EditorGUILayout.PropertyField(mEnableAlphaHitTestMinimusThreshold);
        bool enableChanged = EditorGUI.EndChangeCheck();

        EditorGUI.BeginChangeCheck();
        EditorGUILayout.PropertyField(mAlphaHitTestMinimumThreshold);
        bool thresholdChanged = EditorGUI.EndChangeCheck();

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
            var penetrateImage = targetObj as CustomImage;
            if (penetrateImage != null)
            {
                penetrateImage.UpdateAlphaHitTestMinimumThreshold();
                EditorUtility.SetDirty(targetObj);
            }
        }
    }
}
