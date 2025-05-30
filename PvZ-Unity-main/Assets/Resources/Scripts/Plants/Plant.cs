using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering;

[RequireComponent(typeof(SpriteRenderer))]
public class Plant : MonoBehaviour
{
    #region 定义
    public int ID;

    public PlantType 植物类型;//0为正常，1为地刺（不可啃咬，不可被子弹攻击），3为低矮植物(可被啃咬，不可被子弹攻击)

    public bool tallPlant = false;

    [HideInInspector]
    public PlantGrid myGrid;   //该植物所在Grid

    [HideInInspector]
    public int row;  //该植物在第几行


    [HideInInspector]
    public PlantStruct plantStruct;
    [HideInInspector]
    public int 血量;

    [HideInInspector]
    public int 最大血量;
    [HideInInspector]
    public int Armor;
    //public int ArmorMax;

    [HideInInspector]
    public PlantState state = PlantState.Normal;
    [HideInInspector]
    protected int warmSource = 0;  //周围有几个温暖源
    [HideInInspector]
    protected bool intensified = false;   //是否处于强化状态


    [HideInInspector]
    public Material highlightMaterial; // 在Inspector中指定的高光材质
    [HideInInspector]
    public Renderer[] allRenderers;    // 所有 Renderer 组件
    [HideInInspector]
    public Coroutine 高亮;

    public bool 可死亡 = true;//是否可以死亡
    public bool 可被攻击 = true;//是否可以被攻击

    [HideInInspector]
    public ForestSlider forestSlider;

    [HideInInspector]
    public Animator animator;

    private float 狂暴速度乘区 = 1f;
    private float 减速速度乘区 = 1f;
    private float 随机速度乘区 = 1f;
    private float 关卡特殊乘区 = 1f;
    [SerializeField]
    private float _环境速度乘区 = 1f;


    protected virtual float 环境速度乘区//用于覆写
    {
        get => _环境速度乘区;
        set => _环境速度乘区 = value;
    }

    public DetectZombieRegion detectZombieRegion;

    public Sprite ownSprite;
    [HideInInspector]
    protected TMP_Text 血量显示;
    #endregion


    #region 开始的设置
    protected virtual void Awake()
    {
        初始化();
    }

    public void 初始化()
    {
        animator = GetComponent<Animator>();
        plantStruct = PlantStructManager.GetPlantStructById(ID);
        ID = plantStruct.id;
        血量 = plantStruct.HP;
        string resourcePath = "Sprites/Plants/" + plantStruct.plantName;
        if (Resources.Load<Sprite>(resourcePath) != null)
        {
            ownSprite = Resources.Load<Sprite>(resourcePath);
        }
        随机速度乘区 = Random.Range(0.8f, 1.2f);
        加载动画速度();
    }

    protected virtual void Start()
    {
        PlantManagement.AddPlant(gameObject);

        forestSlider = GameManagement.instance.forestSlider;
        //highlightMaterial = Resources.Load<Material>("Shader/highLight");
        //if (highlightMaterial == null)
        //{
        //    Debug.LogError("Failed to load highlight material from Resources!");
        //}
        allRenderers = GetComponentsInChildren<SpriteRenderer>();
        //foreach (Renderer renderer in allRenderers)
        //{
        //    if(renderer.gameObject.GetComponent<ParticleSystem>() == null)
        //        renderer.material = highlightMaterial;
        //}


        最大血量 = 血量;

        TMP_Text 加载血量物体 = Resources.Load<TMP_Text>("Prefabs/Effects/血量显示/普通植物血量显示");
        血量显示 = Instantiate(加载血量物体, transform.position, Quaternion.identity, transform);
        Vector3 parentScale = transform.localScale;
        float safeX = parentScale.x != 0 ? parentScale.x : 1f;
        float safeY = parentScale.y != 0 ? parentScale.y : 1f;
        float safeZ = parentScale.z != 0 ? parentScale.z : 1f;
        血量显示.transform.localScale = new Vector3(
            0.05f / safeX,
            0.05f / safeY,
            0.05f / safeZ
        );
        Vector3 vector = 血量显示.transform.position;
        vector.y -= 0.35f;
        血量显示.transform.position = vector;
        加载血量文本();
        血量显示.gameObject.SetActive(GameManagement.是否显示血量);

        //// 设置图层顺序
        //SetSortingOrderBasedOnRow();
    }

