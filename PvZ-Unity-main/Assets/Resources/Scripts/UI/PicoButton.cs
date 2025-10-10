using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PicoButton : Button
{
    // Alpha 阈值，用于回退方式检测
    float alphaThreshold = 0.8f;
    private Image _image;
    private Sprite _sprite;

    protected override void Awake()
    {
        base.Awake();
        onClick.AddListener(() => { DoStateTransition(SelectionState.Highlighted, false); });
    }

    protected override void Start()
    {
        base.Start();
        _image = GetComponent<Image>();
        _sprite = _image.sprite;
    }

    public override void OnPointerEnter(PointerEventData eventData)
    {
        base.OnPointerEnter(eventData);
        CursorManagement.SwitchCursor(1);
        if (interactable)
        {
            DoStateTransition(SelectionState.Highlighted, true);
        }
    }

    public override void OnPointerExit(PointerEventData eventData)
    {
        base.OnPointerExit(eventData);
        CursorManagement.SwitchCursor(0);
        if (interactable)
        {
            DoStateTransition(SelectionState.Normal, true);
        }
    }

    /// <summary>
    /// 严格根据 Sprite 的边缘判断点击是否有效：
    /// 1. 尝试获取 Sprite 物理形状，判断点击点是否在多边形内部；
    /// 2. 如果物理形状未设置，则回退到基于 alpha 阈值的采样判断。
    /// </summary>
    /// <param name="sp">屏幕点击位置</param>
    /// <param name="eventCamera">事件摄像机</param>
    /// <returns>是否点击有效</returns>
    public bool IsRaycastLocationValid(Vector2 sp, Camera eventCamera)
    {
        // 更新 Sprite 引用（防止运行时修改）
        _sprite = _image.sprite;
        RectTransform rectTransform = transform as RectTransform;
        Vector2 localPos;
        // 将屏幕坐标转换为 RectTransform 坐标（相对于 pivot）
        if (!RectTransformUtility.ScreenPointToLocalPointInRectangle(rectTransform, sp, eventCamera, out localPos))
        {
            return false;
        }

        // 计算本地归一化坐标（0~1），注意 localPos 是以 rectTransform.pivot 为原点
        Vector2 rectSize = rectTransform.rect.size;
        Vector2 normalizedPos = new Vector2(
            (localPos.x + rectSize.x * rectTransform.pivot.x) / rectSize.x,
            (localPos.y + rectSize.y * rectTransform.pivot.y) / rectSize.y
        );

        // 将归一化坐标映射到 Sprite 的纹理区域
        Rect textureRect = _sprite.textureRect;
        Vector2 spritePixelPos = new Vector2(
            textureRect.x + textureRect.width * normalizedPos.x,
            textureRect.y + textureRect.height * normalizedPos.y
        );
        // 转换为相对于 Sprite pivot（物理形状顶点基于此坐标系）
        // 注意：Sprite.pivot 是以像素为单位，表示相对于 Sprite 底部左侧的位置
        Vector2 relativePos = spritePixelPos - _sprite.pivot;

        // 尝试获取 Sprite 物理形状（多边形轮廓）
        List<Vector2> physicsShape = new List<Vector2>();
        // 此处索引 0 表示使用第一组物理形状数据（如果有多个轮廓）
        _sprite.GetPhysicsShape(0, physicsShape);

        if (physicsShape != null && physicsShape.Count > 0)
        {
            // 判断点击点是否在物理形状多边形内
            if (PointInPolygon(relativePos, physicsShape.ToArray()))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        else
        {
            // 若未设置物理形状，则回退使用 alpha 阈值检测
            // 采用双线性采样方式获得更平滑的效果
            Vector2 uv = new Vector2(
                (textureRect.x + textureRect.width * normalizedPos.x) / _sprite.texture.width,
                (textureRect.y + textureRect.height * normalizedPos.y) / _sprite.texture.height
            );
            try
            {
                Color sampledColor = _sprite.texture.GetPixelBilinear(uv.x, uv.y);
                return sampledColor.a > alphaThreshold;
            }
            catch (UnityException e)
            {
                Debug.LogError("IsRaycastLocationValid 出现异常: " + e.Message);
                Destroy(this);
                return false;
            }
        }
    }

    /// <summary>
    /// 射线法判断点是否在多边形内
    /// </summary>
    /// <param name="point">待检测点</param>
    /// <param name="polygon">多边形顶点数组</param>
    /// <returns>是否在多边形内</returns>
    private bool PointInPolygon(Vector2 point, Vector2[] polygon)
    {
        bool inside = false;
        int j = polygon.Length - 1;
        for (int i = 0; i < polygon.Length; i++)
        {
            if (((polygon[i].y > point.y) != (polygon[j].y > point.y)) &&
                (point.x < (polygon[j].x - polygon[i].x) * (point.y - polygon[i].y) / (polygon[j].y - polygon[i].y) + polygon[i].x))
            {
                inside = !inside;
            }
            j = i;
        }
        return inside;
    }
}
