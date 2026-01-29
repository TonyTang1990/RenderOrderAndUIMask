/*
 * Description:             ParticleClip.cs
 * Author:                  TONYTANG
 * Create Date:             2026/1/29
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ParticleClip.cs
/// 用于粒子系统遮罩裁剪
/// </summary>
 public class ParticleClip : MonoBehaviour
{
    /// <summary>
    /// RectMask2D裁剪区域
    /// </summary>
    [Header("RectMask2D裁剪区域")]
    public Vector4 ClipRect;

    /// <summary>
    /// 子Renderer组件列表
    /// </summary>
    private Renderer[] mChildRenderers;

    void Start()
    {
        var mask = GetComponentInParent<UnityEngine.UI.RectMask2D>();
        if (mask == null)
        {
            return;
        }

        mChildRenderers = GetComponentsInChildren<Renderer>();
        if(mChildRenderers == null || mChildRenderers.Length == 0)
        {
            return;
        }

        Vector3[] vector3s = new Vector3[4];
        mask.rectTransform.GetWorldCorners(vector3s);
        ClipRect = new Vector4(vector3s[0].x, vector3s[0].y, vector3s[2].x, vector3s[2].y);

        foreach (var renderer in mChildRenderers)
        {
            var rendererMaterial = renderer.material;
            if(rendererMaterial == null)
            {
                continue;
            }
            if(rendererMaterial.HasVector("_ClipRect"))
            {
                rendererMaterial.SetVector("_ClipRect", ClipRect);
            }
            if(rendererMaterial.HasFloat("_UNITY_UI_CLIP_RECT"))
            {
                rendererMaterial.SetFloat("_UNITY_UI_CLIP_RECT", 1);
            }
        }
    }
}
