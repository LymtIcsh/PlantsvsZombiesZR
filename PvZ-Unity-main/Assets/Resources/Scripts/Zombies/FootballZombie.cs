using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class FootballZombie : Zombie
{
    public override void loadBucket(int loadType)//根据不同伤害加铁桶
    {
        // 查找名为 "Cone" 的所有子物体（包括未激活的）
        Transform bucketTransform = FindInChildren(transform, "ZOMBIE_FOOTBALL_HELMET");

        if (bucketTransform != null)
        {
            // 获取该子物体的SpriteRenderer组件
            SpriteRenderer bucketSpriteRenderer = bucketTransform.GetComponent<SpriteRenderer>();

            if (bucketSpriteRenderer != null)
            {
                switch (loadType)
                {
                    case 1: bucketTransform.gameObject.SetActive(true); bucketSpriteRenderer.sprite = bucketBroken1; break;
                    case 2: bucketSpriteRenderer.sprite = bucketBroken2; break;
                    case 3: bucketSpriteRenderer.sprite = bucketBroken3; break;
                    case 0:
                        if (!GameManagement.isPerformance)
                        {
                            GameObject shatterEffect = Instantiate(bucketDrop, bucketTransform.position, Quaternion.identity);
                        }


                        bucketTransform.gameObject.SetActive(false);
                        bucketSpriteRenderer.enabled = false;
                        break;
                    default: break;
                }

            }
        }
    }

    protected override void dropArm()//手臂掉落
    {
        if (!armIsDrop)
        {
            armIsDrop = true;

            AudioManager.Instance.PlaySoundEffect(59);

            Transform shouldBeHide1 = FindInChildren(transform, "ZOMBIE_FOOTBALL_LEFTARM_UPPER");
            Transform shouldBeHide2 = FindInChildren(transform, "ZOMBIE_FOOTBALL_LEFTARM_LOWER");
            Transform shouldBeHide3 = FindInChildren(transform, "ZOMBIE_FOOTBALL_LEFTARM_EATINGLOWER");
            Transform shouldBeHide4 = FindInChildren(transform, "ZOMBIE_FOOTBALL_LEFTARM_HAND");
            Transform shouldBeHide5 = FindInChildren(transform, "ZOMBIE_FOOTBALL_LEFTARM_EATINGHAND");
            shouldBeHide1.GetComponent<SpriteRenderer>().enabled = false;
            shouldBeHide2.GetComponent<SpriteRenderer>().enabled = false;
            shouldBeHide3.GetComponent<SpriteRenderer>().enabled = false;
            shouldBeHide4.GetComponent<SpriteRenderer>().enabled = false;
            shouldBeHide5.GetComponent<SpriteRenderer>().enabled = false;
            
        }

    }

    protected override void hideHead()
    {
        if (!dying)
        {
            dying = true;

            StartCoroutine(DyingHealthDeduction());
            AudioManager.Instance.PlaySoundEffect(59);

            Transform createPosition = FindInChildren(transform, "createDropHead");
            Transform hidePosition1 = FindInChildren(transform, "ZOMBIE_FOOTBALL_HEAD");
            Transform hidePosition2 = FindInChildren(transform, "ZOMBIE_JAW");
            Transform hidePosition3 = FindInChildren(transform, "ZOMBIE_HAIR");
            hidePosition1.GetComponent<SpriteRenderer>().enabled = false;
            hidePosition2.GetComponent<SpriteRenderer>().enabled = false;
            hidePosition3.GetComponent<SpriteRenderer>().enabled = false;
            if (!dontHaveDropHead && !GameManagement.isPerformance)
            {
                GameObject gameObject = Instantiate(zombieHeadDrops, createPosition.position, Quaternion.identity);
                gameObject.GetComponent<ParticleSystemRenderer>().sortingLayerName = GetComponent<SortingGroup>().sortingLayerName;
            }

        }
    }
}
