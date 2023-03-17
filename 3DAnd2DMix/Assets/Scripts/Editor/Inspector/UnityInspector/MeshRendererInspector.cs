/*
 * Description:             MeshRendererInspector.cs
 * Author:                  TonyTnag
 * Create Date:             2023/03/14
 */

using System;
using System.Linq;
using UnityEditor;
using UnityEngine;

/// <summary>
/// MeshRendererInspector.cs
/// MeshRenderer编辑器扩展
/// </summary>
[CustomEditor(typeof(MeshRenderer))]
public class MeshRendererInspector : Editor
{
    /// <summary>
    /// SortingOrder属性
    /// </summary>
    private SerializedProperty mSortingOrderProperty;

    /// <summary>
    /// Materials属性
    /// </summary>
    private SerializedProperty mMaterialsProperty;

    /// <summary>
    /// 目标组件
    /// </summary>
    private MeshRenderer mTarget;

    /// <summary>
    /// SortingLayer名数组
    /// </summary>
    private string[] mSortingLayersNameArray;

    /// <summary>
    /// SortingLayer索引
    /// </summary>
    private int mSortingLayerIndex;

    private void OnEnable()
    {
        mTarget = (MeshRenderer)target;
        mSortingLayersNameArray = SortingLayer.layers.Select(x => x.name).ToArray();
        mSortingOrderProperty = serializedObject.FindProperty("m_SortingOrder");
        mMaterialsProperty = serializedObject.FindProperty("m_Materials");
        UpdateSortingLayerIndex();
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        serializedObject.Update();
        EditorGUI.BeginChangeCheck();
        var selectedIndex = EditorGUILayout.Popup("SortingLayerName", mSortingLayerIndex, mSortingLayersNameArray);
        if(EditorGUI.EndChangeCheck())
        {
            mTarget.sortingLayerName = SortingLayer.layers[selectedIndex].name;
            UpdateSortingLayerIndex();
        }
        if(mSortingOrderProperty != null)
        {
            EditorGUILayout.PropertyField(mSortingOrderProperty);
        }
        if(mMaterialsProperty != null)
        {
            for(int i = 0; i < mMaterialsProperty.arraySize; i++)
            {
                var material = mMaterialsProperty.GetArrayElementAtIndex(i);
                if(material != null && material.objectReferenceValue != null)
                {
                    EditorGUILayout.BeginHorizontal();
                    EditorGUILayout.LabelField($"Materials[{i}]", GUILayout.Width(170f));
                    EditorGUILayout.BeginVertical();
                    var mat = (Material)material.objectReferenceValue;
                    EditorGUILayout.ObjectField(mat, EditorType.MATERIAL_TYPE, false);
                    EditorGUILayout.BeginHorizontal();
                    EditorGUILayout.LabelField("RenderQueue", GUILayout.Width(80f));
                    EditorGUILayout.IntField(mat.renderQueue);
                    EditorGUILayout.EndHorizontal();
                    EditorGUILayout.EndVertical();
                    EditorGUILayout.EndHorizontal();
                }
            }
        }
        serializedObject.ApplyModifiedProperties();
    }

    /// <summary>
    /// 更新SortingLayer索引
    /// </summary>
    private void UpdateSortingLayerIndex()
    {
        mSortingLayerIndex = Array.FindIndex(mSortingLayersNameArray, FindSortingLayerNameIndex);
    }

    /// <summary>
    /// 查找当前SortingLayerName索引
    /// </summary>
    /// <param name="sortingName"></param>
    /// <returns></returns>
    private bool FindSortingLayerNameIndex(string sortingName)
    {
        return mTarget.sortingLayerName == sortingName;
    }
}