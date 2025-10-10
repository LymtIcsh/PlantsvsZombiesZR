using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Serialization;

[RequireComponent(typeof(SpriteRenderer))]
public class Plant : MonoBehaviour
{
    #region 定义

    public int ID;

    /// <summary>
    /// 植物类型  //0为正常，1为地刺（不可啃咬，不可被子弹攻击），3为低矮植物(可被啃咬，不可被子弹攻击)
    /// </summary>
    [FormerlySerializedAs("植物类型")] public PlantType _plantType;

    public bool tallPlant = false;

    /// <summary>
    ///  该植物所在Grid
    /// </summary>
    [HideInInspector] public PlantGrid myGrid;

    /// <summary>
    /// 该植物在第几行
    /// </summary>
    [HideInInspector] public int row;


    [HideInInspector] public PlantStruct plantStruct;

    /// <summary>
    /// 血量
    /// </summary>
    [FormerlySerializedAs("血量")] [HideInInspector]
    public int Health;

    /// <summary>
    /// 最大血量
    /// </summary>
    [FormerlySerializedAs("最大血量")] [HideInInspector] public int MaxHealth;
    [HideInInspector] public int Armor;
    //public int ArmorMax;

    [HideInInspector] public PlantState state = PlantState.Normal;

    /// <summary>
    /// 周围有几个温暖源
    /// </summary>
    [HideInInspector] protected int warmSource = 0;

    /// <summary>
    /// 是否处于强化状态
    /// </summary>
    [HideInInspector] protected bool intensified = false;

    /// <summary>
    /// 在Inspector中指定的高光材质
    /// </summary>
    [HideInInspector] public Material highlightMaterial;

    /// <summary>
    /// 所有 Renderer 组件
    /// </summary>
    [HideInInspector] public Renderer[] allRenderers;

    /// <summary>
    /// 高亮
    /// </summary>
    [HideInInspector] public Coroutine _highlight;

    /// <summary>
    /// 可死亡
    /// </summary>
    [FormerlySerializedAs("可死亡")] public bool IsCanDie = true;

    /// <summary>
    /// 可被攻击
    /// </summary>
    [FormerlySerializedAs("可被攻击")] public bool CanSubjectAttack = true; //是否可以被攻击

    [HideInInspector] public ForestSlider forestSlider;

    [HideInInspector] public Animator animator;

    /// <summary>
    /// 狂暴速度乘区
    /// </summary>
    private float _furiousSpeedZone = 1f;
    /// <summary>
    /// 减速速度乘区
    /// </summary>
    private float _decelerationRateZone = 1f;
    /// <summary>
    /// 随机速度乘区
    /// </summary>
    private float _randomSpeedMultiplicationZone = 1f;
    /// <summary>
    /// 关卡特殊乘区
    /// </summary>
    private float _specialMultiplicationAreaOfTheLevel = 1f;
    /// <summary>
    /// 环境速度乘区
    /// </summary>
    [FormerlySerializedAs("_环境速度乘区")] [SerializeField] private float _environmentalVelocityZone = 1f;


    /// <summary>
    /// 环境速度乘区  --用于覆写
    /// </summary>
    protected virtual float EnvironmentalSpeedZone_ForOverwriting //用于覆写
    {
        get => _environmentalVelocityZone;
        set => _environmentalVelocityZone = value;
    }

    public DetectZombieRegion detectZombieRegion;

    public Sprite ownSprite;
    /// <summary>
    /// 血量显示的文本
    /// </summary>
    [HideInInspector] protected TMP_Text _healthShowText;

    #endregion


    #region 开始的设置

    protected virtual void Awake()
    {
        Init();
    }

