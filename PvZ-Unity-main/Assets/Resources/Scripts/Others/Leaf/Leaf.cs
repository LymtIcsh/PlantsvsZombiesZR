using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Leaf : MonoBehaviour
{
    public int row;
    public GameObject leafDrops;
    public GameObject forestZombie;
    private Animator leafAnimator;

    private void Start()
    {
        leafAnimator = GetComponent<Animator>();

    }

    public void returnStatusToFalse()//�����ܻ������������ܵ�����Ϊ�٣���ֹ�ظ�����
    {

        if(!GameManagement.isPerformance)
        {
            GameObject dropLeaf = Instantiate(leafDrops, gameObject.transform.position, Quaternion.identity);
        }
        

        leafAnimator.SetBool("BeAttacked", false);
    }

}
