using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.XR;
using System;
using UnityEngine.Serialization;

public class ThrowBullet : MonoBehaviour
{
    public float speed = 4;   // 子弹速度
    public float rotateSpeed = 4;  // 空中旋转速度
    public int hurt;

    public GameObject boomParticle;  // 爆炸粒子效果
    protected Plant myPlant;  // 子弹所属植物

    private int row;
    private bool boom = false;
    private bool moving = false;
    private Vector2 initialPos;
    private float a;
    private float b;

    public int bulletType;  // 子弹类型: 0-普通, 1-中毒, 2-减速
    [FormerlySerializedAs("附加中毒层数")] [Header("附加中毒层数")]
    public int _addPoisoningLevels;
    [FormerlySerializedAs("引爆中毒层数")] [Header("引爆中毒层数")]
    public int _explosionPoisoningLevels;
    [FormerlySerializedAs("增加森林值")] [Header("增加森林值")]
    public int _increaseForestValue;


    void Update()
    {
        if (moving)
        {
            float delta_x = speed * Time.deltaTime;
            float x = transform.position.x - initialPos.x + delta_x;
            float y = a * x * x + b * x;
            transform.position = new Vector2(x, y) + initialPos;
            transform.Rotate(new Vector3(0, 0, -rotateSpeed * Time.deltaTime));
        }

        // 当子弹低于初始位置某个高度时爆炸
        if (transform.position.y < initialPos.y - 0.5f)
        {
            blast();
        }
    }

    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Zombie"))
        {
            // 检查碰撞对象是否为 Zombie 类型
            Zombie zombieGeneric = collision.GetComponent<Zombie>();
            if (zombieGeneric != null && row == zombieGeneric.pos_row)
            {
                attack(zombieGeneric);
            }
            //else
            //{
            //    // 如果不是 Zombie 类型，检查是否为 Zombie 类型
            //    Zombie zombie = collision.GetComponent<Zombie>();
            //    if (zombie != null && row == zombie.pos_row)
            //    {
            //        attack(zombie);
            //    }
            //}


        }
        else if (collision.CompareTag("BulletDisappearLine"))
        {
            Destroy(gameObject);  // 如果子弹超过了屏幕范围，销毁
        }
    }

    private void HideSpriteRenderer()
    {
        // 获取 SpriteRenderer 组件并隐藏
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer != null)
        {
            spriteRenderer.enabled = false;  // 隐藏 SpriteRenderer
        }
    }

    private void blast()
    {
        if (!boom)  // 防止重复调用
        {
            System.Random random = new System.Random();
            System.Random rand = new System.Random();
            int result = rand.Next(0, 2);
            AudioManager.Instance.PlaySoundEffect(result);
            boom = true;
            moving = false;

            if(!GameManagement.isPerformance)
            {
                GameObject shatterEffect = Instantiate(boomParticle, transform.position, Quaternion.identity);
            }
            

            // 启动协程等待音效播放完成再销毁
            WaitForSoundAndDestroy();
        }
    }

    private void WaitForSoundAndDestroy()
    {
        disappear();  // 音效播放完成后销毁子弹对象
    }

    protected virtual void attack(Zombie zombie)
    {
        if (!zombie.buff.Stealth)
        {
            // 对僵尸执行攻击逻辑
            switch (bulletType)
        {
            case 0: break; // 普通子弹无效果
            case 1: zombie.ApplyPoison(_addPoisoningLevels); zombie.DetonatePoisonDamage(_explosionPoisoningLevels); GameManagement.instance.forestSlider.DecreaseSliderValueSmooth(_increaseForestValue) ; break; // 中毒
            case 2: zombie.ApplyDeceleration(); break; // 冰冻
            default: break;
        }

      
            zombie.beAttacked(hurt, 2, 1);  // 使用 2 作为攻击模式
        }
        blast();  // 在攻击完成后调用 blast
    }

    //protected virtual void attack(Zombie zombie)
    //{
    //    // 对僵尸执行攻击逻辑
    //    switch (bulletType)
    //    {
    //        case 0: break; // 普通子弹无效果
    //        case 1: zombie.beParasiticed(3, 1); break; // 中毒
    //        case 2: zombie.beFrozeAttacked(); break; // 冰冻
    //        default: break;
    //    }

    //    zombie.beAttacked(hurt, true);  // 使用默认的攻击方式

    //    blast();  // 在攻击完成后调用 blast
    //}

    protected void disappear()
    {
        Destroy(gameObject);  // 销毁子弹对象
    }

    // 初始化时为 Zombie 目标
    public void initialize(Zombie targetZombie, Plant myPlant, int row)
    {
        this.row = row;
        this.myPlant = myPlant;

        // 计算抛物线的参数
        initialPos = transform.position;
        float distance = targetZombie.transform.position.x - initialPos.x;
        float y = distance / 3;
        float x = distance / 2;
        a = -y / (x * x);
        b = 2 * (-a) * x;
        moving = true;
    }

    // 初始化时为 Zombie 目标，并包含伤害值
    public void initialize(Zombie targetZombie, Plant myPlant, int row, int hurt)
    {
        this.row = row;
        this.myPlant = myPlant;
        this.hurt = hurt;

        initialPos = transform.position;
        float distance = targetZombie.transform.position.x - initialPos.x;
        float y = distance / 3;
        float x = distance / 2;
        a = -y / (x * x);
        b = 2 * (-a) * x;
        moving = true;
    }

    //// 初始化时为 Zombie 目标
    //public void initialize(Zombie targetZombie, Plant myPlant, int row)
    //{
    //    this.row = row;
    //    this.myPlant = myPlant;

    //    // 计算抛物线的参数
    //    initialPos = transform.position;
    //    float distance = targetZombie.transform.position.x - initialPos.x;
    //    float y = distance / 3;
    //    float x = distance / 2;
    //    a = -y / (x * x);
    //    b = 2 * (-a) * x;
    //    moving = true;
    //}

    //// 初始化时为 Zombie 目标，并包含伤害值
    //public void initialize(Zombie targetZombie, Plant myPlant, int row, int hurt)
    //{
    //    this.row = row;
    //    this.myPlant = myPlant;
    //    this.hurt = hurt;

    //    // 计算抛物线的参数
    //    initialPos = transform.position;
    //    float distance = targetZombie.transform.position.x - initialPos.x;
    //    float y = distance / 3;
    //    float x = distance / 2;
    //    a = -y / (x * x);
    //    b = 2 * (-a) * x;
    //    moving = true;
    //}
}
