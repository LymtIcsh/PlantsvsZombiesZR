using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecialStraightBullet : StraightBullet
{

    //���������ӵ������Գ������ⷽ��ֱ���ƶ����ҹ�����ʬ��������
    public Vector2 TheWay = new Vector2(1,0);

    void Start() {
        float angle = Mathf.Atan2(TheWay.y, TheWay.x) * Mathf.Rad2Deg;//������X��ļн�
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }
    

    protected override void Update()
    {

        if (!boomState)
        {
            //��Ϊ�ܳ������ⷽ�򣬹ʲ���������Ӫ
            transform.Translate(speed * Time.deltaTime,0,0);
        
        }
    }

    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        if (Camp == 0)//ֲ�﷽���ӵ�
        {
            if (collision.CompareTag("Zombie"))
            {
                // �ж��Ƿ��� Zombie ����
                Zombie zombieGeneric = collision.GetComponent<Zombie>();

                if (zombieGeneric != null && zombieGeneric.buff.Stealth == false) // ����� Zombie
                {
                    if (boomState == false)
                    {

                        attack(zombieGeneric);  // ��Ϊ���� attack(Zombie)
                    }
                }

            }
        }
        else
        {
            if (collision.tag == "Plant" && collision.GetComponent<Plant>()._plantType == PlantType.NormalPlants)
            {

                if (peaType == 0)
                {
                    System.Random random = new System.Random();
                    System.Random rand = new System.Random();
                    int result = rand.Next(2, 4);
                    AudioManager.Instance.PlaySoundEffect(result);
                }
                else if (peaType == 1)
                {
                    AudioManager.Instance.PlaySoundEffect(30);
                }

                Plant plant = collision.GetComponent<Plant>();
                plant.beAttacked(hurt, "BeHit", null);
                boom();
            }

        }
        if (collision.CompareTag("BulletDisappearLine"))
        {
            Destroy(gameObject);
        }
    }





}