    private void OnDisable()
    {
        PlantManagement.RemovePlant(gameObject);
    }

    private void SetSortingLayer(Transform obj, string layerName)
    {
        // 如果该物体有 SortingGroup，就设置它的 layer
        SortingGroup group = obj.GetComponent<SortingGroup>();
        if (group != null)
        {
            group.sortingLayerName = layerName;
        }

        // 如果该物体有 SpriteRenderer，就设置它的 layer
        SpriteRenderer renderer = obj.GetComponent<SpriteRenderer>();
        if (renderer != null)
        {
            if (obj.name == "Shadow") // 排除名为 Shadow 的物体
            {
                renderer.sortingLayerName = "Shadow";
            }
            else
            {
                renderer.sortingLayerName = layerName;
            }
        }

        // 递归对子物体进行设置
        foreach (Transform child in obj)
        {
            SetSortingLayer(child, layerName);
        }
    }

    void SetSortingOrderRecursive(Transform parent, int parentSortingOrder)
    {
        GetComponent<SortingGroup>().sortingOrder = 50;
    }

    //private void SetSortingOrderBasedOnRow()
    //{
    //    // 计算 sortingOrder，假设每行相差 10
    //    int baseSortingOrder = 1000 - row * 10;

    //    // 设置主物体的 sortingOrder
    //    SetSortingOrderRecursively(gameObject, baseSortingOrder);
    //}

    //private void SetSortingOrderRecursively(GameObject obj, int baseSortingOrder)
    //{
    //    // 设置父物体的 sortingOrder
    //    SpriteRenderer spriteRenderer = obj.GetComponent<SpriteRenderer>();
    //    if (spriteRenderer != null)
    //    {
    //        spriteRenderer.sortingLayerName = "Plant";
    //        spriteRenderer.sortingOrder = baseSortingOrder;
    //    }

    //    // 创建一个列表来存储子物体
    //    List<Transform> children = new List<Transform>();

    //    // 遍历所有子物体并添加到列表中
    //    foreach (Transform child in obj.transform)
    //    {
    //        children.Add(child);
    //    }

    //    // 遍历所有子物体，优先处理 Shadow
    //    foreach (Transform child in children)
    //    {
    //        if (child.name == "Shadow")
    //        {
    //            // 设置 Shadow 的 sortingOrder 为最低值
    //            SpriteRenderer shadowRenderer = child.GetComponent<SpriteRenderer>();
    //            if (shadowRenderer != null)
    //            {
    //                shadowRenderer.sortingLayerName = "Plant";
    //                shadowRenderer.sortingOrder = int.MinValue; // 设置为最底层
    //            }
    //        }
    //    }

    //    // 按照子物体的图层顺序排序
    //    children.Sort((a, b) =>
    //    {
    //        SpriteRenderer srA = a.GetComponent<SpriteRenderer>();
    //        SpriteRenderer srB = b.GetComponent<SpriteRenderer>();

    //        // 比较图层
    //        if (srA != null && srB != null)
    //        {
    //            int layerComparison = srA.sortingOrder.CompareTo(srB.sortingOrder);
    //            if (layerComparison != 0)
    //                return layerComparison;
    //        }
    //        return a.GetSiblingIndex().CompareTo(b.GetSiblingIndex());
    //    });

    //    // 遍历排序后的子物体，设置它们的 sortingOrder
    //    int childOrderOffset = 1;
    //    foreach (Transform child in children)
    //    {
    //        // 跳过 Shadow，因为它已经被设置为最低
    //        if (child.name != "Shadow")
    //        {
    //            SetSortingOrderRecursively(child.gameObject, baseSortingOrder + childOrderOffset);
    //            childOrderOffset++;
    //        }
    //    }

    //}

    #endregion


