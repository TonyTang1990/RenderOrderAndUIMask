/*
 * Description:             CustomImage.cs
 * Author:                  TONYTANG
 * Create Date:             2026/02/05
 */

using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

/// <summary>
/// CustomImage.cs
/// 自定义Image用于支持反向遮罩和透明点击穿透设置
/// </summary>
public class CustomImage : Image
{
    /// <summary>
    /// 是否开启反向遮罩
    /// </summary>
    [Header("是否开启反向遮罩")]
    public bool EnableInvertMask = false;

    /// <summary>
    /// 是否激活透明Alpha透明可点击阈值
    /// </summary>
    [Header("是否激活透明Alpha透明可点击阈值")]
    public bool EnableAlphaHitTestMinimusThreshold = false;

    /// <summary>
    /// 透明Alpha可点击阈值(<=0表示全部可点击，>1表示全不可点击，其他值表示小于该值不可点击)
    /// Note：
    /// 仅当EnableAlphaHitTestMinimusThreshold=true时有效
    /// </summary>
    [Header("透明Alpha可点击阈值")]
    [Tooltip("<=0表示全部可点击，>1表示全不可点击，其他值表示小于该值不可点击")]
    public float AlphaHitTestMinimumThreshold = 0.1f;

    protected override void Start()
    {
        base.Start();
        UpdateAlphaHitTestMinimumThreshold();
    }

    /// <summary>
    /// 更新透明Alpha可点击阈值
    /// Note:
    /// 外部修改EnableAlphaHitTestMinimusThreshold或AlphaHitTestMinimumThreshold值后
    /// 请调用此方法确保更新成功
    /// </summary>
    public void UpdateAlphaHitTestMinimumThreshold()
    {
        if(EnableAlphaHitTestMinimusThreshold)
        {
            alphaHitTestMinimumThreshold = AlphaHitTestMinimumThreshold;
        }
    }
    
    /// <summary>
    /// See IMaterialModifier.GetModifiedMaterial
    /// </summary>
    public override Material GetModifiedMaterial(Material baseMaterial)
    {
        if(!EnableInvertMask)
        {
            return base.GetModifiedMaterial(baseMaterial);
        }

        return GetInvertMaskModifiedMaterial(baseMaterial);
    }

    /// <summary>
    /// 获取反向遮罩修改材质球
    /// </summary>
    /// <param name="baseMaterial"></param>
    /// <returns></returns>
    protected Material GetInvertMaskModifiedMaterial(Material baseMaterial)
    {
        var toUse = baseMaterial;

        if (m_ShouldRecalculateStencil)
        {
            var rootCanvas = MaskUtilities.FindRootSortOverrideCanvas(transform);
            m_StencilValue = maskable ? MaskUtilities.GetStencilDepth(transform, rootCanvas) : 0;
            m_ShouldRecalculateStencil = false;
        }

        Mask maskComponent = GetComponent<Mask>();
        if (m_StencilValue > 0 && (maskComponent == null || !maskComponent.IsActive()))
        {
            var maskMat = StencilMaterial.Add(toUse, (1 << m_StencilValue) - 1, StencilOp.Keep, CompareFunction.NotEqual, ColorWriteMask.All, (1 << m_StencilValue) - 1, 0);
            StencilMaterial.Remove(m_MaskMaterial);
            m_MaskMaterial = maskMat;
            toUse = m_MaskMaterial;
        }
        return toUse;
    }
}