    /// <summary>
    /// 初始化
    /// </summary>
    public void Init()
    {
        animator = GetComponent<Animator>();
        plantStruct = PlantStructManager.GetPlantStructById(ID);
        ID = plantStruct.id;
        Health = plantStruct.HP;
        string resourcePath = "Sprites/Plants/" + plantStruct.plantName;
        if (Resources.Load<Sprite>(resourcePath) != null)
        {
            ownSprite = Resources.Load<Sprite>(resourcePath);
        }

        _randomSpeedMultiplicationZone = Random.Range(0.8f, 1.2f);
        LoadingAnimationSpeed();
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


        MaxHealth = Health;

        TMP_Text 加载血量物体 = Resources.Load<TMP_Text>("Prefabs/Effects/血量显示/普通植物血量显示");
        _healthShowText = Instantiate(加载血量物体, transform.position, Quaternion.identity, transform);
        Vector3 parentScale = transform.localScale;
        float safeX = parentScale.x != 0 ? parentScale.x : 1f;
        float safeY = parentScale.y != 0 ? parentScale.y : 1f;
        float safeZ = parentScale.z != 0 ? parentScale.z : 1f;
        _healthShowText.transform.localScale = new Vector3(
            0.05f / safeX,
            0.05f / safeY,
            0.05f / safeZ
        );
        Vector3 vector = _healthShowText.transform.position;
        vector.y -= 0.35f;
        _healthShowText.transform.position = vector;
        LoadHealthText();
        _healthShowText.gameObject.SetActive(GameManagement.isShowHp);

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
            Health -= hurt;

            if (Health <= 0)
            {
                die(form, gameObject);
            }
        }

        LoadHealthText();
        return Health;
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

    /// <summary>
    /// 加载动画速度
    /// </summary>
    private void LoadingAnimationSpeed()
    {
        if (animator != null)
        {
            animator.speed = _furiousSpeedZone * _decelerationRateZone * _randomSpeedMultiplicationZone * _specialMultiplicationAreaOfTheLevel * EnvironmentalSpeedZone_ForOverwriting;
        }
    }

    public virtual void recover(int value)
    {
        TriggerHighlight();
        Health += value;
        if (Health > MaxHealth) Health = MaxHealth;
        LoadHealthText();
    }

    public virtual void increaseMaxHP(int value)
    {
        MaxHealth += value;
        if (MaxHealth > 100000)
        {
            MaxHealth = 100000;
        }

        LoadHealthText();
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

        if (grid != null)
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

        Re_CalculateAttackCollisionBox();
    }


    public void die(string reason, GameObject plant)
    {
        if (IsCanDie)
        {
            HandleDeath(reason, plant);
        }
    }

    private void HandleDeath(string reason, GameObject plant)
    {
        beforeDie();
        PlantManagement.RemovePlant(gameObject);
        GetComponentInParent<PlantGrid>().plantDie(reason, gameObject);
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
            if (_highlight == null)
            {
                _highlight = StartCoroutine(HighlightCoroutine());
            }
            else
            {
                StopCoroutine(_highlight);
                _highlight = StartCoroutine(HighlightCoroutine());
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
                    currentColor.a = lerpValue; // 更新透明度
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
                    currentColor.a = lerpValue; // 更新透明度
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
                currentColor.a = 0f; // 最终透明度为 0
                mat.SetColor("_Color", currentColor);
            }
        }
    }
/// <summary>
/// 重新计算攻击碰撞箱
/// </summary>
    public virtual void Re_CalculateAttackCollisionBox()
    {
        Collider2D collider = GetComponent<Collider2D>();
        if (collider != null)
        {
            collider.enabled = false;
            collider.enabled = true;
        }

        if (detectZombieRegion != null)
        {
            detectZombieRegion.Re_CalculateArea();
        }
    }

/// <summary>
/// 血量文本
/// </summary>
/// <returns></returns>
    private string BloodVolumeText()
    {
        string healthDisplay = "";


        if (Armor > 0)
        {
            healthDisplay = "护甲:" + Armor + "\n" +
                            Health + "/" + MaxHealth + "\n";
            ;
        }
        else if (Health > 0)
        {
            healthDisplay = Health + "/" + MaxHealth + "\n";
        }

        return healthDisplay;
    }

    /// <summary>
    /// 加载血量文本
    /// </summary>
    public virtual void LoadHealthText()
    {
        _healthShowText.text = BloodVolumeText();
    }

    /// <summary>
    /// 变更血量显示
    /// </summary>
    /// <param name="b"></param>
    public void ChangeBloodBolumeDisplay(bool b)
    {
        _healthShowText.gameObject.SetActive(b);
    }
}

public enum PlantState
{
    Normal,
    Warm,
    Cold
}

public enum PlantType
{
    /// <summary>
    /// 正常植物
    /// </summary>
    NormalPlants,
    /// <summary>
    /// 地刺类植物
    /// </summary>
    GroundHuggingPlants,
    /// <summary>
    /// 低矮植物
    /// </summary>
    LowGrowingPlants
}