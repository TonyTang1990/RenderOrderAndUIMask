/*
 * Description:             SpriteRendererInspector.cs
 * Author:                  TonyTnag
 * Create Date:             2023/03/14
 */

using UnityEditor;
using UnityEngine;

/// <summary>
/// SpriteRendererInspector.cs
/// SpriteRenderer编辑器扩展
/// </summary>
[CustomEditor(typeof(SpriteRenderer))]
public class SpriteRendererInspector : Editor
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

    private void Enable()
    {
        mTarget = (MeshRenderer)target;
        mSortingOrderProperty = serializedObject.FindProperty("m_SortingOrder");
        mMaterialsProperty = serializedObject.FindProperty("m_Materials");
        UpdateSortingLayerIndex();
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        serializedObject.Update();
        EditorGUI.BeginChangeCheck();
        var selectedIndex = EditorGUILayout.Popup("SortingLayerName", mSortingLayersNameArray, mSortingLayerIndex);
        if (EditorGUI.EndChangeCheck())
        {
            mTarget.sortingLayerName = SortingLayer.layers[selectedIndex].name;
            UpdateSortingLayerIndex();
        }
        if (mSortingOrderProperty != null)
        {
            EditorGUILayout.PropertyField(mSortingOrderProperty);
        }
        if(mMaterialsProperty != null)
        {
            for(int i = 0; i < mMaterialsProperty.arraySize; i++)
            {
                var material = mMaterialsProperty.GetArrayElementAtIndex(i);
                if(material != null)
                {
                    EditorGUILayout.BeginHorizontal();
                    EditorGUILayout.LableField($"Materials[{i}]", GUILayout.Width(170f));
                    EditorGUILayout.BeginVertical();
                    var mat = (Material)material.objectReferenceValue;
                    EditorGUILayout.ObjectField(mat, EditorType.MATERIAL_TYPE);
                    EditorGUILayout.BeginHorizontal();
                    EditorGUILayout.LableField("RenderQueue", GUILayout.Width(80f));
                    EditorGUILayout.IntField(mat.renderQueue);
                    EditorGUILayout.EndHorizontal();
                    EditorGUILayout.EndVertical();
                    EditorGUILayout.EndHorizontal();
                }
            }
        }
        serializedObject.ApplyModifiedProeprties();
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
        return mTarget.sortingLayerName == mSortingLayersNameArray;
    }
}