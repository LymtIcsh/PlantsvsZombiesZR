using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class Boom : MonoBehaviour
{
    [Tooltip("爆炸类型，用于行判定")]
    public PoolType poolType = PoolType.CherryBigBoom;

    [Tooltip("所在行号")]
    public int row;

    private Collider2D col2d;
    private BoxCollider2D box;
    private CircleCollider2D circle;

    public BombType bombType = BombType.Plant;
    public int hurt = 1800; // 默认伤害

    private void Awake()
    {
        col2d = GetComponent<Collider2D>();
        box = GetComponent<BoxCollider2D>();
        circle = GetComponent<CircleCollider2D>();

        if (col2d == null)
            Debug.LogError("需要挂载 Collider2D！", this);
    }

    private void OnEnable()
    {
        col2d.enabled = true;
        StartCoroutine(ExplodeAndReturn());
    }

    private IEnumerator ExplodeAndReturn()
    {
        // 等这一帧渲染和物理都走一遍
        yield return new WaitForEndOfFrame();
        // 强制把 Transform 变化同步到 Physics2D
        Physics2D.SyncTransforms();

        Collider2D[] hits;

        // 根据不同的 Collider 类型做精确检测
        if (box != null)
        {
            // BoxCollider2D
            Vector2 center = (Vector2)transform.position + box.offset;
            Vector2 size = box.size;
            float angle = transform.eulerAngles.z;
            hits = Physics2D.OverlapBoxAll(center, size, angle);
        }
        else if (circle != null)
        {
            // CircleCollider2D
            Vector2 center = (Vector2)transform.position + circle.offset;
            // 考虑缩放
            float scale = Mathf.Max(transform.lossyScale.x, transform.lossyScale.y);
            float radius = circle.radius * scale;
            hits = Physics2D.OverlapCircleAll(center, radius);
        }
        else
        {
            // 万一是其他类型回退到 bounds（轴对齐 AABB）
            var b = col2d.bounds;
            hits = Physics2D.OverlapBoxAll(b.center, b.size, 0f);
        }

        // 遍历所有命中的 Collider
        foreach (var hit in hits)
        {
            if ((!hit.CompareTag("Zombie") && bombType == BombType.Plant) ||
                (!hit.CompareTag("Plant") && bombType == BombType.Zombie))
                continue;

            switch (bombType)
            {
                case BombType.Plant:
                    var zombie = hit.GetComponent<Zombie>();
                    if (zombie != null && IsInRowRange(zombie.pos_row))
                        zombie.beAshAttacked();
                    break;

                case BombType.Zombie:
                    var plant = hit.GetComponent<Plant>();
                    if (plant != null && IsInRowRange(plant.row))
                        plant.beBombAttacked(hurt);
                    break;
            }
        }

        // 关闭碰撞器，下次复用时在 OnEnable 会重新开启
        col2d.enabled = false;
    }

    private bool IsInRowRange(int targetRow)
    {
        switch (poolType)
        {
            case PoolType.Doom:
                return true;
            case PoolType.CherryBigBoom:
                return targetRow >= row - 1 && targetRow <= row + 1;
            case PoolType.SteelBomb:
                return true;
            case PoolType.Fire:
                return targetRow == row;
            default:
                return false;
        }
    }
}


public enum BombType
{
    Plant,
    Zombie
}