    public virtual int beAttacked(int hurt, string form, GameObject zombieObject)
    {
        beAttackedMoment(hurt, form, zombieObject);
        TriggerHighlight();
        if (Armor > 0)
        {
            int ArmorDamage = Mathf.Min(hurt, Armor);
            Armor -= ArmorDamage;
            hurt -= ArmorDamage;
        }
        if (hurt > 0)
        {
            血量 -= hurt;
           
            if (血量 <= 0)
            {
                die(form, gameObject);
            }

          
        }
        加载血量文本();
        return 血量;
    }

    public virtual void beAttackedMoment(int hurt, string reason, GameObject zombieObject)
    {

    }

    /// <summary>
    /// 被空爆攻击时触发的方法
    /// </summary>
    /// <param name="type"></param>爆炸类型
    /// <param name="hurt"></param>爆炸伤害
    public virtual void beBombAttacked(int hurt)
    {
        beAttacked(hurt, null, null);
        
    }

    public virtual void cold()
    {
        if (state == PlantState.Normal && !intensified)
        {
            state = PlantState.Cold;
            AudioManager.Instance.PlaySoundEffect(24);
            GetComponent<SpriteRenderer>().color = new Color(0.33f, 0.54f, 1f);
            GetComponent<Animator>().speed = 0.5f;
            Invoke("coldHurt", 1f);
        }
    }

    protected virtual void coldHurt()
    {
        if (state == PlantState.Cold)
        {
            beAttacked(8, "coldHurt", gameObject);
            Invoke("coldHurt", 1f);
        }
    }

    public virtual void warm()
    {
        warmSource++;
        if (warmSource == 1 && !intensified)
        {
            state = PlantState.Warm;
            GetComponent<SpriteRenderer>().color = Color.white;
            GetComponent<Animator>().speed = 1f;
        }
    }

    public virtual void stopWarm()
    {
        warmSource--;
        if (warmSource <= 0 && !intensified)
        {
            normal();
        }
    }

    public virtual void normal()
    {
        state = PlantState.Normal;
        GetComponent<SpriteRenderer>().color = Color.white;
        GetComponent<Animator>().speed = 1f;
    }

    private void 加载动画速度()
    {
        if (animator != null)
        {
            animator.speed = 狂暴速度乘区 * 减速速度乘区 * 随机速度乘区 * 关卡特殊乘区 * 环境速度乘区;
        }
    }

    public virtual void recover(int value)
    {
        TriggerHighlight();
        血量 += value;
        if (血量 > 最大血量) 血量 = 最大血量;
        加载血量文本();
    }

    public virtual void increaseMaxHP(int value)
    {
        最大血量 += value;
        if (最大血量 > 100000)
        {
            最大血量 = 100000;
        }
        加载血量文本();
    }

    //强化函数，执行公共操作并调用特定操作函数
    public void intensify()
    {
        if (!intensified)
        {
            intensified = true;
            normal();
            transform.Find("Halo").gameObject.SetActive(true);
            intensify_specific();
        }
    }

    //强化特定操作
    protected virtual void intensify_specific()
    {
        GetComponent<Animator>().speed = 1.5f;
    }

    //取消强化函数，执行公共操作并调用特定操作函数
    public void cancelIntensify()
    {
        if (intensified)
        {
            intensified = false;
            transform.Find("Halo").gameObject.SetActive(false);
            if (warmSource > 0) state = PlantState.Warm;
            else state = PlantState.Normal;
            cancelIntensify_specific();
        }
    }

    //取消强化特定操作
    protected virtual void cancelIntensify_specific()
    {
        GetComponent<Animator>().speed = 1f;
    }

    public virtual void attack(bool attack)
    {

    }

    public virtual void highlight()
    {
        GetComponent<SpriteRenderer>().color = new Color(0.75f, 0.75f, 0.75f);
    }

    public virtual void cancelHighlight()
    {
        GetComponent<SpriteRenderer>().color = Color.white;
    }

