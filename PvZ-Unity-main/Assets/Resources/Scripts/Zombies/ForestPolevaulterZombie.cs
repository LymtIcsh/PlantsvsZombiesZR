using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Serialization;

public class ForestPolevaulterZombie : ForestZombie
{
    [FormerlySerializedAs("子物体")] [Header("子物体")]
    public PoleVaultingDetector subObj;
    [FormerlySerializedAs("杆子")] [Header("杆子")]
    public GameObject pole;
    protected override void Start()
    {
        base.Start();
        subObj.row = pos_row;
       
    }

    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        base.OnTriggerEnter2D(collision);
        if (collision.tag == "Tombstone")
        {

            if (collision.GetComponent<Bushes>() != null)
            {
                if (collision.GetComponent<Bushes>().pos_row == pos_row)
                {
                    SwitchFuriousState(true);
                }


            }
        }
    }

/// <summary>
/// 删除杆子
/// </summary>
    public void RemovePole()
    {
        Destroy(pole);
    }
    public void DetectIfInterrupt()
    {
        if (subObj.plantGrid.nowPlant != null && subObj.plantGrid.nowPlant.GetComponent<Plant>().tallPlant)
        {
            myAnimator.SetBool("Interrupt", true);
            OpenCollider();
            AudioManager.Instance.PlaySoundEffect(52);
        }
    }

    public override void CloseCollider()
    {
        base.CloseCollider();
        CanBite = false;
        GetComponentInParent<Animator>().SetBool("Walk", true);
        GetComponentInParent<Animator>().SetBool("Attack", false);
        isEating = false;
        CurrentBiteTarget = null;
    }

    public override void OpenCollider()
    {
        base.OpenCollider();
        CanBite = true;
        GetComponentInParent<Animator>().SetBool("Walk", true);
        GetComponentInParent<Animator>().SetBool("Attack", false);
        isEating = false;
        CurrentBiteTarget = null;
    }

    protected override void dropArm()
    {
        if (!armIsDrop)
        {
            armIsDrop = true;

            AudioManager.Instance.PlaySoundEffect(59);

            // 查找名为 "Cone" 的所有子物体（包括未激活的）
            Transform createPosition = FindInChildren(transform, "CreateDropArmPosition");
            Transform shouldBeHide1 = FindInChildren(transform, "Zombie_polevaulter_outerarm_lower");
            Transform shouldBeHide2 = FindInChildren(transform, "Zombie_outerarm_hand");
            Transform shouldBeExchange = FindInChildren(transform, "Zombie_polevaulter_outerarm_upper");
            if (shouldBeExchange != null)
            {
                shouldBeExchange.GetComponent<SpriteRenderer>().sprite = brokenArm;
                shouldBeExchange.gameObject.SetActive(true);
                shouldBeHide1.GetComponent<SpriteRenderer>().enabled = false;
                shouldBeHide2.GetComponent<SpriteRenderer>().enabled = false;
                if (!GameManagement.isPerformance)
                {
                    GameObject gameObject = Instantiate(zombieArmDrops, createPosition.position, Quaternion.identity);
                    gameObject.GetComponent<ParticleSystemRenderer>().sortingLayerName = GetComponent<SortingGroup>().sortingLayerName;
                }
            }
        }
    }
    protected override void hideHead()
    {
        if (!dying)
        {
            dying = true;
            StartCoroutine(DyingHealthDeduction());
            AudioManager.Instance.PlaySoundEffect(59);
            Transform createPosition = FindInChildren(transform, "Zombie_head");
            SpriteRenderer shouldBeHideSpriteRenderer = createPosition.GetComponent<SpriteRenderer>();
            SetChildrenInactive(createPosition);
            shouldBeHideSpriteRenderer.enabled = false;
            if (!dontHaveDropHead && !GameManagement.isPerformance)
            {
                GameObject gameObject = Instantiate(zombieHeadDrops, createPosition.position, Quaternion.identity);
                gameObject.GetComponent<ParticleSystemRenderer>().sortingLayerName = GetComponent<SortingGroup>().sortingLayerName;
            }
        }
    }

    void SetChildrenInactive(Transform parent)
    {
        foreach (Transform child in parent)
        {
            // 将子物体设为不活跃
            child.gameObject.SetActive(false);

            // 递归设置子物体的子物体为不活跃
            SetChildrenInactive(child);
        }
    }
}
