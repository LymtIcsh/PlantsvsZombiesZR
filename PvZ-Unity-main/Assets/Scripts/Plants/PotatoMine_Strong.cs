using System.Collections;
using System.Collections.Generic;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;

public class PotatoMine_Strong : PotatoMine
{
    public float moveSpeed = 8f; // �ƶ��ٶ�
    private GameObject currentGrid; // ��ǰ���ڵ� PlantGrid
    private Vector3 targetPosition; // Ŀ��λ��
    private bool isMoving = false; // �Ƿ������ƶ�

    protected override void Start()
    {
        base.Start();
        Rise();
    }

    public override void initialize(PlantGrid grid, string sortingLayer, int sortingOrder)
    {
        base.initialize(grid, sortingLayer, sortingOrder);
        this.transform.position -= new Vector3(0, 0.2f, 0);
    }

    protected override void Explode()
    {
        AudioManager.Instance.PlaySoundEffect(22);
        CameraShake.Instance.Shake(0.2f, 0.06f);
        Collider2D[] array = Physics2D.OverlapCircleAll(base.transform.position, 0.8f);//�뾶Ϊ0.8��Ȧ
        foreach (Collider2D collider2D in array)
        {
            if (collider2D.CompareTag("Zombie"))
            {
                Zombie component = collider2D.GetComponent<Zombie>();
                if (component == null) return;
                if (component.pos_row == this.row)
                {
                    component.beAttacked(Attack, 2, 1);//���Ӷ��໤��
                }
            }
        }
        GameObject potatoExplosion = Instantiate(PotatoExplosion, this.transform.position, Quaternion.identity);
        Destroy(potatoExplosion, 1f);
        explode = false;
    }


    void Update()
    {
        if (isMoving)
        {
            // ��������ƶ������ƶ���Ŀ��λ��
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);

            // ����Ƿ��Ѿ�����Ŀ��λ��
            if (Vector3.Distance(transform.position, targetPosition) < 0.01f)
            {
                GetComponent<Plant>().row = currentGrid.GetComponent<PlantGrid>().row;
                isMoving = false; // ֹͣ�ƶ�
            }

            return; // ��������ƶ����򲻽����µ�����
        }


        // ����û��ļ�������
        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
        {
            MoveToDirection(Vector3.up);
        }
        else if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
        {
            MoveToDirection(Vector3.down);
        }
        else if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
        {
            MoveToDirection(Vector3.left);
        }
        else if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
        {
            MoveToDirection(Vector3.right);
        }
    }

    /// <summary>
    /// ���ƶ�
    /// </summary>
    public void MoveUp()
    {
        MoveToDirection(Vector3.up);
    }

    /// <summary>
    /// ���ƶ�
    /// </summary>
    public void MoveDown()
    {
        MoveToDirection(Vector3.down);
    }

    /// <summary>
    /// ���ƶ�
    /// </summary>
    public void MoveLeft()
    {
        MoveToDirection(Vector3.left);
    }

    /// <summary>
    /// ���ƶ�
    /// </summary>
    public void MoveRight()
    {
        MoveToDirection(Vector3.right);
    }
    // ���ݷ���Ѱ������� PlantGrid�����ƶ�����Ӧλ��
    public void MoveToDirection(Vector3 direction)
    {
        // ��ȡ���д��� "PlantGrid" ��ǩ������
        GameObject[] grids = GameObject.FindGameObjectsWithTag("PlantGrid");

        // ����Ŀ��λ��
        Vector3 desiredPosition = transform.position + direction;

        // �ҵ������ PlantGrid
        GameObject closestGrid = null;
        float closestDistance = Mathf.Infinity;

        foreach (GameObject grid in grids)
        {
            // �ų���ǰ���ڵ� Grid
            if (grid == currentGrid) continue;

            // ���� grid ��Ŀ��λ�õľ���
            float distance = Vector3.Distance(grid.transform.position, desiredPosition);

            if (distance < closestDistance)
            {
                closestDistance = distance;
                closestGrid = grid;
            }
        }

        // ����ҵ�Ŀ�� Grid����ʼ�ƶ�
        if (closestGrid != null)
        {
            currentGrid = closestGrid; // ���µ�ǰ���ڵ� Grid
            targetPosition = closestGrid.transform.position; // ����Ŀ��λ��
            isMoving = true; // ����Ϊ�����ƶ�״̬
        }
    }

    // �ҵ����嵱ǰ���ڵ� PlantGrid
    void FindCurrentGrid()
    {
        GameObject[] grids = GameObject.FindGameObjectsWithTag("PlantGrid");
        float closestDistance = Mathf.Infinity;

        foreach (GameObject grid in grids)
        {
            float distance = Vector3.Distance(transform.position, grid.transform.position);

            if (distance < closestDistance)
            {
                closestDistance = distance;
                currentGrid = grid;
            }
        }
    }
}
