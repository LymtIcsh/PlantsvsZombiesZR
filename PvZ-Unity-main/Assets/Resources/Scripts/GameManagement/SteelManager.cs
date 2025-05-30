using UnityEngine;
using System.Collections;
using System;

/// <summary>
/// ������ͼ������ - ����ը�����ɺ�Ͷ��ϵͳ
/// </summary>
public class SteelManager : MonoBehaviour
{
    // ����ģʽʵ��
    public static SteelManager Instance { get; private set; }

    // ��ͼ�Ƿ�Ϊ������ͼ
    public static bool IsSteelMap = true;
    // �Ƿ���ں�ը������
    public static bool IsExistBomberCore = true;
    // �Ƿ���ڷ����ߺ���
    public static bool IsExistDefenderCore = false;
    // ��ը�������λ��
    public Transform launchPoint;
    // �յ���������
    public float endAreaX = 0f;
    // �յ�����������
    public float endAreaWidth = 1f;
    // �յ�����߶�����
    public float endAreaYMin = 0f;
    // �յ�����߶���������
    public float endAreaYMax = 5f;
    // ը����ʼ�ٶ�����
    public float bombInitialSpeed = 5f;
    // ը���������ٶ�����
    public float gravity = 9.81f;

    // [����] ��Ϊ˽���ֶ�
    [SerializeField]
    private float _bombSpawnInterval = -1f;
    // [����] ��������
    public float bombSpawnInterval
    {
        get { return _bombSpawnInterval; }
        set
        {
            if (_bombSpawnInterval != value)
            {
                _bombSpawnInterval = value;
                if (isBombingActive) UpdateBombingInterval(); // ��̬���¼��
            }
        }
    }

    // ÿ�η����ը����������
    public int minBombsPerLaunch = 1;
    // ÿ�η����ը��������������
    public int maxBombsPerLaunch = 5;

    [Header("�ڵ���ת����")]
    public float initialRotationOffset = 0f;
    public bool enableInFlightRotation = true;
    public float inFlightRotationSpeed = 180f;
    public bool faceTowardsVelocity = true;

    [Header("���ӻ�����")]
    [Tooltip("�������ӻ���ɫ")]
    public Color launchGizmoColor = new Color(1f, 0.5f, 0.5f, 0.8f); // ��͸����ɫ

    [Tooltip("�յ�������ӻ���ɫ")]
    public Color endGizmoColor = new Color(0.5f, 0.5f, 1f, 0.8f); // ��͸����ɫ

    [Tooltip("�Ƿ�������ʾ��Χ")]
    public bool alwaysShowGizmo = true; // �Ƿ�������ʾ��Χ

    // ���ƺ�ը�Ƿ񼤻�ı�־λ
    private bool isBombingActive = false;

    private void Awake()
    {
        // ȷ������Ψһ��
        if (Instance == null)
        {
            Instance = this;
        }
    }

    //private void Update()
    //{
    //    // ����Ƿ���Ҫ������ը
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
    /// ����ը��Ͷ��ϵͳ
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
    /// ֹͣը��Ͷ��ϵͳ
    /// </summary>
    private void StopBombing()
    {
        if (isBombingActive)
        {
            isBombingActive = false;
            CancelInvoke(nameof(SpawnBombs));
        }
    }

    // [����] ��̬���¼������
    private void UpdateBombingInterval()
    {
        CancelInvoke(nameof(SpawnBombs)); // ȡ���ɼ��
        InvokeRepeating(nameof(SpawnBombs), _bombSpawnInterval, _bombSpawnInterval); // Ӧ���¼��
    }

