/*
 * Description:             MeshRendererInspector.cs
 * Author:                  TonyTnag
 * Create Date:             2023/03/14
 */

using UnityEditor;
using UnityEngine;

/// <summary>
/// MeshRendererInspector.cs
///  MeshRenderer编辑器扩展
/// </summary>
[CustomEditor(typeof(SpriteRenderer))]
public class MeshRendererInspector : Editor
{
    /// <summary>
    /// SortingLayer属性
    /// </summary>
    private SerializedProperty mSortingLayerProperty;

    /// <summary>
    /// SortingOrder属性
    /// </summary>
    private SerializedProperty mSortingOrderProperty;

    /// <summary>
    /// Materials属性
    /// </summary>
    private SerializedProperty mMaterialsProperty;

    private void Enable()
    {
        mSortingLayerProperty = serializedObject.FindProperty("m_SortingLayer");
        mSortingOrderProperty = serializedObject.FindProperty("m_SortingOrder");
        mMaterialsProperty = serializedObject.FindProperty("m_Materials")
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        serializedObject.Update();
        if(mSortingLayerProperty != null)
        {
            EditorGUILayout.PropertyField(mSortingLayerProperty);
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
}