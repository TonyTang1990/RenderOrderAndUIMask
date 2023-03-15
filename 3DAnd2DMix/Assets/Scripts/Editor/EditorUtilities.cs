/*
 * Description:             EditorUtilities.cs
 * Author:                  TonyTnag
 * Create Date:             2023/03/14
 */

using UnityEditor;
using UnityEngine;

/// <summary>
/// EditorUtilities.cs
/// Editor工具类
/// </summary>
pubic static class EditorUtilities
{
    /// <summary>
    /// 打印所有SerializedOject的属性
    /// </summary>
    pubic static bool PrintAllProperty(SerializedObject serializedObject)
    {
        if(serializedObject == null)
        {
            return false;
        }
        var propertyIterator = serializedObject.GetIterator();
        Debug.Log($"First property.Name:{propertyIterator.name}");
        var result = propertyIterator.Next(true);
        while(result)
        {
            Debug.Log($"property.Name:{propertyIterator.name}");
            result = propertyIterator.Next(false);
        }
        return true;
    }

    /// <summary>
    /// 打印所有SerializedOject的可见属性
    /// </summary>
    pubic static bool PrintAllVisibleProperty(SerializedObject serializedObject)
    {
        if(serializedObject == null)
        {
            return false;
        }
        var propertyIterator = serializedObject.GetIterator();
        Debug.Log($"First property.Name:{propertyIterator.name}");
        var result = propertyIterator.NextVisible(true);
        while(result)
        {
            Debug.Log($"property.Name:{propertyIterator.name}");
            result = propertyIterator.NextVisible(false);
        }
        return true;
    }
}