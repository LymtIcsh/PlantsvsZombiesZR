using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.XR;
using System;
using UnityEngine.Serialization;

public class ThrowBullet : MonoBehaviour
{
    public float speed = 4;   // �ӵ��ٶ�
    public float rotateSpeed = 4;  // ������ת�ٶ�
    public int hurt;

    public GameObject boomParticle;  // ��ը����Ч��
    protected Plant myPlant;  // �ӵ�����ֲ��

    private int row;
    private bool boom = false;
    private bool moving = false;
    private Vector2 initialPos;
    private float a;
    private float b;

    public int bulletType;  // �ӵ�����: 0-��ͨ, 1-�ж�, 2-����
    [FormerlySerializedAs("�����ж�����")] [Header("�����ж�����")]
    public int _addPoisoningLevels;
    [FormerlySerializedAs("�����ж�����")] [Header("�����ж�����")]
    public int _explosionPoisoningLevels;
    [FormerlySerializedAs("����ɭ��ֵ")] [Header("����ɭ��ֵ")]
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

        // ���ӵ����ڳ�ʼλ��ĳ���߶�ʱ��ը
        if (transform.position.y < initialPos.y - 0.5f)
        {
            blast();
        }
    }

    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Zombie"))
        {
            // �����ײ�����Ƿ�Ϊ Zombie ����
            Zombie zombieGeneric = collision.GetComponent<Zombie>();
            if (zombieGeneric != null && row == zombieGeneric.pos_row)
            {
                attack(zombieGeneric);
            }
            //else
            //{
            //    // ������� Zombie ���ͣ�����Ƿ�Ϊ Zombie ����
            //    Zombie zombie = collision.GetComponent<Zombie>();
            //    if (zombie != null && row == zombie.pos_row)
            //    {
            //        attack(zombie);
            //    }
            //}


        }
        else if (collision.CompareTag("BulletDisappearLine"))
        {
            Destroy(gameObject);  // ����ӵ���������Ļ��Χ������
        }
    }

    private void HideSpriteRenderer()
    {
        // ��ȡ SpriteRenderer ���������
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer != null)
        {
            spriteRenderer.enabled = false;  // ���� SpriteRenderer
        }
    }

    private void blast()
    {
        if (!boom)  // ��ֹ�ظ�����
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
            

            // ����Э�̵ȴ���Ч�������������
            WaitForSoundAndDestroy();
        }
    }

    private void WaitForSoundAndDestroy()
    {
        disappear();  // ��Ч������ɺ������ӵ�����
    }

    protected virtual void attack(Zombie zombie)
    {
        if (!zombie.buff.Stealth)
        {
            // �Խ�ʬִ�й����߼�
            switch (bulletType)
        {
            case 0: break; // ��ͨ�ӵ���Ч��
            case 1: zombie.ApplyPoison(_addPoisoningLevels); zombie.DetonatePoisonDamage(_explosionPoisoningLevels); GameManagement.instance.forestSlider.DecreaseSliderValueSmooth(_increaseForestValue) ; break; // �ж�
            case 2: zombie.ApplyDeceleration(); break; // ����
            default: break;
        }

      
            zombie.beAttacked(hurt, 2, 1);  // ʹ�� 2 ��Ϊ����ģʽ
        }
        blast();  // �ڹ�����ɺ���� blast
    }

    //protected virtual void attack(Zombie zombie)
    //{
    //    // �Խ�ʬִ�й����߼�
    //    switch (bulletType)
    //    {
    //        case 0: break; // ��ͨ�ӵ���Ч��
    //        case 1: zombie.beParasiticed(3, 1); break; // �ж�
    //        case 2: zombie.beFrozeAttacked(); break; // ����
    //        default: break;
    //    }

    //    zombie.beAttacked(hurt, true);  // ʹ��Ĭ�ϵĹ�����ʽ

    //    blast();  // �ڹ�����ɺ���� blast
    //}

    protected void disappear()
    {
        Destroy(gameObject);  // �����ӵ�����
    }

    // ��ʼ��ʱΪ Zombie Ŀ��
    public void initialize(Zombie targetZombie, Plant myPlant, int row)
    {
        this.row = row;
        this.myPlant = myPlant;

        // ���������ߵĲ���
        initialPos = transform.position;
        float distance = targetZombie.transform.position.x - initialPos.x;
        float y = distance / 3;
        float x = distance / 2;
        a = -y / (x * x);
        b = 2 * (-a) * x;
        moving = true;
    }

    // ��ʼ��ʱΪ Zombie Ŀ�꣬�������˺�ֵ
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

    //// ��ʼ��ʱΪ Zombie Ŀ��
    //public void initialize(Zombie targetZombie, Plant myPlant, int row)
    //{
    //    this.row = row;
    //    this.myPlant = myPlant;

    //    // ���������ߵĲ���
    //    initialPos = transform.position;
    //    float distance = targetZombie.transform.position.x - initialPos.x;
    //    float y = distance / 3;
    //    float x = distance / 2;
    //    a = -y / (x * x);
    //    b = 2 * (-a) * x;
    //    moving = true;
    //}

    //// ��ʼ��ʱΪ Zombie Ŀ�꣬�������˺�ֵ
    //public void initialize(Zombie targetZombie, Plant myPlant, int row, int hurt)
    //{
    //    this.row = row;
    //    this.myPlant = myPlant;
    //    this.hurt = hurt;

    //    // ���������ߵĲ���
    //    initialPos = transform.position;
    //    float distance = targetZombie.transform.position.x - initialPos.x;
    //    float y = distance / 3;
    //    float x = distance / 2;
    //    a = -y / (x * x);
    //    b = 2 * (-a) * x;
    //    moving = true;
    //}
}