    public virtual void initialize(PlantGrid grid, string sortingLayer, int sortingOrder)
    {
        
        // 设置自己和子物体的排序层
        SetSortingLayer(transform, sortingLayer);
        
        // 遍历所有子物体并设置它们的 sortingOrder
        SetSortingOrderRecursive(transform, sortingOrder);

        if(grid != null)
        {
            row = grid.row;
            myGrid = grid;
        }
        GetComponent<SpriteRenderer>().sortingOrder = sortingOrder;
        if (grid != null)
        {
            row = grid.row;
            myGrid = grid;
        }

        重新计算攻击碰撞箱();
    }


    public void die(string reason, GameObject plant)
    {
        if(可死亡)
        {
            HandleDeath(reason, plant);
        }
        
    }

    private void HandleDeath(string reason, GameObject plant)
    {
        beforeDie();
        PlantManagement.RemovePlant(gameObject);
        GetComponentInParent<PlantGrid>().plantDie(reason,gameObject);
        Destroy(gameObject);

        AfterDestroy();
    }



    public virtual void AfterDestroy()
    {
        
        
    }


    // 死前需要处理的事，由具体植物实现
    protected virtual void beforeDie()
    {

    }



    public virtual void TriggerHighlight()
    {
        if (!GameManagement.isPerformance)
        {
            if (高亮 == null)
            {
                高亮 = StartCoroutine(HighlightCoroutine());
            }
            else
            {
                StopCoroutine(高亮);
                高亮 = StartCoroutine(HighlightCoroutine());
            }

        }

    }

    public virtual IEnumerator HighlightCoroutine()
    {
        float duration = 0.15f; // 高亮和恢复的持续时间
        float startTime = Time.time;

        // 获取当前透明度
        float currentAlpha = 0f;

        // 透明度增加：从当前透明度到最大值 1
        while (Time.time - startTime < duration)
        {
            float lerpValue = Mathf.Lerp(currentAlpha, 0.2f, (Time.time - startTime) / duration); // 从当前透明度到 1
            foreach (Renderer renderer in allRenderers)
            {
                if (renderer != null && renderer.gameObject.name != "Shadow")
                {
                    Material mat = renderer.material;
                    Color currentColor = mat.GetColor("_Color");
                    currentColor.a = lerpValue;  // 更新透明度
                    mat.SetColor("_Color", currentColor);
                }
            }
            yield return null;
        }
        startTime = Time.time;

        // 透明度减少：从 1 到 0
        while (Time.time - startTime < duration)
        {
            float lerpValue = Mathf.Lerp(0.2f, 0f, (Time.time - startTime) / duration); // 从 1 到 0
            foreach (Renderer renderer in allRenderers)
            {
                if (renderer != null && renderer.gameObject.name != "Shadow")
                {
                    Material mat = renderer.material;
                    Color currentColor = mat.GetColor("_Color");
                    currentColor.a = lerpValue;  // 更新透明度
                    mat.SetColor("_Color", currentColor);
                }
            }
            yield return null;
        }

        // 确保透明度最终为 0
        foreach (Renderer renderer in allRenderers)
        {
            if (renderer != null && renderer.gameObject.name != "Shadow")
            {
                Material mat = renderer.material;
                Color currentColor = mat.GetColor("_Color");
                currentColor.a = 0f;  // 最终透明度为 0
                mat.SetColor("_Color", currentColor);
            }
        }
    }

    public virtual void 重新计算攻击碰撞箱()
    {
        Collider2D collider = GetComponent<Collider2D>();
        if(collider != null)
        {
            collider.enabled = false;
            collider.enabled = true;
        }
        if(detectZombieRegion!=null)
        {
            detectZombieRegion.重新计算区域();
        }
    }

    private string 血量文本()
    {
        string healthDisplay = "";

      
        if (Armor > 0)
        {
            healthDisplay = "护甲:" + Armor + "\n" +
                血量 + "/" + 最大血量 + "\n"; ;
        }
        else if(血量 > 0){
            healthDisplay = 血量 + "/" + 最大血量 + "\n";
        }
        return healthDisplay;
    }

    public virtual void 加载血量文本()
    {
        血量显示.text = 血量文本();
    }

    public void 变更血量显示(bool b)
    {
        血量显示.gameObject.SetActive(b);
    }
}
public enum PlantState { Normal, Warm, Cold }

public enum PlantType { 正常植物, 地刺类植物, 低矮植物 }
