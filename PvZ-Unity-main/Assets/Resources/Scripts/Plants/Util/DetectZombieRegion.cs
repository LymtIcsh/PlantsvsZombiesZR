using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectZombieRegion : MonoBehaviour
{
    // 1. ����ö�����ͣ�Inspector �л���ʾΪ������
    public enum AttackSpeed
    {
        Fast,    // ��Ӧ 1.4 ��
        Slow     // ��Ӧ 3.0 ��
    }

    [Header("�����ٶ�ö�� (Ĭ�� Fast = 1.4s)")]
    public AttackSpeed attackSpeed = AttackSpeed.Fast;  // 2. Ĭ��ֵΪ Fast

    // 3. ֻ�����ԣ�����ö�ٷ��ؾ����ʱ����
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

        // Trigger ���Ͳ�����Э�̣���ʹ�� attackInterval
        if (isTriggerAttack)
        {
            StartCoroutine(RepeatedAttack());
        }

        // �����������㱣�ֲ���
        float rightEdge = 5.3f;
        float leftEdge = myPlant.transform.position.x;
        myCollider.size = new Vector2(rightEdge - leftEdge, myCollider.size.y);
        myCollider.offset = new Vector2((rightEdge - leftEdge) / 2, 0);
        myCollider.enabled = true;
    }
/// <summary>
/// ���¼�������
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

    // 4. ��Ӳ����� 1.4f ���� attackInterval
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
