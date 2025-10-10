using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectZombieRegion : MonoBehaviour
{
    // 1. 增加枚举类型，Inspector 中会显示为下拉框
    public enum AttackSpeed
    {
        Fast,    // 对应 1.4 秒
        Slow     // 对应 3.0 秒
    }

    [Header("攻击速度枚举 (默认 Fast = 1.4s)")]
    public AttackSpeed attackSpeed = AttackSpeed.Fast;  // 2. 默认值为 Fast

    // 3. 只读属性，根据枚举返回具体的时间间隔
    private float attackInterval
    {
        get
        {
            switch (attackSpeed)
            {
                case AttackSpeed.Slow:
                    return 3.0f;
                case AttackSpeed.Fast:
                default:
                    return 1.4f;
            }
        }
    }

    public GameObject myPlant;
    public BoxCollider2D myCollider;
    public List<GameObject> zombiesInRegion = new List<GameObject>();

    protected Animator plantAnimator;
    protected bool isTriggerAttack;

    protected virtual void Start()
    {
        plantAnimator = myPlant.GetComponent<Animator>();

        isTriggerAttack = false;
        foreach (var param in plantAnimator.parameters)
        {
            if (param.name == "Attack")
            {
                if (param.type == AnimatorControllerParameterType.Trigger)
                    isTriggerAttack = true;
                break;
            }
        }

        // Trigger 类型才启动协程，并使用 attackInterval
        if (isTriggerAttack)
        {
            StartCoroutine(RepeatedAttack());
        }

        // 下面的区域计算保持不变
        float rightEdge = 5.3f;
        float leftEdge = myPlant.transform.position.x;
        myCollider.size = new Vector2(rightEdge - leftEdge, myCollider.size.y);
        myCollider.offset = new Vector2((rightEdge - leftEdge) / 2, 0);
        myCollider.enabled = true;
    }
/// <summary>
/// 重新计算区域
/// </summary>
    public virtual void Re_CalculateArea()
    {
        float rightEdge = 5.3f;
        float leftEdge = myPlant.transform.position.x;
        myCollider.size = new Vector2(rightEdge - leftEdge, myCollider.size.y);
        myCollider.offset = new Vector2((rightEdge - leftEdge) / 2, 0);
        myCollider.enabled = false;

        if (!isTriggerAttack)
            StopAttack();
        zombiesInRegion.Clear();

        myCollider.enabled = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Zombie"))
        {
            Zombie zombieGeneric = collision.GetComponent<Zombie>();
            if (IsZombieInRow(zombieGeneric) && !zombieGeneric.debuff.Charmed)
            {
                if (!zombiesInRegion.Contains(collision.gameObject))
                    zombiesInRegion.Add(collision.gameObject);

                if (!isTriggerAttack)
                    TriggerAttack();
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        zombiesInRegion.Remove(collision.gameObject);
        zombiesInRegion.RemoveAll(z => z == null);
        if (zombiesInRegion.Count <= 0 && !isTriggerAttack)
            StopAttack();
    }

    private bool IsZombieInRow(Zombie zombieGeneric)
    {
        return zombieGeneric != null && zombieGeneric.pos_row == myPlant.GetComponent<Plant>().row;
    }

    protected void TriggerAttack()
    {
        plantAnimator.SetBool("Attack", true);
    }

    protected void StopAttack()
    {
        if (plantAnimator != null)
            plantAnimator.SetBool("Attack", false);
    }

    // 4. 把硬编码的 1.4f 换成 attackInterval
    protected IEnumerator RepeatedAttack()
    {
        while (true)
        {
            zombiesInRegion.RemoveAll(z => z == null);
            if (zombiesInRegion.Count > 0)
                plantAnimator.SetTrigger("Attack");
            yield return new WaitForSeconds(attackInterval);
        }
    }
}
