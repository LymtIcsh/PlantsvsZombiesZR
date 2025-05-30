using UnityEngine;
using System.Collections;
using System;

/// <summary>
/// 钢铁地图管理器 - 控制炸弹生成和投放系统
/// </summary>
public class SteelManager : MonoBehaviour
{
    // 单例模式实现
    public static SteelManager Instance { get; private set; }

    // 地图是否为钢铁地图
    public static bool IsSteelMap = true;
    // 是否存在轰炸机核心
    public static bool IsExistBomberCore = true;
    // 是否存在防御者核心
    public static bool IsExistDefenderCore = false;
    // 轰炸机发射点位置
    public Transform launchPoint;
    // 终点区域设置
    public float endAreaX = 0f;
    // 终点区域宽度设置
    public float endAreaWidth = 1f;
    // 终点区域高度设置
    public float endAreaYMin = 0f;
    // 终点区域高度设置上限
    public float endAreaYMax = 5f;
    // 炸弹初始速度设置
    public float bombInitialSpeed = 5f;
    // 炸弹重力加速度设置
    public float gravity = 9.81f;

    // [新增] 改为私有字段
    [SerializeField]
    private float _bombSpawnInterval = -1f;
    // [新增] 公共属性
    public float bombSpawnInterval
    {
        get { return _bombSpawnInterval; }
        set
        {
            if (_bombSpawnInterval != value)
            {
                _bombSpawnInterval = value;
                if (isBombingActive) UpdateBombingInterval(); // 动态更新间隔
            }
        }
    }

    // 每次发射的炸弹数量设置
    public int minBombsPerLaunch = 1;
    // 每次发射的炸弹数量设置上限
    public int maxBombsPerLaunch = 5;

    [Header("炮弹旋转设置")]
    public float initialRotationOffset = 0f;
    public bool enableInFlightRotation = true;
    public float inFlightRotationSpeed = 180f;
    public bool faceTowardsVelocity = true;

    [Header("可视化设置")]
    [Tooltip("发射点可视化颜色")]
    public Color launchGizmoColor = new Color(1f, 0.5f, 0.5f, 0.8f); // 半透明橙色

    [Tooltip("终点区域可视化颜色")]
    public Color endGizmoColor = new Color(0.5f, 0.5f, 1f, 0.8f); // 半透明蓝色

    [Tooltip("是否总是显示范围")]
    public bool alwaysShowGizmo = true; // 是否总是显示范围

    // 控制轰炸是否激活的标志位
    private bool isBombingActive = false;

    private void Awake()
    {
        // 确保单例唯一性
        if (Instance == null)
        {
            Instance = this;
        }
    }

    //private void Update()
    //{
    //    // 检查是否需要启动轰炸
    //    if (IsSteelMap && IsExistBomberCore)
    //    {
    //        StartBombing();
    //    }
    //    else
    //    {
    //        StopBombing();
    //    }
    //}

    private void OnEnable()
    {
        EventHandler.GameStart += OnGameStart;
    }

    private void OnDisable()
    {
        EventHandler.GameStart -= OnGameStart;
    }

    private void OnGameStart()
    {
        if (GameManagement.levelData.levelEnviornment == "Steel")
        {
            Invoke("StartBombing", 5f);
        }
    }

    /// <summary>
    /// 启动炸弹投放系统
    /// </summary>
    private void StartBombing()
    {
        if (!isBombingActive)
        {
            isBombingActive = true;
            SteelSignController.Instance.ActivateSteelSign(SteelSign.SmallBombing);
            AudioManager.Instance.PlaySoundEffect(62);
            AudioManager.Instance.PlaySoundEffect(40);
            bombSpawnInterval = 5f;
            maxBombsPerLaunch = 1;
        }
    }

    /// <summary>
    /// 停止炸弹投放系统
    /// </summary>
    private void StopBombing()
    {
        if (isBombingActive)
        {
            isBombingActive = false;
            CancelInvoke(nameof(SpawnBombs));
        }
    }

