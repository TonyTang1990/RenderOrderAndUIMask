/*
 * Description:             SpriteRendererMenuItem.cs
 * Author:                  TonyTnag
 * Create Date:             2023/03/14
 */

using UnityEditor;
using UnityEngine;

/// <summary>
/// SpriteRendererMenuItem.cs
/// SpriteRenderer菜单扩展
/// </summary>
public static class SpriteRendererMenuItem
{
    /// <summary>
    /// 打印对象材质Render信息
    /// </summary>
    /// <param name="menuCommand"></param>
    [MenuItem("CONTEXT/SpriteRenderer/PrintMaterialsRenderInfo")]
    static void PrintMaterialsRenderInfo(MenuCommand menuCommand)
    {
        Debug.Log($"PrintMaterialsRenderInfo() menuCommand.context.GetType().Name:{menuCommand.context.GetType().Name}");
        var spriteRenderer = (SpriteRenderer)menuCommand.context;
        for(int i = 0; i < spriteRenderer.shareMaterials.Length; i++)
        {
            var material = spriteRenderer.shareMaterials[i];
            if(material != null)
            {
                Debug.Log($"Materials[{i}].name:{maetrial.name} RenderQueue:{maetrial.renderQueue}");
            }
        }
    }
}