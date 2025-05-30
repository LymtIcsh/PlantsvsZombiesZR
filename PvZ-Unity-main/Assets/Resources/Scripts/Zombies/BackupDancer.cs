using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class BackupDancer : Zombie
{
    protected override void hideHead()
    {
        if (!dying)
        {
            dying = true;

            StartCoroutine(±ôËÀ¿ÛÑª());

            AudioManager.Instance.PlaySoundEffect(59);

            Transform createPosition = FindInChildren(transform, "ZOMBIE_DANCER__HEAD");
            Transform hidePosition = FindInChildren(transform, "ZOMBIE_JAW");
            Transform hidePosition2 = FindInChildren(transform, "ZOMBIE_BACKUP_STASH");
            Transform hidePosition3 = FindInChildren(transform, "ZOMBIE_BACKUP_CHOPS(Zombie_disco_chops_part2)");
            Transform hidePosition4 = FindInChildren(transform, "ZOMBIE_BACKUP_CHOPS(Zombie_disco_chops_part1)");
            createPosition.gameObject.SetActive(false);
            hidePosition.gameObject.SetActive(false);
            hidePosition2.gameObject.SetActive(false);
            hidePosition3.gameObject.SetActive(false);
            hidePosition4.gameObject.SetActive(false);

            if (!dontHaveDropHead && !GameManagement.isPerformance)
            {
                GameObject go = DynamicObjectPoolManager.Instance.GetFromPool(PoolType.ZombieNormalHeadDrop);
                go.GetComponent<ParticleSystem>().textureSheetAnimation.RemoveSprite(0);
                go.GetComponent<ParticleSystem>().textureSheetAnimation.AddSprite(fullHead);
                go.transform.position = createPosition.transform.position;
                go.transform.rotation = Quaternion.identity;
                go.GetComponent<ParticleSystemRenderer>().sortingLayerName = GetComponent<SortingGroup>().sortingLayerName;
            }
        }
        
    }

    protected override void dropArm()//ÊÖ±ÛµôÂä
    {
        if (!armIsDrop)
        {
            armIsDrop = true;
            AudioManager.Instance.PlaySoundEffect(59);
            Transform shouldBeHide1 = FindInChildren(transform, "ZOMBIE_BACKUP_OUTERARM_LOWER");
            Transform shouldBeHide2 = FindInChildren(transform, "ZOMBIE_BACKUP_OUTERHAND");
            Transform shouldBeExchange = FindInChildren(transform, "ZOMBIE_BACKUP_OUTERARM_UPPER");
            if (shouldBeExchange != null)
            {
                shouldBeExchange.GetComponent<SpriteRenderer>().sprite = brokenArm;
                shouldBeHide1.gameObject.SetActive(false);
                shouldBeHide2.gameObject.SetActive(false);
                if (!dontHaveDropHead && !GameManagement.isPerformance)
                {
                    GameObject go = DynamicObjectPoolManager.Instance.GetFromPool(PoolType.ZombieNormalArmDrop);

                    go.GetComponent<ParticleSystem>().textureSheetAnimation.RemoveSprite(0);
                    go.GetComponent<ParticleSystem>().textureSheetAnimation.AddSprite(Resources.Load<Sprite>("Sprites/Zombies/ZombieArms/BackupDancerArm"));

                    go.transform.position = shouldBeHide1.transform.position;
                    go.transform.rotation = Quaternion.identity;

                    go.GetComponent<ParticleSystemRenderer>().sortingLayerName = GetComponent<SortingGroup>().sortingLayerName;
                }
            }
        }

    }
}
