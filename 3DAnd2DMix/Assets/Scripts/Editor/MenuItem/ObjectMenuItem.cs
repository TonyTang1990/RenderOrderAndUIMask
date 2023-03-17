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
    [MenuItem("CONTEXT/Object/PrintAllProperty")]
    static void PrintAllProperty(MenuCommand menuCommand)
    {
        Debug.Log($"PrintAllProperty() menuCommand.context.name:{menuCommand.context.name}");
        var serializedObject = new SerializedObject(menuCommand.context);
        EditorUtilities.PrintAllProperty(serializedObject);
    }

    /// <summary>
    /// 打印所有对象可见属性
    /// </summary>
    [MenuItem("CONTEXT/Object/PrintAllVisibleProperty")]
    static void PrintAllVisibleProperty(MenuCommand menuCommand)
    {
        Debug.Log($"PrintAllVisibleProperty() menuCommand.context.name:{menuCommand.context.name}");
        var serializedObject = new SerializedObject(menuCommand.context);
        EditorUtilities.PrintAllVisibleProperty(serializedObject);
    }

}