using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MelonPult : MultiImagePlant
{
    public GameObject rolling;
    public List<GameObject> bullets;

    private Vector2 castEndPoint;   //����Ͷ��ʱ�յ�

    public GameObject Melon;//��������λ��

    protected override void Start()
    {
        base.Start();
        castEndPoint = new Vector2(6f, transform.position.y);
    }

    public override void initialize(PlantGrid grid, string sortingLayer, int sortingOrder)
    {
        base.initialize(grid, sortingLayer, sortingOrder);
        castEndPoint = new Vector2(6f, transform.position.y);
    }

    public void fireEvent()
    {
        //// ʹ�����߼�⽩ʬ
        //RaycastHit2D[] hitResults =
        //    Physics2D.LinecastAll(transform.position, castEndPoint, LayerMask.GetMask("Zombie"));





        //if (audioSource == null)
        //{
        //    Debug.Log("AudioSource is not assigned.");
        //}

        //foreach (RaycastHit2D hitResult in hitResults)
        //{

        //    // ��ȡ��ʬ���
        //    Zombie zombieGeneric = hitResult.transform.GetComponent<Zombie>();
        //    Zombie zombie = hitResult.transform.GetComponent<Zombie>(); // ��ȡ Zombie

        GameObject z = GetZombieWithMinX();
        if (z == null)
        {
            return;
        }


       
        Zombie zombieGeneric = z.transform.GetComponent<Zombie>();
        //Zombie zombie = z.transform.GetComponent<Zombie>(); // ��ȡ Zombie
        if (zombieGeneric != null && zombieGeneric.pos_row == row)
            {
                // ѡ��һ������ӵ�����
                int bulletIndex = Random.Range(0, bullets.Count);
                GameObject bullet = Instantiate
                    (
                    bullets[bulletIndex],
                    Melon.transform.position,
                    Quaternion.identity
                );

                ThrowBullet bulletScript = bullet.GetComponent<ThrowBullet>();
                bulletScript.initialize(zombieGeneric, this, row); // ���� Zombie

            }
        //else if (zombie != null && zombie.pos_row == row)
        //    {
        //        // ����������� Zombie ���͵Ľ�ʬ
        //        int bulletIndex = Random.Range(0, bullets.Count);
        //        GameObject bullet = Instantiate(
        //            bullets[bulletIndex],
        //            Melon.transform.position,
        //            Quaternion.identity
        //        );

        //        ThrowBullet bulletScript = bullet.GetComponent<ThrowBullet>();
        //        bulletScript.initialize(zombie, this, row); // ���� Zombie

        //        audioSource.Play();
        //    }
        ////}


    }

    public GameObject GetZombieWithMinX()
    {
        GameObject minXZombie = null;
        float minX = float.MaxValue;

        foreach (GameObject zombie in detectZombieRegion.zombiesInRegion.ToList())
        {
            if (zombie != null)
            {
                if (zombie.transform.position.x < minX)
                {
                    minX = zombie.transform.position.x;
                    minXZombie = zombie;
                }
            }
            else
            {
                detectZombieRegion.zombiesInRegion.Remove(zombie);
            }
        }

        return minXZombie;
    }

    public override void cold()
    {
        // �䶳�߼�������У�
    }

    protected override void beforeDie()
    {
        //// ����Ƿ�ͨ���ض��ؿ����������ϱ�������
        //if (LevelManagerStatic.IsLevelCompleted(61))  // ����ͨ�����ϱ�����ؿ������ٻ�����
        //{
        //    Instantiate(rolling, transform.position, Quaternion.identity);
        //}
    }
}
