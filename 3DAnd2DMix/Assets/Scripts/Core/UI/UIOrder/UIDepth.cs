/*
 * Description:             UIDepth.cs
 * Author:                  TONYTANG
 * Create Date:             2019/12/21
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace TH.Modules.UI
{
    /// <summary>
    /// UIDepth.cs
    /// 解决UI和3D特效显示层级问题
    /// 参考博客:
    /// https://www.xuanyusong.com/archives/3435
    /// </summary>
    public class UIDepth : MonoBehaviour
    {
        /// <summary>
        /// 相对层级值
        /// 找到目标上层第一个Canvas的Order作为基准值计算最终Order
        /// UI计算方式如下:
        /// 上层第一个Canvas.order + RelativeOrder = 最终Order
        /// 粒子计算方式如下:
        /// 上层第一个Canvas.order + ParticleSystem基准Order + RelativeOrder = 最终Order
        /// </summary>
        [Header("相对层级值")]
        public int RelativeOrder = 0;

        /// <summary>
        /// 是否是UI反之是粒子
        /// </summary>
        [Header("是否是UI")]
        public bool IsUI = false;

        /// <summary>
        /// UI是否可点击(仅当IsUI==true有效)
        /// </summary>
        [Header("UI是否可点击")]
        public bool IsUIAvalibleClick = false;

        /// <summary>
        /// Renderer(e.g. ParticleSystemRenderer, SpriteRenderer, MeshRenderer, SkinnedMeshRenderer)组件初始SortingOrder Map<Renderer组件, 初始SortingOrder>
        /// </summary>
        private Dictionary<Renderer, int> mRendererBasicOrderMap;

        void Start()
        {
            mRendererBasicOrderMap = new Dictionary<Renderer, int>();
            refreshUIDepth();
        }

        /// <summary>
        /// 刷新深度信息(当特效挂载到不同深度的面板时手动触发刷新计算以实现正确的Order排序)
        /// </summary>
        public void refreshUIDepth()
        {
            var firstparentcanvas = GetComponentInParent<Canvas>();
            var baseOrder = firstparentcanvas.sortingOrder;
            if (IsUI)
            {
                UpdateUISortingOrder(baseOrder);
            }
            else
            {
                UpdateRendererSortingOrder<ParticleSystemRenderer>(baseOrder);
                UpdateRendererSortingOrder<SpriteRenderer>(baseOrder);
                UpdateRendererSortingOrder<MeshRenderer>(baseOrder);
                UpdateRendererSortingOrder<SkinnedMeshRenderer>(baseOrder);
            }
        }

        /// <summary>
        /// 更新UI的sortingOrder
        /// </summary>
        /// <param name="baseOrder"></param>
        private void UpdateUISortingOrder(int baseOrder)
        {
            var canvas = gameObject.GetOrAddComponet<Canvas>();
            if (IsUIAvalibleClick)
            {
                gameObject.GetOrAddComponet<GraphicRaycaster>();
            }
            canvas.overrideSorting = true;
            canvas.sortingOrder = baseOrder + RelativeOrder;
        }

        /// <summary>
        /// 更新指定T Renderer的sortingOrder
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="baseOrder"></param>
        private void UpdateRendererSortingOrder<T>(int baseOrder) where T : Renderer
        {
            var renderers = GetComponentsInChildren<T>(true);
            foreach (var renderer in renderers)
            {
                if(renderer == null)
                {
                    continue;
                }
                if (!IsContainRendererBasicOrder(renderer))
                {
                    AddRendererBasicOrder(renderer);
                }
                var basicOrder = GetRendererBasicOrder(renderer);
                renderer.sortingOrder = baseOrder + basicOrder + RelativeOrder;
            }
        }

        /// <summary>
        /// 是否包含指定Renderer的初始sortingOrder
        /// </summary>
        /// <param name="renderer"></param>
        /// <returns></returns>
        private bool IsContainRendererBasicOrder(Renderer renderer)
        {
            if(renderer == null)
            {
                return false;
            }
            return mRendererBasicOrderMap.ContainsKey(renderer);
        }

        /// <summary>
        /// 添加Renderer的初始sortingOrder
        /// </summary>
        /// <param name="renderer"></param>
        /// <returns></returns>
        private bool AddRendererBasicOrder(Renderer renderer)
        {
            if (renderer == null)
            {
                return false;
            }
            mRendererBasicOrderMap.Add(renderer, renderer.sortingOrder);
            return true;
        }

        /// <summary>
        /// 获取Renderer的初始sortingOrder
        /// </summary>
        /// <param name="renderer"></param>
        /// <returns></returns>
        private int GetRendererBasicOrder(Renderer renderer)
        {
            if(renderer == null)
            {
                Debug.LogError($"Renderer为空，获取Renderer初始SortingOrder失败！");
                return 0;
            }
            int sortingOrder;
            mRendererBasicOrderMap.TryGetValue(renderer, out sortingOrder);
            return sortingOrder;
        }
    }
}