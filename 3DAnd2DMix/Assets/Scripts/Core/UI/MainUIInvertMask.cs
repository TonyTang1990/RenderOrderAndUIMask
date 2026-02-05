/*
 * Description:             MainUIInvertMask.cs
 * Author:                  TONYTANG
 * Create Date:             2026/02/05
 */

using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// MainUIInvertMask.cs
/// 主UI反向遮罩
/// </summary>
public class MainUIInvertMask : MonoBehaviour
{
    /// <summary>
    /// 被遮挡的真正UI组件
    /// </summary>
    [Header("被遮挡的真正UI组件")]
    public Button BtnMasked;
    
    /// <summary>
    /// 反向遮罩按钮组件
    /// </summary>
    [Header("反向遮罩按钮组件")]
    public Button BtnInvertMask;

    void Start()
    {
        AddAllListeners();
    }

    /// <summary>
    /// 添加所有监听器
    /// </summary>
    private void AddAllListeners()
    {
        BtnMasked.onClick.AddListener(OnBtnMaskedClick);
        BtnInvertMask.onClick.AddListener(OnBtnInvertMaskClick);
    }

    void Update()
    {
        SyncInvertMaskImagePos();
    }

    /// <summary>
    /// 同步反向遮罩Image的遮挡按钮的位置
    /// </summary>
    private void SyncInvertMaskImagePos()
    {
        if(BtnMasked == null || BtnInvertMask == null)
        {
            return;
        }

        BtnInvertMask.transform.position = BtnMasked.transform.position;
    }

    /// <summary>
    /// 响应被遮挡按钮点击
    /// </summary>
    private void OnBtnMaskedClick()
    {
        Debug.Log($"响应被遮挡按钮点击");
    }

    /// <summary>
    /// 响应遮罩按钮点击
    /// </summary>
    private void OnBtnInvertMaskClick()
    {
        Debug.Log($"响应遮挡按钮点击");
        BtnMasked?.onClick.Invoke();
    }
}
