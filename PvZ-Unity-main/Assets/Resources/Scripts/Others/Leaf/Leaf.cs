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

    public void returnStatusToFalse()//用于受击动画后设置受到攻击为假，防止重复调用
    {

        if(!GameManagement.isPerformance)
        {
            GameObject dropLeaf = Instantiate(leafDrops, gameObject.transform.position, Quaternion.identity);
        }
        

        leafAnimator.SetBool("BeAttacked", false);
    }

}
