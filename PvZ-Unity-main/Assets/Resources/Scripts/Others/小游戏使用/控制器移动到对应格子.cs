using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Unity.Burst.Intrinsics.X86.Avx;

public class 控制器移动到对应格子 : MonoBehaviour
{
    public float moveSpeed = 8f; // 移动速度
    private GameObject currentGrid; // 当前所在的 PlantGrid
    private Vector3 targetPosition; // 目标位置
    private bool isMoving = false; // 是否正在移动
    public GameObject wood1;
    public GameObject wood2;
    private List<GameObject> forestSign_Good = new List<GameObject>();
    private bool isWood1Active = true;//用于切换
    private ForestSlider forestSlider;

    public GameObject myFireLine;
    public float Skill_CD;
  
    void Start()
    {
        // 初始时，设置 wood1 激活，wood2 禁用
        wood1.SetActive(true);
        wood2.SetActive(false);
        // 找到物体当前所在的 PlantGrid（基于初始位置）
        FindCurrentGrid();

        forestSlider = GameManagement.instance.forestSlider;
     
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
                wood1.GetComponent<Plant>().row = currentGrid.GetComponent<PlantGrid>().row;
                wood2.GetComponent<Plant>().row = currentGrid.GetComponent<PlantGrid>().row;
                isMoving = false; // 停止移动
            }

            return; // 如果正在移动，则不接受新的输入
        }

        // 检测 Q 键是否按下
        if (Input.GetKeyDown(KeyCode.Q) || Input.GetMouseButtonDown(1))
        {
            // 切换木块的激活状态
            SwitchWoodActiveState();
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

        if (Input.GetKeyDown(KeyCode.E) || Input.GetMouseButtonDown(2) && !isMoving) {
            Skill();
        }

   
    }

    public void 上移动()
    {
        MoveToDirection(Vector3.up);
    }

    public void 下移动()
    {
        MoveToDirection(Vector3.down);
    }

    public void 左移动()
    {
        MoveToDirection(Vector3.left);
    }

    public void 右移动()
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


    // 切换 wood1 和 wood2 的活跃状态
    public void SwitchWoodActiveState()
    {
        if (isWood1Active)
        {
            // 如果 wood1 当前是活跃的，禁用它并激活 wood2
            wood1.SetActive(false);
            wood2.SetActive(true);
        }
        else
        {
            // 如果 wood2 当前是活跃的，禁用它并激活 wood1
            wood1.SetActive(true);
            wood2.SetActive(false);
        }

        // 切换活跃状态
        isWood1Active = !isWood1Active;
    }

    public void Skill() { //使用技能
        print("SKILL");
      
        if (GameManagement.instance.SunText.GetSunNum() < 1000)
        {
            return;
        }
        else {
            GameManagement.instance.SunText.subSun(1000);
        }

        if (wood1.gameObject.activeSelf == true)
        {
            forestSlider.woodDreamBuff();
        }
        else if (wood2.gameObject.activeSelf == true)
        {//火炬树桩可以释放火爆辣椒效果
            GameObject jalanopefireline;

            jalanopefireline = Instantiate(myFireLine, transform.position, Quaternion.identity);
            jalanopefireline.GetComponent<Jalapeno_FireLine>().Camp = 0;
            jalanopefireline.GetComponent<Jalapeno_FireLine>().Attack = 1000;
            jalanopefireline.GetComponent<Jalapeno_FireLine>().Row = wood1.GetComponent<Plant>().row;

        }


    
    }


}
