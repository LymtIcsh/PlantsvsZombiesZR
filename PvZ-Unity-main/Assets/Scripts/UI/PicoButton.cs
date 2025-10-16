using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PicoButton : Button
{
    // Alpha ��ֵ�����ڻ��˷�ʽ���
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
    /// �ϸ���� Sprite �ı�Ե�жϵ���Ƿ���Ч��
    /// 1. ���Ի�ȡ Sprite ������״���жϵ�����Ƿ��ڶ�����ڲ���
    /// 2. ���������״δ���ã�����˵����� alpha ��ֵ�Ĳ����жϡ�
    /// </summary>
    /// <param name="sp">��Ļ���λ��</param>
    /// <param name="eventCamera">�¼������</param>
    /// <returns>�Ƿ�����Ч</returns>
    public bool IsRaycastLocationValid(Vector2 sp, Camera eventCamera)
    {
        // ���� Sprite ���ã���ֹ����ʱ�޸ģ�
        _sprite = _image.sprite;
        RectTransform rectTransform = transform as RectTransform;
        Vector2 localPos;
        // ����Ļ����ת��Ϊ RectTransform ���꣨����� pivot��
        if (!RectTransformUtility.ScreenPointToLocalPointInRectangle(rectTransform, sp, eventCamera, out localPos))
        {
            return false;
        }

        // ���㱾�ع�һ�����꣨0~1����ע�� localPos ���� rectTransform.pivot Ϊԭ��
        Vector2 rectSize = rectTransform.rect.size;
        Vector2 normalizedPos = new Vector2(
            (localPos.x + rectSize.x * rectTransform.pivot.x) / rectSize.x,
            (localPos.y + rectSize.y * rectTransform.pivot.y) / rectSize.y
        );

        // ����һ������ӳ�䵽 Sprite ����������
        Rect textureRect = _sprite.textureRect;
        Vector2 spritePixelPos = new Vector2(
            textureRect.x + textureRect.width * normalizedPos.x,
            textureRect.y + textureRect.height * normalizedPos.y
        );
        // ת��Ϊ����� Sprite pivot��������״������ڴ�����ϵ��
        // ע�⣺Sprite.pivot ��������Ϊ��λ����ʾ����� Sprite �ײ�����λ��
        Vector2 relativePos = spritePixelPos - _sprite.pivot;

        // ���Ի�ȡ Sprite ������״�������������
        List<Vector2> physicsShape = new List<Vector2>();
        // �˴����� 0 ��ʾʹ�õ�һ��������״���ݣ�����ж��������
        _sprite.GetPhysicsShape(0, physicsShape);

        if (physicsShape != null && physicsShape.Count > 0)
        {
            // �жϵ�����Ƿ���������״�������
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
            // ��δ����������״�������ʹ�� alpha ��ֵ���
            // ����˫���Բ�����ʽ��ø�ƽ����Ч��
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
                Debug.LogError("IsRaycastLocationValid �����쳣: " + e.Message);
                Destroy(this);
                return false;
            }
        }
    }

    /// <summary>
    /// ���߷��жϵ��Ƿ��ڶ������
    /// </summary>
    /// <param name="point">������</param>
    /// <param name="polygon">����ζ�������</param>
    /// <returns>�Ƿ��ڶ������</returns>
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
