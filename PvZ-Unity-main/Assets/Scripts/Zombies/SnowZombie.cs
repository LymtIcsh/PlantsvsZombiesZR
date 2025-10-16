using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.U2D.Animation;

public class SnowZombie : ForestZombie
{
    public GameObject iceShield;
    bool haveIce = false;   //�Ƿ��Ѵ��������

    protected override void dropArm()//�ֱ۵���
    {
        if (!armIsDrop)
        {
            armIsDrop = true;

            AudioManager.Instance.PlaySoundEffect(59);

            // ������Ϊ "Cone" �����������壨����δ����ģ�
            Transform createPosition = FindInChildren(transform, "DropArmPosition");
            Transform shouldBeHide1 = FindInChildren(transform, "outerarm_lower");
            Transform shouldBeHide2 = FindInChildren(transform, "outerarm_hand");
            Transform shouldBeExchange = FindInChildren(transform, "outerarm_upper");
            if (shouldBeExchange != null)
            {

                shouldBeExchange.GetComponent<SpriteRenderer>().sprite = brokenArm;
                shouldBeHide1.GetComponent<SpriteRenderer>().enabled = false;
                shouldBeHide2.GetComponent<SpriteRenderer>().enabled = false;
                if (!GameManagement.isPerformance)
                {
                    GameObject gameObject = Instantiate(zombieArmDrops, createPosition.position, Quaternion.identity);
                }
            }
        }

    }

    //������ܣ�ӵ�б����ڼ�ÿ��ָ�200��Ѫ����ֱ�����������ж�
    public void createIce()
    {
        myAnimator.SetBool("CreateIce", false);
        
        GameObject grass = Instantiate(iceShield,
                    transform.position + new Vector3(-0.3f, 0, 0),
                    Quaternion.Euler(0, 0, 0));
        grass.GetComponent<Zombie>().setPosRow(pos_row);
        if (debuff.Charmed)
        {
            grass.GetComponent<Zombie>().SwitchCharmedState();
        }
    }

    //Ѫ���ָ�����
    public void recover()
    {
        Health += 200;
        if(Health >= MaxHealth)
        {
            myAnimator.SetBool("Walk", true);
        }
    }

    public void frozePlant()
    {
        if (attackPlant != null)
        {
            attackPlant.cold();
        }
        myAnimator.SetBool("FrozePlant", false);
    }

  
    //���Ž�ʬ��ҧ����Ч
    public override void PlayEatAudio()
    {
        //audioSource.PlayOneShot(
        //    Resources.Load<AudioClip>("Sounds/Zombies/attack_snowzombie")
        //);
    }

    //������
    public override void beAttacked(int hurt, int bulletTupe,int music)
    {
        base.beAttacked(hurt, bulletTupe,music);
        if (Health <= MaxHealth/2 && haveIce == false)
        {
            haveIce = true;
            myAnimator.SetBool("CreateIce", true);
            myAnimator.SetBool("Walk", false);
        }
    }

    //���ڸ�����ʬͷ���ֿ��ܲ�ͬ���ʸú�����������д
    protected override void hideHead()
    {
        if (!dying)
        {
            dying = true;
            StartCoroutine(DyingHealthDeduction());
            AudioManager.Instance.PlaySoundEffect(59);
            Transform createPosition = FindInChildren(transform, "head");
            Transform hidePosition = FindInChildren(transform, "jaw");
            SpriteRenderer shouldBeHideSpriteRenderer = createPosition.GetComponent<SpriteRenderer>();
            SpriteRenderer shouldBeHide2 = hidePosition.GetComponent<SpriteRenderer>();
            shouldBeHide2.enabled = false;
            shouldBeHideSpriteRenderer.enabled = false;
            if (!dontHaveDropHead && !GameManagement.isPerformance)
            {
                GameObject gameObject = Instantiate(zombieHeadDrops, createPosition.position, Quaternion.identity);
                gameObject.GetComponent<ParticleSystemRenderer>().sortingLayerName = GetComponent<SortingGroup>().sortingLayerName;
            }
            
        }
    }
}
