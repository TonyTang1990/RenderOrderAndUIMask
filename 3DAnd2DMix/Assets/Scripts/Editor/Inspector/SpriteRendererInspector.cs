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
    /// Materials属性
    /// </summary>
    private SerializedProperty mMaterialsProperty;

    /// <summary>
    /// SpriteRendererEditor类型信息
    /// </summary>
    private EditorType mSpriteRendererEditorType;

    /// <summary>
    /// SpriteRenderer Editor
    /// </summary>
    private EditorType mSpriteRendererEditor;

    /// <summary>
    /// SortingLayer索引
    /// </summary>
    private int mSortingLayerIndex;

    private void Enable()
    {
        UpdateDatas();
        UpdateSortingLayerIndex();
    }

    /// <summary>
    /// 更新数据
    /// </summary>
    private void UpdateDatas()
    {
        if(mMaterialsProperty == null)
        {
            mMaterialsProperty = serializedObject.FindProperty("m_Materials");
        }
        if(mSpriteRendererEditorType == null)
        {
            var assembly = assembly.GetAssembly(typeof(Editor));
            mSpriteRendererEditorType = assembly.GetType("UnityEditor.SpriteRendererEditor", true);
        }
        if(mSpriteRendererEditor == null && mSpriteRendererEditorType != null && target != null)
        {
            mSpriteRendererEditor = Editor.CreateEditor(target, mSpriteRendererEditorType);
        }
    }

    public override void OnInspectorGUI()
    {
        if(mSpriteRendererEditor != null)
        {
            mSpriteRendererEditor.OnInspectorGUI();
        }
        serializedObject.Update();
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