using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForestZombie_P : ForestZombie
{
    //污染森林僵尸

    protected override void Start()
    {
        base.Start();
        buff.坚韧 = 4;
       
    }

    protected override void HandleBodyDamage(int hurt)
    {
        // 这里可以处理本体受伤后的逻辑，例如扣除血量、播放受伤音效、动画等
        血量 -= hurt; // 根据难度减伤
        //Debug.Log($"本体受到 {hurt} 点伤害，剩余血量: {bloodVolume}");
        // 可以在这里添加本体受伤后的其他逻辑，例如播放受伤动画、音效等
        计算坚韧();
    }


    protected override void doAfterStartSomeTimes()//自然死亡是必定产生污染草丛
    {
        if (alive)
        {
            zombieForestSlider.DecreaseSliderValueSmooth(7);
            GameObject zombieLeaf = Instantiate(ZombieLeaf, gameObject.transform.position, Quaternion.identity);
            zombieLeaf.GetComponent<ZombieLeaf>().init(pos_row);
            die();
        }

    }

}
