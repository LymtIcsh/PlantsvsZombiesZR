using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class Boom : MonoBehaviour
{
    [Tooltip("��ը���ͣ��������ж�")]
    public PoolType poolType = PoolType.CherryBigBoom;

    [Tooltip("�����к�")]
    public int row;

    private Collider2D col2d;
    private BoxCollider2D box;
    private CircleCollider2D circle;

    public BombType bombType = BombType.Plant;
    public int hurt = 1800; // Ĭ���˺�

    private void Awake()
    {
        col2d = GetComponent<Collider2D>();
        box = GetComponent<BoxCollider2D>();
        circle = GetComponent<CircleCollider2D>();

        if (col2d == null)
            Debug.LogError("��Ҫ���� Collider2D��", this);
    }

    private void OnEnable()
    {
        col2d.enabled = true;
        StartCoroutine(ExplodeAndReturn());
    }

    private IEnumerator ExplodeAndReturn()
    {
        // ����һ֡��Ⱦ��������һ��
        yield return new WaitForEndOfFrame();
        // ǿ�ư� Transform �仯ͬ���� Physics2D
        Physics2D.SyncTransforms();

        Collider2D[] hits;

        // ���ݲ�ͬ�� Collider ��������ȷ���
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
            // ��������
            float scale = Mathf.Max(transform.lossyScale.x, transform.lossyScale.y);
            float radius = circle.radius * scale;
            hits = Physics2D.OverlapCircleAll(center, radius);
        }
        else
        {
            // ��һ���������ͻ��˵� bounds������� AABB��
            var b = col2d.bounds;
            hits = Physics2D.OverlapBoxAll(b.center, b.size, 0f);
        }

        // �����������е� Collider
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

        // �ر���ײ�����´θ���ʱ�� OnEnable �����¿���
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
