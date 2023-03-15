/*
 * Description:             ObjectMenuItem.cs
 * Author:                  TonyTnag
 * Create Date:             2023/03/14
 */

using UnityEditor;
using UnityEngine;

/// <summary>
/// ObjectMenuItem.cs
/// Object菜单扩展
/// </summary>
public static class ObjectMenuItem
{
    /// <summary>
    /// 打印所有对象属性
    /// </summary>
    [MenuItem("CONTEXT/Object/PrintAllProperties")]
    static void PrintAllProperties(MenuCommand menuCommand)
    {
        Debug.Log($"PrintAllProperties() menuCommand.context.name:{menuCommand.context.name}");
        var serializedObject = new SerializedObject(menuCommand.context);
        EditorUtilities.PrintAllProperties(serializedObject);
    }

    /// <summary>
    /// 打印所有对象可见属性
    /// </summary>
    [MenuItem("CONTEXT/Object/PrintAllVisibleProperties")]
    static void PrintAllVisibleProperties(MenuCommand menuCommand)
    {
        Debug.Log($"PrintAllVisibleProperties() menuCommand.context.name:{menuCommand.context.name}");
        var serializedObject = new SerializedObject(menuCommand.context);
        EditorUtilities.PrintAllVisibleProperties(serializedObject);
    }

}