    // [新增] 动态更新间隔方法
    private void UpdateBombingInterval()
    {
        CancelInvoke(nameof(SpawnBombs)); // 取消旧间隔
        InvokeRepeating(nameof(SpawnBombs), _bombSpawnInterval, _bombSpawnInterval); // 应用新间隔
    }

    /// <summary>
    /// 生成炸弹并设置其运动轨迹
    /// </summary>
    private void SpawnBombs()
    {
        // 随机生成本次发射的炸弹数量
        int bombsToLaunch = UnityEngine.Random.Range(minBombsPerLaunch, maxBombsPerLaunch + 1);

        for (int i = 0; i < bombsToLaunch; i++)
        {
            // 确保必要组件存在
            if (launchPoint == null)
            {
                return;
            }

            // 随机生成终点区域的目标位置
            Vector3 targetPosition = GetRandomTargetPosition();

            GameObject bomb = DynamicObjectPoolManager.Instance.GetFromPool(PoolType.SteelBullet);
            bomb.transform.position = launchPoint.position;
            bomb.name = $"Bomb_{Time.time}_{i}";

            // 计算并应用初始速度
            ApplyBombPhysics(bomb, targetPosition);
            SetInitialRotation(bomb, targetPosition);
            StartCoroutine(MoveBomb(bomb, targetPosition));
        }
    }

    /// <summary>
    /// 获取随机目标位置
    /// </summary>
    private Vector3 GetRandomTargetPosition()
    {
        float randomX = UnityEngine.Random.Range(endAreaX - endAreaWidth / 2, endAreaX + endAreaWidth / 2);
        float randomY = UnityEngine.Random.Range(endAreaYMin, endAreaYMax);
        return new Vector3(randomX, randomY, 0f);
    }

    /// <summary>
    /// 应用炸弹物理属性和初始速度
    /// </summary>
    private void ApplyBombPhysics(GameObject bomb, Vector3 targetPosition)
    {
        Rigidbody2D rb = bomb.GetComponent<Rigidbody2D>();
        if (rb == null) rb = bomb.AddComponent<Rigidbody2D>();

        rb.gravityScale = 0f;
        rb.bodyType = RigidbodyType2D.Dynamic;
        rb.freezeRotation = !enableInFlightRotation;

        Vector3 direction = targetPosition - launchPoint.position;
        float horizontalDistance = direction.x;
        float verticalDistance = direction.y;

        float timeToTarget = Mathf.Abs(horizontalDistance) / bombInitialSpeed;
        float initialVerticalVelocity = (verticalDistance + 0.5f * gravity * timeToTarget * timeToTarget) / timeToTarget;
        Vector3 initialVelocity = new Vector3(
            bombInitialSpeed * Mathf.Sign(horizontalDistance),
            initialVerticalVelocity,
            0f
        );

        rb.velocity = initialVelocity;
    }

    private void SetInitialRotation(GameObject bomb, Vector3 targetPosition)
    {
        Rigidbody2D rb = bomb.GetComponent<Rigidbody2D>();
        if (rb == null) return;

        float angle = Mathf.Atan2(rb.velocity.y, rb.velocity.x) * Mathf.Rad2Deg + initialRotationOffset;
        bomb.transform.rotation = Quaternion.Euler(0, 0, angle - 90f); // 修正模型朝向
    }