    /// <summary>
    /// ����ը�����������˶��켣
    /// </summary>
    private void SpawnBombs()
    {
        // ������ɱ��η����ը������
        int bombsToLaunch = UnityEngine.Random.Range(minBombsPerLaunch, maxBombsPerLaunch + 1);

        for (int i = 0; i < bombsToLaunch; i++)
        {
            // ȷ����Ҫ�������
            if (launchPoint == null)
            {
                return;
            }

            // ��������յ������Ŀ��λ��
            Vector3 targetPosition = GetRandomTargetPosition();

            GameObject bomb = DynamicObjectPoolManager.Instance.GetFromPool(PoolType.SteelBullet);
            bomb.transform.position = launchPoint.position;
            bomb.name = $"Bomb_{Time.time}_{i}";

            // ���㲢Ӧ�ó�ʼ�ٶ�
            ApplyBombPhysics(bomb, targetPosition);
            SetInitialRotation(bomb, targetPosition);
            StartCoroutine(MoveBomb(bomb, targetPosition));
        }
    }

    /// <summary>
    /// ��ȡ���Ŀ��λ��
    /// </summary>
    private Vector3 GetRandomTargetPosition()
    {
        float randomX = UnityEngine.Random.Range(endAreaX - endAreaWidth / 2, endAreaX + endAreaWidth / 2);
        float randomY = UnityEngine.Random.Range(endAreaYMin, endAreaYMax);
        return new Vector3(randomX, randomY, 0f);
    }

    /// <summary>
    /// Ӧ��ը���������Ժͳ�ʼ�ٶ�
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
        bomb.transform.rotation = Quaternion.Euler(0, 0, angle - 90f); // ����ģ�ͳ���
    }

    /// <summary>
    /// ����ը���˶���Ӧ������
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
            // ǿ���������ʱͷ���£�����ģ������Ϊ0�㣬����Ϊ270�㣩
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
    /// ��Scene��ͼ�л��Ʒ������յ�����
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
    /// ���ƿ��ӻ���������
    /// </summary>
    private void DrawGizmos()
    {
        // ���Ʒ����
        if (launchPoint != null)
        {
            Gizmos.color = launchGizmoColor;
            Gizmos.DrawSphere(launchPoint.position, 0.5f);

            //// �ڱ༭������ʾ��ǩ
            //UnityEditor.Handles.Label(launchPoint.position, "ը�������",
            //    new GUIStyle() { normal = new GUIStyleState() { textColor = launchGizmoColor } });
        }

        // �����յ�����
        DrawTargetAreaGizmo();
    }

    /// <summary>
    /// ����Ŀ������Ŀ��ӻ���������
    /// </summary>
    private void DrawTargetAreaGizmo()
    {
        Gizmos.color = endGizmoColor;

        // ����������ĸ���
        Vector3 topLeft = new Vector3(endAreaX - endAreaWidth / 2, endAreaYMax, 0);
        Vector3 topRight = new Vector3(endAreaX + endAreaWidth / 2, endAreaYMax, 0);
        Vector3 bottomLeft = new Vector3(endAreaX - endAreaWidth / 2, endAreaYMin, 0);
        Vector3 bottomRight = new Vector3(endAreaX + endAreaWidth / 2, endAreaYMin, 0);

        // ����������
        Gizmos.DrawLine(topLeft, topRight);
        Gizmos.DrawLine(topRight, bottomRight);
        Gizmos.DrawLine(bottomRight, bottomLeft);
        Gizmos.DrawLine(bottomLeft, topLeft);

        // ���ƶԽ���
        Gizmos.DrawLine(topLeft, bottomRight);
        Gizmos.DrawLine(topRight, bottomLeft);

        // ������λ�û���һ��С����
        Vector3 center = new Vector3(endAreaX, (endAreaYMin + endAreaYMax) / 2, 0);
        Gizmos.DrawSphere(center, 0.2f);

        // ������ֱ�ǩ
#if UNITY_EDITOR
        UnityEditor.Handles.Label(center, "ը���յ�����",
            new GUIStyle() { normal = new GUIStyleState() { textColor = endGizmoColor } });
#endif
    }

    // [����] �༭��ʵʱ����
    private void OnValidate()
    {
        if (isBombingActive) UpdateBombingInterval(); // �༭���޸�ʵʱ��Ч
    }
}

public enum SteelBonus//������ͼ�ӳ�����
{
    ����,
    ����,
    �һ�,
    ����
}