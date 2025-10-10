using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class ChickenZombie : Zombie
{
    public GameObject Chicken;
    public GameObject Chicken_Rope;
    public bool created;

    protected override void Start()
    {
        base.Start();
        created = false;
        Chicken_Rope.SetActive(true);
    }

    protected virtual void CreateChicken() 
    {
        created = true;
        for (int i = 0; i < 5; i++) {
            int totalRows = GameManagement.levelData.zombieInitPosY.Count;

            int minRow = Mathf.Max(0, pos_row - 1);
            int maxRow = Mathf.Min(totalRows - 1, pos_row + 1);

            int rand = Random.Range(minRow, maxRow + 1);

            float randY = GameManagement.levelData.zombieInitPosY[rand];
            Vector3 vector3 = gameObject.transform.position;
            vector3.y = randY;
            GameObject chicken = Instantiate(Chicken, vector3, Quaternion.identity);
            Zombie zombieGeneric = chicken.GetComponent<Zombie>();
            zombieGeneric.pos_row = rand;
            zombieGeneric.setPosRow(zombieGeneric.pos_row);
            if(debuff.Charmed)
            {
                zombieGeneric.SwitchCharmedState();
                Debug.Log("¼¦ÇÐ»»÷È»ó");
            }
        }
        AudioManager.Instance.PlaySoundEffect(16);
        
        Chicken_Rope.SetActive(false);
    }

    public override void beAttacked(int hurt, int BulletType, int AttackedMusicType)
    {
        base.beAttacked(hurt, BulletType, AttackedMusicType);
        if (Health <= MaxHealth * 0.9 && created == false) {
            CreateChicken();
        }
    }

   public override void attack() 
    {
        base.attack();
        if (created == false)
        {
            CreateChicken();
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
            Transform hidePosition = FindInChildren(transform, "Zombie_jaw");
            Transform hidePosition2 = FindInChildren(transform, "Hair");
            Transform showPosition = FindInChildren(transform, "Zombie_neck_0");
            SpriteRenderer shouldBeHideSpriteRenderer = createPosition.GetComponent<SpriteRenderer>();
            SpriteRenderer shouldBeHide2 = hidePosition.GetComponent<SpriteRenderer>();
            SpriteRenderer shouldBeHide3 = hidePosition2.GetComponent<SpriteRenderer>();
            shouldBeHide2.enabled = false;
            shouldBeHide3.enabled = false;
            shouldBeHideSpriteRenderer.enabled = false;
            showPosition.gameObject.SetActive(true);
            if (!dontHaveDropHead && !GameManagement.isPerformance)
            {
                GameObject gameObject = Instantiate(zombieHeadDrops, createPosition.position, Quaternion.identity);
                gameObject.GetComponent<ParticleSystemRenderer>().sortingLayerName = GetComponent<SortingGroup>().sortingLayerName;
            }
        }
    }


}
