using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Rendering;


public class PaperZombie : Zombie
{
    //二爷僵尸

    public GameObject Paper;//报纸
    public Sprite[] PaperSprites;//报纸的三张
    public bool LostPaper;
    public bool Angry;
    public GameObject PaperDrop;
    public AudioClip Paper_falling_Sound;
    public AudioClip[] Anger_Sound;
    protected override void Start()
    {
        base.Start();
        loadPaper(1);

    }

    protected void loadPaper(int loadType) { //改变报纸形态
        SpriteRenderer PaperSpriteRenderer = Paper.GetComponent<SpriteRenderer>();

        switch (loadType)
        {
            case 1: Paper.gameObject.SetActive(true); PaperSpriteRenderer.sprite = PaperSprites[0]; break;
            case 2: PaperSpriteRenderer.sprite = PaperSprites[1]; break;
            case 3: PaperSpriteRenderer.sprite = PaperSprites[2]; break;
            case 0:

                LostPaper = true;
                Paper.gameObject.SetActive(false);
                PaperSpriteRenderer.enabled = false;

                Anger();

                if (!GameManagement.isPerformance)
                {
                    GameObject shatterEffect = Instantiate(PaperDrop, PaperSpriteRenderer.transform.position, Quaternion.identity);
                }



                break;


            default: break;
        }


    }

    protected virtual void Anger() {//二爷生气了
        myAnimator.SetBool("Lost", true);
        FindChildByNameRecursive(gameObject.transform, "Zombie_paper_hands").gameObject.SetActive(false);
        FindChildByNameRecursive(gameObject.transform, "Zombie_paper_hands2").gameObject.SetActive(true);
        FindChildByNameRecursive(gameObject.transform, "Zombie_outerarm_hand").gameObject.SetActive(true);
    }
    private void PlayAngerSound()
    {
        Angry = true;
        int randomIndex = Random.Range(0, 2) == 0 ? 17 : 18;
        AudioManager.Instance.PlaySoundEffect(randomIndex);
    }
    protected override void HandleLevel1ArmorDamage(int hurt)
    {
        if (level1ArmorHealth <= level1ArmorMaxHealth * 2 / 3 && !Level1ArmorHalfDamagedSwitched) {
            loadPaper(2);
            Level1ArmorHalfDamagedSwitched = true;
        }
        if (level1ArmorHealth <= level1ArmorMaxHealth / 3 && !Level1ArmorFullyDamagedSwitched) {
            loadPaper(3);
            Level1ArmorFullyDamagedSwitched = true;
        }
        if (level1ArmorHealth <= 0 && LostPaper == false) {
            loadPaper(0);

        }
        // 这里可以添加二类防具受伤后的逻辑，例如播放二类防具的损坏动画、音效等
        //Debug.Log($"二类防具受到 {hurt} 点伤害");
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
            Transform showPosition = FindInChildren(transform, "Zombie_neck_0");
            FindInChildren(transform, "Zombie_paper_glasses").gameObject.SetActive(false);
            if (createPosition != null)
            {
                createPosition.gameObject.SetActive(false);
            }
            if (hidePosition != null)
            {
                SpriteRenderer shouldBeHide2 = hidePosition.GetComponent<SpriteRenderer>();
                shouldBeHide2.enabled = false;
            }
            if (showPosition != null)
            {
                showPosition.gameObject.SetActive(true);
            }
            if (!dontHaveDropHead && !GameManagement.isPerformance)
            {
                GameObject gameObject = Instantiate(zombieHeadDrops, createPosition.position, Quaternion.identity);
                gameObject.GetComponent<ParticleSystemRenderer>().sortingLayerName = GetComponent<SortingGroup>().sortingLayerName;
            }
        }
    }
}
