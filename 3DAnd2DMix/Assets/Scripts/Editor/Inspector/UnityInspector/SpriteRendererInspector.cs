/*
 * Description:             SpriteRendererInspector.cs
 * Author:                  TonyTnag
 * Create Date:             2023/03/14
 */

using System;
using System.Reflection;
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
    private Type mSpriteRendererEditorType;

    /// <summary>
    /// SpriteRenderer Editor
    /// </summary>
    private Editor mSpriteRendererEditor;

    private void OnEnable()
    {
        mMaterialsProperty = serializedObject.FindProperty("m_Materials");
        var assembly = Assembly.GetAssembly(typeof(Editor));
        mSpriteRendererEditorType = assembly.GetType("UnityEditor.SpriteRendererEditor", true);
        mSpriteRendererEditor = Editor.CreateEditor(target, mSpriteRendererEditorType);
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
}