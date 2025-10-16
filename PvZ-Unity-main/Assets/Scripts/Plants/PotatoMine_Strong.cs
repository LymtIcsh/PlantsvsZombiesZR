using System.Collections;
using System.Collections.Generic;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;

public class PotatoMine_Strong : PotatoMine
{
    public float moveSpeed = 8f; // 移动速度
    private GameObject currentGrid; // 当前所在的 PlantGrid
    private Vector3 targetPosition; // 目标位置
    private bool isMoving = false; // 是否正在移动

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
        Collider2D[] array = Physics2D.OverlapCircleAll(base.transform.position, 0.8f);//半径为0.8的圈
        foreach (Collider2D collider2D in array)
        {
            if (collider2D.CompareTag("Zombie"))
            {
                Zombie component = collider2D.GetComponent<Zombie>();
                if (component == null) return;
                if (component.pos_row == this.row)
                {
                    component.beAttacked(Attack, 2, 1);//无视二类护甲
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
            // 如果正在移动，逐渐移动到目标位置
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);

            // 检测是否已经到达目标位置
            if (Vector3.Distance(transform.position, targetPosition) < 0.01f)
            {
                GetComponent<Plant>().row = currentGrid.GetComponent<PlantGrid>().row;
                isMoving = false; // 停止移动
            }

            return; // 如果正在移动，则不接受新的输入
        }


        // 检测用户的键盘输入
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
    /// 上移动
    /// </summary>
    public void MoveUp()
    {
        MoveToDirection(Vector3.up);
    }

    /// <summary>
    /// 下移动
    /// </summary>
    public void MoveDown()
    {
        MoveToDirection(Vector3.down);
    }

    /// <summary>
    /// 左移动
    /// </summary>
    public void MoveLeft()
    {
        MoveToDirection(Vector3.left);
    }

    /// <summary>
    /// 右移动
    /// </summary>
    public void MoveRight()
    {
        MoveToDirection(Vector3.right);
    }
    // 根据方向寻找最近的 PlantGrid，并移动到对应位置
    public void MoveToDirection(Vector3 direction)
    {
        // 获取所有带有 "PlantGrid" 标签的物体
        GameObject[] grids = GameObject.FindGameObjectsWithTag("PlantGrid");

        // 计算目标位置
        Vector3 desiredPosition = transform.position + direction;

        // 找到最近的 PlantGrid
        GameObject closestGrid = null;
        float closestDistance = Mathf.Infinity;

        foreach (GameObject grid in grids)
        {
            // 排除当前所在的 Grid
            if (grid == currentGrid) continue;

            // 计算 grid 到目标位置的距离
            float distance = Vector3.Distance(grid.transform.position, desiredPosition);

            if (distance < closestDistance)
            {
                closestDistance = distance;
                closestGrid = grid;
            }
        }

        // 如果找到目标 Grid，开始移动
        if (closestGrid != null)
        {
            currentGrid = closestGrid; // 更新当前所在的 Grid
            targetPosition = closestGrid.transform.position; // 更新目标位置
            isMoving = true; // 设置为正在移动状态
        }
    }

    // 找到物体当前所在的 PlantGrid
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
