using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Serialization;

public class PolevaulterZombie : Zombie
{
    [FormerlySerializedAs("������")] [Header("������")]
    public PoleVaultingDetector subObj;
    [FormerlySerializedAs("����")] [Header("����")]
    public GameObject pole;
    protected override void Start()
    {
        base.Start();
        subObj.row = pos_row;
    }

    /// <summary>
    /// ɾ������
    /// </summary>
    public void RemovePole()
    {
        Destroy(pole);
    }

    public void DetectIfInterrupt()
    {
        if(subObj.plantGrid.nowPlant != null && subObj.plantGrid.nowPlant.GetComponent<Plant>().tallPlant)
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
        Debug.Log("��ײ���Ѿ��ر�");
    }

    public override void OpenCollider()
    {
        base.OpenCollider();
        CanBite = true;
        GetComponentInParent<Animator>().SetBool("Walk", true);
        GetComponentInParent<Animator>().SetBool("Attack", false);
        isEating = false;
        CurrentBiteTarget = null;
        Debug.Log("��ײ���ѿ���");
        GetComponent<BoxCollider2D>().enabled = false;
        GetComponent<BoxCollider2D>().enabled = true;
    }

    protected override void dropArm()
    {
        if (!armIsDrop)
        {
            armIsDrop = true;

            AudioManager.Instance.PlaySoundEffect(59);

            // ������Ϊ "Cone" �����������壨����δ����ģ�
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
            // ����������Ϊ����Ծ
            child.gameObject.SetActive(false);

            // �ݹ������������������Ϊ����Ծ
            SetChildrenInactive(child);
        }
    }
}
