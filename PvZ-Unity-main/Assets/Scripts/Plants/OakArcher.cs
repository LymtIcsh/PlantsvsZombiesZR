using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class OakArcher : Plant
{
    // 橡木弓箭手,优先攻击本行血量比例最低的目标

    public List<Zombie> zombieGenericList;


    public StraightBullet Arrow;
    public GameObject ShootPoint;
    protected override void Start()
    {
        base.Start();
    
    }

    public void FireEvent() {
      
            StraightBullet arrow = Instantiate(Arrow, ShootPoint.transform.position, Quaternion.identity);
            arrow.GetComponent<StraightBullet>().initialize(row);
        forestSlider.DecreaseSliderValueSmooth(2);

        
    }


}