    /// <summary>
    /// 控制炸弹运动并应用重力
    /// </summary>
    private IEnumerator MoveBomb(GameObject bomb, Vector3 targetPosition)
    {
        Rigidbody2D rb = bomb.GetComponent<Rigidbody2D>();
        if (rb == null) yield break;

        float horizontalDistance = Mathf.Abs(targetPosition.x - launchPoint.position.x);
        float timeToTarget = horizontalDistance / bombInitialSpeed;
        float elapsedTime = 0f;

        while (elapsedTime < timeToTarget && bomb != null)
        {
            rb.velocity += new Vector2(0f, -gravity * Time.deltaTime);
            UpdateProjectileRotation(rb);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        if (bomb != null)
        {
            // 强制设置落地时头朝下（假设模型向上为0°，向下为270°）
            bomb.transform.rotation = Quaternion.Euler(0, 0, 270f);

            GameObject steelBomb = DynamicObjectPoolManager.Instance.GetFromPool(PoolType.SteelBomb);
            steelBomb.transform.position = bomb.transform.position;
            bomb.SetActive(false);
            AudioManager.Instance.PlaySoundEffect(61);
        }
    }

    private void UpdateProjectileRotation(Rigidbody2D rb)
    {
        if (!enableInFlightRotation) return;

        if (faceTowardsVelocity && rb.velocity.magnitude > 0.1f)
        {
            float angle = Mathf.Atan2(rb.velocity.y, rb.velocity.x) * Mathf.Rad2Deg - 90f;
            rb.rotation = Mathf.LerpAngle(rb.rotation, angle, Time.deltaTime * 5f);
        }
        else if (!faceTowardsVelocity)
        {
            rb.angularVelocity = inFlightRotationSpeed;
        }
    }

    /// <summary>
    /// 在Scene视图中绘制发射点和终点区域
    /// </summary>
    private void OnDrawGizmos()
    {
        if (!alwaysShowGizmo) return;

        DrawGizmos();
    }

    private void OnDrawGizmosSelected()
    {
        if (alwaysShowGizmo) return;

        DrawGizmos();
    }

    /// <summary>
    /// 绘制可视化辅助工具
    /// </summary>
    private void DrawGizmos()
    {
        // 绘制发射点
        if (launchPoint != null)
        {
            Gizmos.color = launchGizmoColor;
            Gizmos.DrawSphere(launchPoint.position, 0.5f);

            //// 在编辑器中显示标签
            //UnityEditor.Handles.Label(launchPoint.position, "炸弹发射点",
            //    new GUIStyle() { normal = new GUIStyleState() { textColor = launchGizmoColor } });
        }

        // 绘制终点区域
        DrawTargetAreaGizmo();
    }

    /// <summary>
    /// 绘制目标区域的可视化辅助工具
    /// </summary>
    private void DrawTargetAreaGizmo()
    {
        Gizmos.color = endGizmoColor;

        // 计算区域的四个角
        Vector3 topLeft = new Vector3(endAreaX - endAreaWidth / 2, endAreaYMax, 0);
        Vector3 topRight = new Vector3(endAreaX + endAreaWidth / 2, endAreaYMax, 0);
        Vector3 bottomLeft = new Vector3(endAreaX - endAreaWidth / 2, endAreaYMin, 0);
        Vector3 bottomRight = new Vector3(endAreaX + endAreaWidth / 2, endAreaYMin, 0);

        // 绘制四条边
        Gizmos.DrawLine(topLeft, topRight);
        Gizmos.DrawLine(topRight, bottomRight);
        Gizmos.DrawLine(bottomRight, bottomLeft);
        Gizmos.DrawLine(bottomLeft, topLeft);

        // 绘制对角线
        Gizmos.DrawLine(topLeft, bottomRight);
        Gizmos.DrawLine(topRight, bottomLeft);

        // 在中心位置绘制一个小球标记
        Vector3 center = new Vector3(endAreaX, (endAreaYMin + endAreaYMax) / 2, 0);
        Gizmos.DrawSphere(center, 0.2f);

        // 添加文字标签
#if UNITY_EDITOR
        UnityEditor.Handles.Label(center, "炸弹终点区域",
            new GUIStyle() { normal = new GUIStyleState() { textColor = endGizmoColor } });
#endif
    }

    // [新增] 编辑器实时更新
    private void OnValidate()
    {
        if (isBombingActive) UpdateBombingInterval(); // 编辑器修改实时生效
    }
}

public enum SteelBonus//钢铁地图加成种类
{
    生机,
    防御,
    烈火,
    穿刺
}