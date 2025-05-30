using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class ForestPolevaulterZombie : ForestZombie
{
    public �Ÿ��������巭Խ�ж� ������;
    public GameObject ����;
    protected override void Start()
    {
        base.Start();
        ������.row = pos_row;
       
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
                    �л���״̬(true);
                }


            }
        }
    }


    public void ɾ������()
    {
        Destroy(����);
    }
    public void DetectIfInterrupt()
    {
        if (������.plantGrid.nowPlant != null && ������.plantGrid.nowPlant.GetComponent<Plant>().tallPlant)
        {
            myAnimator.SetBool("Interrupt", true);
            ������ײ��();
            AudioManager.Instance.PlaySoundEffect(52);
        }
    }

    public override void �ر���ײ��()
    {
        base.�ر���ײ��();
        ���Կ�ҧ = false;
        GetComponentInParent<Animator>().SetBool("Walk", true);
        GetComponentInParent<Animator>().SetBool("Attack", false);
        isEating = false;
        ���ڿ�ҧĿ�� = null;
    }

    public override void ������ײ��()
    {
        base.������ײ��();
        ���Կ�ҧ = true;
        GetComponentInParent<Animator>().SetBool("Walk", true);
        GetComponentInParent<Animator>().SetBool("Attack", false);
        isEating = false;
        ���ڿ�ҧĿ�� = null;
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
            StartCoroutine(������Ѫ());
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
