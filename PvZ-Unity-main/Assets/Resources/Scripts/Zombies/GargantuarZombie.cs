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
    public Sprite lower_Broken;//以下是手臂、头、头盔的图片
    public Sprite head_Broken;
    public Sprite bucket_Broken1;
    public Sprite bucket_Broken2;
    public GameObject gargantuarBucketDrop;//头盔掉落粒子
    public bool hasBucket;//是否有头盔或铁桶
    public bool hasHelmet;

    public AudioClip gargantuarThump;  // 攻击音效
    public AudioClip gargantuarDeath;  // 死亡音效

    protected override void Awake()
    {
        base.Awake();
    }

    //被攻击
    //剩三分之一血后受击,有铁桶加3000血量，有橄榄球头盔加6000血量
    public override void beAttacked(int hurt, int BulletType, int AttackedMusicType)
    {
        base.beAttacked(hurt, BulletType, AttackedMusicType);
        if (hasBucket)
        {
            if (血量 <= 最大血量 - 3000)
            {
                SpriteRenderer bucketSprite = bucket.GetComponent<SpriteRenderer>();
                bucketSprite.enabled = false;
                GameObject gargantuarBucketDropEffect = Instantiate(gargantuarBucketDrop, bucket.transform.position, Quaternion.identity);
                hasBucket = false;
            }
            else if (血量 <= 最大血量 - 2000)
            {
                SpriteRenderer bucketSprite = bucket.GetComponent<SpriteRenderer>();
                bucketSprite.sprite = bucket_Broken2;
            }
            else if (血量 <= 最大血量 - 1000)
            {
                SpriteRenderer bucketSprite = bucket.GetComponent<SpriteRenderer>();
                bucketSprite.sprite = bucket_Broken1;
            }


            if (血量 <= 最大血量 / 3 * 2)
            {

                SpriteRenderer lowerSprite = gargantuar_outerarm_lower.GetComponent<SpriteRenderer>();
                lowerSprite.sprite = lower_Broken;
            }
            if (血量 <= 最大血量 / 3)
            {
                SpriteRenderer headSprite = gargantuar_head.GetComponent<SpriteRenderer>();
                headSprite.sprite = head_Broken;
            }
        }
        if (hasHelmet)
        {
            if (血量 <= 最大血量 - 6000)
            {
                SpriteRenderer bucketSprite = bucket.GetComponent<SpriteRenderer>();
                bucketSprite.enabled = false;
                GameObject gargantuarBucketDropEffect = Instantiate(gargantuarBucketDrop, bucket.transform.position, Quaternion.identity);
                hasHelmet = false;
            }
            else if (血量 <= 最大血量 - 4000)
            {
                SpriteRenderer bucketSprite = bucket.GetComponent<SpriteRenderer>();
                bucketSprite.sprite = bucket_Broken2;
            }
            else if (血量 <= 最大血量 - 2000)
            {
                SpriteRenderer bucketSprite = bucket.GetComponent<SpriteRenderer>();
                bucketSprite.sprite = bucket_Broken1;
            }


            if (血量 <= 最大血量 / 3 * 2)
            {

                SpriteRenderer lowerSprite = gargantuar_outerarm_lower.GetComponent<SpriteRenderer>();
                lowerSprite.sprite = lower_Broken;
            }
            if (血量 <= 最大血量 / 3)
            {
                SpriteRenderer headSprite = gargantuar_head.GetComponent<SpriteRenderer>();
                headSprite.sprite = head_Broken;
            }
        }
        else
        {
            if (血量 <= 最大血量 / 3 * 2)
            {
                
                SpriteRenderer lowerSprite = gargantuar_outerarm_lower.GetComponent<SpriteRenderer>();
                lowerSprite.sprite = lower_Broken;
            }
            if (血量 <= 最大血量 / 3)
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
