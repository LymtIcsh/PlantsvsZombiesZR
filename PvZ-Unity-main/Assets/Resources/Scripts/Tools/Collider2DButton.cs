using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using UnityEngine.EventSystems;

[RequireComponent(typeof(Collider2D))]
[RequireComponent(typeof(Image))]
public class Collider2DButton : MonoBehaviour
{
    [System.Serializable]
    public class ButtonClickedEvent : UnityEvent { }

    public ButtonClickedEvent onClick = new ButtonClickedEvent();

    [Header("Sprite Settings")]
    public Sprite normalSprite;
    public Sprite highlightedSprite;

    private Image spriteRenderer;
    private bool isPointerOver = false;

    private GraphicRaycaster graphicRaycaster;  // ���ڼ��UIԪ���Ƿ��ڵ�

    void Awake()
    {
        spriteRenderer = GetComponent<Image>();

        // ���û���ֶ�ָ�� normalSprite���Զ�ʹ�õ�ǰ SpriteRenderer �� Sprite
        if (normalSprite == null && spriteRenderer != null)
        {
            normalSprite = spriteRenderer.sprite;
        }
        graphicRaycaster = GetComponentInParent<GraphicRaycaster>();  // ��ȡ�������GraphicRaycaster
    }

    void Update()
    {
        // �����⣨���������ͣʱ��
        if (isPointerOver && Input.GetMouseButtonDown(0))
        {
            onClick.Invoke();
        }
    }

    void OnMouseEnter()
    {
        if(StaticThingsManagement.IsSecondaryPanelOpen)
        {
            return;
        }
        isPointerOver = true;
        

        if (highlightedSprite != null && spriteRenderer != null)
        {
            AudioManager.Instance.PlaySoundEffect(5);
            spriteRenderer.sprite = highlightedSprite;
        }
    }

    void OnMouseExit()
    {
        isPointerOver = false;

        if (normalSprite != null && spriteRenderer != null)
        {
            spriteRenderer.sprite = normalSprite;
        }
    }
}
