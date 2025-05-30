using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectZombie3Row : MonoBehaviour
{
    public GameObject myPlant;
    public BoxCollider2D myCollider;
    private HashSet<GameObject> zombiesInRange = new HashSet<GameObject>();  // ���浱ǰ��Χ�ڵĽ�ʬ

    private void Start()
    {
        // ������ײ��Ĵ�С��ƫ����
        float rightEdge = 5.3f;
        float leftEdge = myPlant.transform.position.x;
        myCollider.size = new Vector2(rightEdge - leftEdge, myCollider.size.y);
        myCollider.offset = new Vector2((rightEdge - leftEdge) / 2, 0);
        myCollider.enabled = true;
    }

    // ��ײ�����¼�
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // �����ײ�������Ƿ��ǽ�ʬ��������������ֲ�������ƥ��
        if (collision.CompareTag("Zombie"))
        {
            Zombie zombieGeneric = collision.GetComponent<Zombie>();  // ��ȡZombie���
            //Zombie zombie = collision.GetComponent<Zombie>();  // ��ȡZombie���������еĻ���

            bool isInAttackRange = false;  // ��ǽ�ʬ�Ƿ��ڹ�����Χ��

            // �жϽ�ʬ�Ƿ��ڹ�����Χ�ڣ�����ƥ�䣩
            if (zombieGeneric != null && zombieGeneric.pos_row >= myPlant.GetComponent<Plant>().row - 1
                && zombieGeneric.pos_row <= myPlant.GetComponent<Plant>().row + 1)
            {
                isInAttackRange = true;
            }
            //else if (zombie != null && zombie.pos_row >= myPlant.GetComponent<Plant>().row - 1
            //         && zombie.pos_row <= myPlant.GetComponent<Plant>().row + 1)
            //{
            //    isInAttackRange = true;
            //}

            // �����ʬ�ڹ�����Χ�ڲ����ǵ�һ�ν��룬��ʼ����
            if (isInAttackRange)
            {
                zombiesInRange.Add(collision.gameObject);  // ����ʬ��ӵ���Χ�ڵļ�����
                if (zombiesInRange.Count == 1)  // ����ǵ�һ������Ľ�ʬ����ʼ����
                {
                    myPlant.GetComponent<Animator>().SetBool("Attack", true);
                }
            }
        }
    }

    // ��ײ�˳��¼�
    private void OnTriggerExit2D(Collider2D collision)
    {
        // ����Ƿ��ǽ�ʬ
        if (collision.CompareTag("Zombie"))
        {
            // ֻ���� zombiesInRange �д��ڸ�����ʱ���ŴӼ������Ƴ�
            if (zombiesInRange.Contains(collision.gameObject))
            {
                zombiesInRange.Remove(collision.gameObject);  // �Ӽ������Ƴ��ý�ʬ

                // ���û�н�ʬ�ڷ�Χ�ڣ���ֹͣ����
                if (zombiesInRange.Count == 0)
                {
                    myPlant.GetComponent<Animator>().SetBool("Attack", false);  // ֹͣ��������
                }
            }
        }
    }
}
