using UnityEngine;
using UnityEngine.UI;

public class AlphaHitTestImage : Image
{
    [Range(0, 1)]
    public float alphaThreshold = 0.1f;

    protected override void Awake()
    {
        base.Awake();

    }

    public override bool IsRaycastLocationValid(Vector2 screenPoint, Camera eventCamera)
    {
        if (alphaThreshold <= 0)
        {
            return true;
        }

        if (sprite == null || sprite.texture == null)
        {
            return false;
        }

        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            rectTransform,
            screenPoint,
            eventCamera,
            out Vector2 localPoint
        );
        Rect rect = GetPixelAdjustedRect();
        Vector2 normalizedPoint = new Vector2(
            (localPoint.x - rect.x) / rect.width,
            (localPoint.y - rect.y) / rect.height
        );

        Vector2 textureCoord = new Vector2(
            normalizedPoint.x * sprite.rect.width / sprite.texture.width,
            normalizedPoint.y * sprite.rect.height / sprite.texture.height
        );

        if (textureCoord.x < 0 || textureCoord.x > 1 || textureCoord.y < 0 || textureCoord.y > 1)
        {
            return false;
        }
        Color pixelColor = sprite.texture.GetPixelBilinear(textureCoord.x, textureCoord.y);
        return pixelColor.a >= alphaThreshold;
    }
}
