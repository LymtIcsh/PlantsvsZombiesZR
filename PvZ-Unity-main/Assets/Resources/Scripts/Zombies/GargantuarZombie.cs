using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D.Animation;
using UnityEngine.UIElements;

public class GargantuarZombie : Zombie
{
    public GameObject gargantuar_outerarm_lower;
    public GameObject gargantuar_head;
    public GameObject bucket;
    public Sprite lower_Broken;//�������ֱۡ�ͷ��ͷ����ͼƬ
    public Sprite head_Broken;
    public Sprite bucket_Broken1;
    public Sprite bucket_Broken2;
    public GameObject gargantuarBucketDrop;//ͷ����������
    public bool hasBucket;//�Ƿ���ͷ������Ͱ
    public bool hasHelmet;

    public AudioClip gargantuarThump;  // ������Ч
    public AudioClip gargantuarDeath;  // ������Ч

    protected override void Awake()
    {
        base.Awake();
    }

    //������
    //ʣ����֮һѪ���ܻ�,����Ͱ��3000Ѫ�����������ͷ����6000Ѫ��
    public override void beAttacked(int hurt, int BulletType, int AttackedMusicType)
    {
        base.beAttacked(hurt, BulletType, AttackedMusicType);
        if (hasBucket)
        {
            if (Ѫ�� <= ���Ѫ�� - 3000)
            {
                SpriteRenderer bucketSprite = bucket.GetComponent<SpriteRenderer>();
                bucketSprite.enabled = false;
                GameObject gargantuarBucketDropEffect = Instantiate(gargantuarBucketDrop, bucket.transform.position, Quaternion.identity);
                hasBucket = false;
            }
            else if (Ѫ�� <= ���Ѫ�� - 2000)
            {
                SpriteRenderer bucketSprite = bucket.GetComponent<SpriteRenderer>();
                bucketSprite.sprite = bucket_Broken2;
            }
            else if (Ѫ�� <= ���Ѫ�� - 1000)
            {
                SpriteRenderer bucketSprite = bucket.GetComponent<SpriteRenderer>();
                bucketSprite.sprite = bucket_Broken1;
            }


            if (Ѫ�� <= ���Ѫ�� / 3 * 2)
            {

                SpriteRenderer lowerSprite = gargantuar_outerarm_lower.GetComponent<SpriteRenderer>();
                lowerSprite.sprite = lower_Broken;
            }
            if (Ѫ�� <= ���Ѫ�� / 3)
            {
                SpriteRenderer headSprite = gargantuar_head.GetComponent<SpriteRenderer>();
                headSprite.sprite = head_Broken;
            }
        }
        if (hasHelmet)
        {
            if (Ѫ�� <= ���Ѫ�� - 6000)
            {
                SpriteRenderer bucketSprite = bucket.GetComponent<SpriteRenderer>();
                bucketSprite.enabled = false;
                GameObject gargantuarBucketDropEffect = Instantiate(gargantuarBucketDrop, bucket.transform.position, Quaternion.identity);
                hasHelmet = false;
            }
            else if (Ѫ�� <= ���Ѫ�� - 4000)
            {
                SpriteRenderer bucketSprite = bucket.GetComponent<SpriteRenderer>();
                bucketSprite.sprite = bucket_Broken2;
            }
            else if (Ѫ�� <= ���Ѫ�� - 2000)
            {
                SpriteRenderer bucketSprite = bucket.GetComponent<SpriteRenderer>();
                bucketSprite.sprite = bucket_Broken1;
            }


            if (Ѫ�� <= ���Ѫ�� / 3 * 2)
            {

                SpriteRenderer lowerSprite = gargantuar_outerarm_lower.GetComponent<SpriteRenderer>();
                lowerSprite.sprite = lower_Broken;
            }
            if (Ѫ�� <= ���Ѫ�� / 3)
            {
                SpriteRenderer headSprite = gargantuar_head.GetComponent<SpriteRenderer>();
                headSprite.sprite = head_Broken;
            }
        }
        else
        {
            if (Ѫ�� <= ���Ѫ�� / 3 * 2)
            {
                
                SpriteRenderer lowerSprite = gargantuar_outerarm_lower.GetComponent<SpriteRenderer>();
                lowerSprite.sprite = lower_Broken;
            }
            if (Ѫ�� <= ���Ѫ�� / 3)
            {
                SpriteRenderer headSprite = gargantuar_head.GetComponent<SpriteRenderer>();
                headSprite.sprite = head_Broken;
            }
        }
    }

    private void AttackedAudio()
    {
        //audioSource.enabled = true;
        //audioSource.PlayOneShot(gargantuarThump);
    }
    private void DeathAudio()
    {
        //audioSource.enabled = true;
        //audioSource.PlayOneShot(gargantuarDeath);
    }
}
