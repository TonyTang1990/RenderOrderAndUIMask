/*
 * Description:             PenetrateImage.cs
 * Author:                  TONYTANG
 * Create Date:             2026/02/05
 */

using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// PenetrateImage.cs
/// 穿透Image
/// </summary>
public class PenetrateImage : Image
{
    /// <summary>
    /// 是否激活透明Alpha穿透阈值
    /// </summary>
    [Header("是否激活透明Alpha穿透阈值")]
    public bool EnableAlphaHitTestMinimusThreshold = false;

    /// <summary>
    /// 透明Alpha穿透阈值(<=0表示不可穿透，>=1表示全可穿透，其他值表示小于该值可穿透)
    /// Note：
    /// 仅当EnableAlphaHitTestMinimusThreshold=true时有效
    /// </summary>
    [Header("透明Alpha穿透阈值")]
    public float AlphaHitTestMinimumThreshold = 0.1f;

    protected override void Start()
    {
        base.Start();
        UpdateAlphaHitTestMinimumThreshold();
    }

    /// <summary>
    /// 更新透明Alpha穿透阈值
    /// Note:
    /// 外部修改EnableAlphaHitTestMinimusThreshold或AlphaHitTestMinimumThreshold值后
    /// 请调用此方法确保更新成功
    /// </summary>
    public void UpdateAlphaHitTestMinimumThreshold()
    {
        alphaHitTestMinimumThreshold = EnableAlphaHitTestMinimusThreshold ? AlphaHitTestMinimumThreshold : 0f;
    }
}
