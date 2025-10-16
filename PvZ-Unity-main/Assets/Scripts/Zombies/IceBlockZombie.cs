using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D.Animation;

public class IceBlockZombie : Zombie
{
    IceBlockZombieState iceState = IceBlockZombieState.IceComplete;

    public GameObject outerarm_upper;
    public GameObject outerarm_lower;
    public GameObject outerarm_hand;
    public GameObject hat;
    public GameObject jaw;

    //±»¹¥»÷
    //×´Ì¬±¸×¢£º1300ÂúÑª£¬950±ù¿éËðÉË1,600±ù¿éËðÉË2,200±ù¿éµôÂä£¬100¸ì²²µôÂä

    //ÓÐ±ù¿éÊ±£¬»áÊÜµ½ºÜ´óµÄ»ðÉËº¦
    public override void beBurned(int damage)
    {
        RemoveDecelerationState();
        if (iceState == IceBlockZombieState.IceComplete ||
            iceState == IceBlockZombieState.IceIncomplete1 ||
            iceState == IceBlockZombieState.IceIncomplete2)
        {
            if (level1ArmorHealth >= damage * 20) beAttacked(damage*20, 1, 1);
            else if (level1ArmorHealth <= damage * 20) level1ArmorHealth = 0;
        }
        else
        {
            beAttacked(10, 1, 1);
        }
    }
    protected override void HandleLevel1ArmorDamage(int hurt)
    {
        if (level1ArmorHealth <= level1ArmorMaxHealth * 0.7 && iceState == IceBlockZombieState.IceComplete)
        {
            iceDamage1();
        }
        if (level1ArmorHealth <= level1ArmorMaxHealth * 0.4 && iceState == IceBlockZombieState.IceIncomplete1)
        {
            iceDamage2();
        }
        if (level1ArmorHealth <= 0 && iceState == IceBlockZombieState.IceIncomplete2)
        {
            fallIce();
        }

    }

    private void iceDamage1()
    {
        hat.GetComponent<SpriteResolver>()
            .SetCategoryAndLabel("Head", "IceIncomplete1");

        iceState = IceBlockZombieState.IceIncomplete1;
    }

    private void iceDamage2()
    {
        hat.GetComponent<SpriteResolver>()
            .SetCategoryAndLabel("Head", "IceIncomplete2");

        iceState = IceBlockZombieState.IceIncomplete2;
    }

    private void fallIce()
    {
        hat.GetComponent<SpriteResolver>()
            .SetCategoryAndLabel("Head", "NoIce");
        outerarm_upper.SetActive(true);
        outerarm_lower.SetActive(true);
        outerarm_hand.SetActive(true);
        jaw.SetActive(true);

        iceState = IceBlockZombieState.NoIce;
    }

    protected override void dropArm()
    {
        if (!armIsDrop)
        {
            armIsDrop = true;

            AudioManager.Instance.PlaySoundEffect(59);

            outerarm_hand.SetActive(false);
            outerarm_lower.SetActive(false);
            outerarm_upper.GetComponent<SpriteResolver>()
                .SetCategoryAndLabel("Arm", "Incomplete");
            iceState = IceBlockZombieState.NoArm;
            if (!GameManagement.isPerformance)
            {
                GameObject gameObject = Instantiate(zombieArmDrops, outerarm_lower.transform.position, Quaternion.identity);
            }
        }
    }

    public override  void ApplyDeceleration() // ÓÃÓÚ¼õËÙ
    {
        if (level1ArmorHealth > 0) { return; }
        else { base.ApplyDeceleration(); }
    }
}
enum IceBlockZombieState { IceComplete, IceIncomplete1, IceIncomplete2, NoIce, NoArm };
