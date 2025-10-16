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

    private GraphicRaycaster graphicRaycaster;  // 用于检测UI元素是否遮挡

    void Awake()
    {
        spriteRenderer = GetComponent<Image>();

        // 如果没有手动指定 normalSprite，自动使用当前 SpriteRenderer 的 Sprite
        if (normalSprite == null && spriteRenderer != null)
        {
            normalSprite = spriteRenderer.sprite;
        }
        graphicRaycaster = GetComponentInParent<GraphicRaycaster>();  // 获取父物体的GraphicRaycaster
    }

    void Update()
    {
        // 点击检测（仅当鼠标悬停时）
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
